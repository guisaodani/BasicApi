using BasicApi.BackEnd.Data;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployesController : ControllerBase
{
    private readonly DataContext _context;

    public EmployesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Employees.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string letter)
    {
        if (string.IsNullOrWhiteSpace(letter))
        {
            return BadRequest("Debe proporcionar un valor de búsqueda.");
        }

        letter = letter.ToLower();

        var employees = await _context.Employees
            .Where(e => e.FirstName.ToLower().Contains(letter)
                     || e.LastName.ToLower().Contains(letter))
            .ToListAsync();

        if (!employees.Any())
        {
            return NotFound($"No se encontraron empleados que contengan '{letter}'.");
        }

        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsyn(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(employee);
    }
}