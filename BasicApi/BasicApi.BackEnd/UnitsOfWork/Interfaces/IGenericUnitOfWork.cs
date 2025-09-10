using BasicApi.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BasicApi.BackEnd.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class
{
    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> predicate);

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<T>> UpdateAsync(T entity);
}