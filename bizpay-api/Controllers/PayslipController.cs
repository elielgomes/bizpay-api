using bizpay_api.Data;
using bizpay_api.Models;
using bizpay_api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace bizpay_api.Controllers
{
    [ApiController]
    public class PayslipController : ControllerBase
    {

        private readonly APIDbContext _dbContext;

        public PayslipController(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/payslip/{cpf}
        [HttpGet]
        [Route("api/payslip/cpf/{cpf}")]
        public async Task<ActionResult<IEnumerable<Payslip>>> GetAllEmployeePayslipsByCpf(string cpf)
        {
            if (_dbContext.Payslips == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            };

            if (String.IsNullOrEmpty(cpf))
            {
                return StatusCode(400, "Informe dos dados corretamente!");
            }

            try
            {
                var employeeExists = (_dbContext.Employees?.Any(e => e.Cpf == cpf)).GetValueOrDefault();

                if (employeeExists)
                {
                    var payslipList = await _dbContext.Payslips
                        .Include(e => e.Employee)
                        .ThenInclude(c => c.Role)
                        .Where(p => p.EmployeeCpf == cpf)
                        .ToListAsync();
                    
                    return payslipList;
                  
                }
                else
                {
                    return NotFound("Funcionário inválido!");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");

            }
        }

        // GET: api/payslip/{id}
        [HttpGet]
        [Route("api/payslip/id/{id}")]
        public async Task<ActionResult<Payslip>> GetPayslipById(Guid id)
        {
			if (_dbContext.Payslips == null)
			{
				return NotFound(new { message = "Contexto de banco dados inválido!" });
			};

			if (String.IsNullOrEmpty(id.ToString()))
			{
				return StatusCode(400, "Informe dos dados corretamente!");
			}

			try
			{
				var payslipExists = (_dbContext.Payslips?.Any(e => e.Id == id)).GetValueOrDefault();

				if (payslipExists)
				{
					var payslipList = await _dbContext.Payslips
						.Include(e => e.Employee)
						.ThenInclude(c => c.Role)
						.Where(p => p.Id == id)
						.FirstOrDefaultAsync();

					if (payslipList != null)
					{
						return payslipList;
					}
					else
					{
						return NotFound("Holerite não encontrado!");
					}
				}
				else
				{
					return NotFound("Holerite não encontrado!");
				}

			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Erro interno do servidor: {ex.Message}");

			}
		}

        // POST: api/payslip
        [HttpPost]
        [Route("api/payslip")]
        public async Task<IActionResult> CreatePayslip(PayslipDTO payslip)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Employees == null)
                {
                    return NotFound(new { message = "Contexto de banco dados inválido!" });
                };

                try
                {
                    var employeeExists = (_dbContext.Employees?.Any(e => e.Cpf == payslip.EmployeeCpf)).GetValueOrDefault();

                    if (employeeExists)
                    {
                        if (!PayslipExists(payslip.Id))
                        {
                            Payslip newPayslip = new Payslip();
                            newPayslip.FromDTO(payslip);
                            newPayslip.SalaryCalculation();

                            await _dbContext.Payslips.AddAsync(newPayslip);
                            await _dbContext.SaveChangesAsync();
                        } else
                        {
                            return Conflict(new { message = "O registro do holerite já existe no banco de dados!" });
                        }

                    } else
                    {
                        return NotFound(new { message = "Funcionário inválido!" });
                    }
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

            return Ok(new { message = "Holerite criado com sucesso!" });
        }

        // PATCH: api/payslip
        [HttpPatch]
        [Route("api/payslip")]
        public async Task<ActionResult> UpdatePayslip(PayslipDTO payslip)
        {

            if (_dbContext.Payslips == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }
            try
            {

                if (PayslipExists(payslip.Id))
                {
                   
                    Payslip updatedPayslip = new Payslip();
                    updatedPayslip.FromDTO(payslip);
                    updatedPayslip.SalaryCalculation();

                    _dbContext.Payslips.Update(updatedPayslip);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return NotFound(new { message = "O Holerite que você esta tentando atualizar não existe!" });
                }

                return Ok(new { message = "Holerite atualizado com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }

        // DELETE: api/payslip/{id}
        [HttpDelete]
        [Route("/api/payslip/{id}")]
        public async Task<ActionResult> DeletePayslip(Guid id)
        {
            if (_dbContext.Payslips == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            try
            {
                var payslip = await _dbContext.Payslips.FindAsync(id);

                if (payslip != null)
                {
                    _dbContext.Payslips.Remove(payslip);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Holerite excluído com sucesso!");
                }
                else
                {
                    return NotFound("O Holerite que está tentando excluir não existe!");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }

        }


        private bool PayslipExists(Guid id)
        {
            return (_dbContext.Payslips?.Any(d => d.Id == id)).GetValueOrDefault();
        }
    }
}
