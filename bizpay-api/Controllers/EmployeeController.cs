using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bizpay_api.Data;
using System.Text.Json;
using bizpay_api.Models;
using bizpay_api.Repository;
using Microsoft.AspNetCore.Authorization;
using bizpay_api.Services;
using System.IdentityModel.Tokens.Jwt;
using bizpay_api.Shared;

namespace bizpay_api.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly APIDbContext _dbContext;

        public EmployeeController(APIDbContext context)
        {
            _dbContext = context;
        }

        // GET: api/employee
        [HttpGet]
        [Route("api/employee")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            if (_dbContext.Employees == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            try
            {
                var employeeList = await _dbContext.Employees
                    .Include(p => p.Permition)
                    .Include(r => r.Role)
                    .ThenInclude(d => d.Department)
                    .ToListAsync();

                if (employeeList.Any())
                {
                    return employeeList;
                }
                else
                {
                    return StatusCode(404, "Lista de funcionários vazia!");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // GET: api/employee/{cpf}

        [HttpGet]
        [Route("api/employee/{cpf}")]
        [Authorize]
        public async Task<ActionResult<Employee>> GetEmployeeByCpf(string cpf)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            if (String.IsNullOrEmpty(cpf))
            {
                return StatusCode(400, "Informe dos dados corretamente!");
            }

            try
            {
                var employee = await _dbContext.Employees
                    .Include(p => p.Permition)
                    .Include(r => r.Role)
                    .ThenInclude(d => d.Department)
                    .FirstOrDefaultAsync(e => e.Cpf == cpf);


                if (employee == null)
                {
                    return NotFound("Funcionário inválido!");
                }

                return employee;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        // POST: api/employee
        [HttpPost]
        [Route("api/employee")]
        public async Task<ActionResult> CreateEmployee(EmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Employees == null)
                {
                    return NotFound(new { message = "Contexto de banco dados inválido!" });
                };

                try
                {

                    if (!EmployeeExists(employee.Cpf))
                    {
                        Employee newEmployee = new Employee();
                        newEmployee.FromDTO(employee);
                        await _dbContext.Employees.AddAsync(newEmployee);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return Conflict(new { message = "O registro do funcionário já existe no banco de dados!" });
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

            return Ok(new { message = "Funcionário criado com sucesso!" });
        }

        // PATCH: api/employee
        [HttpPatch]
        [Route("api/employee")]
        public async Task<ActionResult> UpdateEmployee(EmployeeDTO employee)
        {

            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            try
            {

                if (EmployeeExists(employee.Cpf))
                {
                    if (!String.IsNullOrEmpty(employee.Cpf))
                    {

                        Employee updatedEmployee = new Employee();
                        updatedEmployee.FromDTO(employee);
                        if (employee.Status == Status.Inactive)
                        {
                            updatedEmployee.TerminationDate = GetBrasiliaTime.Time();
                        } else
                        {
                            updatedEmployee.TerminationDate = null;
                        }
                        _dbContext.Employees.Update(updatedEmployee);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest(new { message = "Houve um problema na requisição, favor informe os dados corretamente!" });
                    }
                }
                else
                {
                    return NotFound(new { message = "O funciónário que você esta tentando atualizar não existe!" });
                }

                return Ok(new { message = "Funcionário atualizado com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("api/employee/token/{token}")]
        public async Task<ActionResult<Employee>> GetEmployeeByToken(String token)
        {
            if (token == null)
            {
                return NotFound();
            }

            if (_dbContext.Employees == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenRead = tokenHandler.ReadJwtToken(token);
                var claims = tokenRead.Claims;

                string subClaimValue = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
                string permissionClaimValue = claims.FirstOrDefault(claim => claim.Type == "Permition")?.Value;

                if (!string.IsNullOrEmpty(subClaimValue) && !string.IsNullOrEmpty(permissionClaimValue))
                {
                    var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Email == subClaimValue);

                    if (employee == null)
                    {
                        return NotFound();
                    }

                    return employee;
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return Unauthorized("Login falhou");
            }
        }

        private bool EmployeeExists(string Cpf)
        {
            return (_dbContext.Employees?.Any(e => e.Cpf == Cpf)).GetValueOrDefault();
        }
    }
}
