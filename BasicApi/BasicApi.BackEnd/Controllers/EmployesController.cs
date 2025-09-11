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
}