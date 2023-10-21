using bizpay_api.Data;
using bizpay_api.Models;
using bizpay_api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace bizpay_api.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly APIDbContext _dbContext;

        public RoleController(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: api/role
        [HttpGet]
        [Route("api/role")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            if (_dbContext.Payslips == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            };

            try
            {
                var roleList = await _dbContext.Roles.Include(d => d.Department).ToListAsync();
                if (roleList.Any())
                {
                    return roleList;
                }
                else
                {
                    return StatusCode(404, "Lista de departamentos vazia!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // GET: api/role/{id}
        [HttpGet]
        [Route("api/role/{id}")]
        public async Task<ActionResult<Role>> GetRoleById(Guid id)
        {
            if (_dbContext.Roles == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            if (String.IsNullOrEmpty(id.ToString()))
            {
                return StatusCode(400, "Informe dos dados corretamente!");
            }

            try
            {
                var role = await _dbContext.Roles.Include(d => d.Department).FirstOrDefaultAsync(r => r.Id == id);

                if (role == null)
                {
                    return NotFound("Cargo inválido!");
                }

                return role;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // GET: api/role/department/{departmentId}
        [HttpGet]
        [Route("api/role/department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesByDepartment(Guid departmentId)
        {
            if (_dbContext.Roles == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            if (String.IsNullOrEmpty(departmentId.ToString()))
            {
                return StatusCode(400, "Informe dos dados corretamente!");
            }

            try
            {
                var roleList = await _dbContext.Roles
                    .Include(d => d.Department)
                    .Where(r => r.DepartamentId == departmentId)
                    .ToListAsync();

                return roleList;
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");

            }
        }

        // POST: api/role
        [HttpPost]
        [Route("api/role")]
        public async Task<ActionResult> CreateRole(RoleDTO role)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Roles == null)
                {
                    return NotFound(new { message = "Contexto de banco dados inválido!" });
                };

                try
                {

                    if (!RoleExists(role.Id, role.Name))
                    {
                        Role newRole = new Role();
                        newRole.FromDTO(role);
                        await _dbContext.Roles.AddAsync(newRole);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return Conflict(new { message = "O registro do cargo já existe no banco de dados!" });
                    };

                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                return StatusCode(400, new StringContent(JsonSerializer.Serialize(errorMessages)));

            }

            return Ok(new { message = "Cargo criado com sucesso!" });
        }

        // PATCH: api/role
        [HttpPatch]
        [Route("/api/role")]
        public async Task<ActionResult> UpdateRole(RoleDTO role)
        {

            if (_dbContext.Roles == null)
            {
                return NotFound();
            }

            try
            {

                if (RoleExists(role.Id, role.Name))
                {
                    if (!String.IsNullOrEmpty(role.Id.ToString()))
                    {

                        Role updatedRole = new Role();
                        updatedRole.FromDTO(role);
                        _dbContext.Roles.Update(updatedRole);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest(new { message = "Houve um problema na requisição, favor informe os dados corretamente!" });
                    }
                }
                else
                {
                    return NotFound(new { message = "O cargo que você esta tentando atualizar não existe!" });
                }

                return Ok(new { message = "Cargo atualizado com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        // DELETE: api/role
        [HttpDelete]
        [Route("/api/role/{id}")]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            if (_dbContext.Roles == null)
            {
                return NotFound();
            }

            try
            {
                var role = await _dbContext.Roles.FindAsync(id);

                if (role != null)
                {
                    _dbContext.Roles.Remove(role);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Cargo excluido com sucesso!");
                }
                else
                {
                    return NotFound("O cargo que está tentando excluir não existe!");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        private bool RoleExists(Guid id, string name)
        {
            return (_dbContext.Roles?.Any(d => d.Id == id || d.Name == name)).GetValueOrDefault();
        }

    }
}
