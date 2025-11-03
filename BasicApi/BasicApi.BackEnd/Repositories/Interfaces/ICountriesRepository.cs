using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.Repositories.Interfaces;

public interface ICountriesRepository
{
    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();
}