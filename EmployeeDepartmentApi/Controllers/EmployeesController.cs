using EmployeeDepartmentApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDepartmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifie que le département existe
            var department = await _context.Departments.FindAsync(employee.DepartmentId);
            if (department == null)
            {
                return NotFound($"Department with ID {employee.DepartmentId} not found.");
            }

            // Ajouter l'employé
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Retourne l’objet créé avec le code 201
            return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, employee);
        }

    }
}
