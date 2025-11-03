using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.Repositories.Interfaces;

public interface IStatesRepository
{
    Task<ActionResponse<State>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<State>>> GetAsync();
}