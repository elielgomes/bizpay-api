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

namespace bizpay_api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly APIDbContext _context;

        public EmployeeController(APIDbContext context)
        {
            _context = context;
        }

        /*// GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateEmployee(EmployeeDTO employee)
        {

            if (ModelState.IsValid)
            {

                if (_context.Employees == null)
                {
                    return Problem("Contexto do banco inválido!");
                }

                Employee dbEmployee = new Employee() { Id = Guid.NewGuid() };

                if (employee.Id != Guid.Empty)
                {
                    dbEmployee = await _context.Employees.FindAsync(employee.Id);
                }

                dbEmployee.FromDTO(employee);

                if (employee.Id == Guid.Empty)
                {
                    _context.Employees.Add(dbEmployee);
                } else
                {
                    _context.Employees.Update(dbEmployee);
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return new JsonResult(
                    new
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                        Value = new StringContent(JsonSerializer.Serialize(errorMessages)),
                    });
            }

            return new JsonResult(
                    new
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.OK,
                        Value = new StringContent(JsonSerializer.Serialize(new { message = "Funcionario criado com sucesso!" })),
                    });
        }


        private bool EmployeeExists(Guid id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
