using BasicApi.BackEnd.UnitsOfWork.Interfaces;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BasicApi.BackEnd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : GenericController<City>
{
    public CitiesController(IGenericUnitOfWork<City> unitOfWork) : base(unitOfWork)
    {
    }
}