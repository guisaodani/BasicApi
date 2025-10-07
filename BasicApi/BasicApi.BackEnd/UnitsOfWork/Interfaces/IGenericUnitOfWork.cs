using BasicApi.Shared.DTOs;
using BasicApi.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BasicApi.BackEnd.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class
{
    //aca lo nuevo para pagination
    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<T>> UpdateAsync(T entity);
}