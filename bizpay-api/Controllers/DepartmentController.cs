using bizpay_api.Data;
using bizpay_api.Models;
using bizpay_api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace bizpay_api.Controllers
{
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly APIDbContext _dbContext;

        public DepartmentController(APIDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        // GET: api/departments
        [HttpGet]
        [Route("/api/departments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            if (_dbContext.Departments == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            try
            {
                var departmentList = await _dbContext.Departments.ToListAsync();

                if (departmentList.Any())
                {
                    return departmentList;
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

        // GET: api/department/{id}
        [HttpGet]
        [Route("/api/department/{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(Guid id)
        {
            if (_dbContext.Departments == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            if (String.IsNullOrEmpty(id.ToString()))
            {
                return StatusCode(400, "Informe dos dados corretamente!");
            }

            try
            {
                var department = await _dbContext.Departments.FindAsync(id);

                if (department == null)
                {
                    return NotFound("Departamento inválido!");
                }

                return department;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // POST: api/employee
        [HttpPost]
        [Route("/api/department")]
        public async Task<ActionResult> CreateDepartment(DepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Departments == null)
                {
                    return NotFound(new { message = "Contexto de banco dados inválido!" });
                };

                try
                {

                    if (!DepartmentExists(department.Id))
                    {
                        Department newDepartment = new Department();
                        newDepartment.FromDTO(department);
                        await _dbContext.Departments.AddAsync(newDepartment);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return Conflict(new { message = "O registro do departamento já existe no banco de dados!" });
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

            return Ok(new { message = "Departamento criado com sucesso!" });
        }

        // PATCH: api/employee
        [HttpPatch]
        [Route("/api/department")]
        public async Task<ActionResult> UpdateDepartment(DepartmentDTO department)
        {

            if (_dbContext.Departments == null)
            {
                return NotFound();
            }

            try
            {

                if (DepartmentExists(department.Id))
                {
                    if (!String.IsNullOrEmpty(department.Id.ToString()))
                    {

                        Department updatedDepartment = new Department();
                        updatedDepartment.FromDTO(department);
                        _dbContext.Departments.Update(updatedDepartment);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest(new { message = "Houve um problema na requisição, favor informe os dados corretamente!" });
                    }
                }
                else
                {
                    return NotFound(new { message = "O departamento que você esta tentando atualizar não existe!" });
                }

                return Ok(new { message = "Departamento atualizado com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        // DELETE: api/employee
        [HttpDelete]
        [Route("/api/department/{id}")]
        public async Task<ActionResult> DeleteDepartment(Guid id)
        {
            if (_dbContext.Departments == null)
            {
                return NotFound();
            }

            try
            {
                var department = await _dbContext.Departments.FindAsync(id);

                if (department != null)
                {
                    _dbContext.Departments.Remove(department);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Departamento excluido com sucesso!");
                }
                else
                {
                    return NotFound("O departamento que está tentando excluir não existe!");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        private bool DepartmentExists(Guid id)
        {
            return (_dbContext.Departments?.Any(d => d.Id == id)).GetValueOrDefault();
        }

    }
}
