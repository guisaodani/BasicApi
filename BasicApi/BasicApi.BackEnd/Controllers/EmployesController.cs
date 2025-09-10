using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.UnitsOfWork.Interfaces;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployesController : GenericController<Employee>
{
    private readonly IGenericUnitOfWork<Employee> _unitOfWork;

    public EmployesController(IGenericUnitOfWork<Employee> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("search")]
    public async Task<IActionResult> FindAsync([FromQuery] string letter)
    {
        if (string.IsNullOrWhiteSpace(letter))
        {
            return BadRequest("Debe proporcionar un valor de búsqueda.");
        }

        letter = letter.ToLower();

        var action = await _unitOfWork.FindAsync(e =>
            e.FirstName.ToLower().Contains(letter.ToLower()) ||
            e.LastName.ToLower().Contains(letter.ToLower())
        );

        if (!action.WasSuccess || !(action.Result?.Any() ?? false))
            return NotFound($"No se encontraron empleados que contengan '{letter}'.");

        return Ok(action.Result);
    }
}