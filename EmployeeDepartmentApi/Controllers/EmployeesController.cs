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
    }
}
