using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.UnitsOfWork.Interfaces;

public interface IStatesUnitOfWork
{
    Task<ActionResponse<State>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<State>>> GetAsync();
}