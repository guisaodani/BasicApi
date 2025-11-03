using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.UnitsOfWork.Interfaces;

public interface IEmployeeUnitOfWork : IGenericUnitOfWork<Employee>
{
    Task<IEnumerable<Employee>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Employee>>> SearchAsync(string letter);
}