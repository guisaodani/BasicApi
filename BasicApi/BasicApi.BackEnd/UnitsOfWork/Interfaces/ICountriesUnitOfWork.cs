using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.UnitsOfWork.Interfaces;

public interface ICountriesUnitOfWork
{
    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();
}