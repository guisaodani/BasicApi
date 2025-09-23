using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.UnitsOfWork.Implementations;
using BasicApi.BackEnd.UnitsOfWork.Interfaces;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployesController : GenericController<Employee>
{
    private readonly IEmployeeUnitOfWork _employeeunitOfWork;

    public EmployesController(IEmployeeUnitOfWork employeeunitOfWork) : base(employeeunitOfWork)
    {
        _employeeunitOfWork = employeeunitOfWork;
    }

    [HttpGet("find")]
    public async Task<IActionResult> SearchAsync([FromQuery] string letter)
    {
        var action = await _employeeunitOfWork.SearchAsync(letter);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound(action.Message);
    }
}