using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.Repositories.Interfaces;
using BasicApi.Shared.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BasicApi.BackEnd.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(DataContext contex)
    {
        _context = contex;
        _entity = _context.Set<T>();
    }

    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<T>
            {
                Message = ex.Message,
            };
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "Registro no encontrado."
            };
        }
        try
        {
            _entity.Remove(row);
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
            };
        }
        catch
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = "No se pudo eliminar el registro."
            };
        }
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> SearchAsync(string letter)
    {
        var results = await _entity
              .Where(x => EF.Property<string>(x, "FirstName").Contains(letter)
                || EF.Property<string>(x, "LastName").Contains(letter))
              .ToListAsync();

        if (!results.Any())

        {
            return new ActionResponse<IEnumerable<T>>
            {
                Message = "No se encontraron registros."
            };
        }
        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = results
        };
    }

    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "Registro no encontrado."
            };
        }
        return new ActionResponse<T>
        {
            WasSuccess = true,
            Result = row
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
    {
        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = await _entity.ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        _context.Update(entity);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<T>
            {
                Message = ex.Message,
            };
        }
    }
}