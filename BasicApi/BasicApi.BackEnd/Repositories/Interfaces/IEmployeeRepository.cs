using BasicApi.Shared.DTOs;
using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.Repositories.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<ActionResponse<IEnumerable<Employee>>> SearchAsync(string letter);
}