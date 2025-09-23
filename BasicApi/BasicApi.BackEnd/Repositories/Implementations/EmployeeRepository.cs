using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.Repositories.Interfaces;
using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Repositories.Implementations;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly DataContext _contex;

    public EmployeeRepository(DataContext contex) : base(contex)
    {
        _contex = contex;
    }

    public async Task<ActionResponse<IEnumerable<Employee>>> SearchAsync(string letter)
    {
        var results = await _contex.Employees
            .Where(e => e.FirstName.Contains(letter) || e.LastName.Contains(letter))
            .ToListAsync();

        if (!results.Any())
        {
            return new ActionResponse<IEnumerable<Employee>>
            {
                WasSuccess = false,
                Message = "No se encontraron registros."
            };
        }
        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = results
        };
    }
}