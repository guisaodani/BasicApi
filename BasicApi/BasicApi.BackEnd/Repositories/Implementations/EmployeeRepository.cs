using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.Helpers;
using BasicApi.BackEnd.Repositories.Interfaces;
using BasicApi.Shared.DTOs;
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

    public async Task<IEnumerable<Employee>> GetComboAsync()
    {
        return await _contex.Employees
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToListAsync();
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _contex.Employees.AsQueryable();

        // Aplicar filtro si existe
        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(e =>
                e.FirstName.ToLower().Contains(pagination.Filter.ToLower()) ||
                e.LastName.ToLower().Contains(pagination.Filter.ToLower()));
        }

        // Ordenar y paginar
        var employees = await queryable
            .OrderBy(e => e.FirstName)
            .Paginate(pagination)
            .ToListAsync();

        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = employees
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _contex.Employees.AsQueryable();

        // Aplicar el mismo filtro
        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(e =>
                e.FirstName.ToLower().Contains(pagination.Filter.ToLower()) ||
                e.LastName.ToLower().Contains(pagination.Filter.ToLower()));
        }

        var count = await queryable.CountAsync();

        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = count
        };
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