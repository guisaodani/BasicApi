using BasicApi.BackEnd.Repositories.Interfaces;
using BasicApi.BackEnd.UnitsOfWork.Interfaces;
using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;

namespace BasicApi.BackEnd.UnitsOfWork.Implementations;

public class EmployeeUnitOfWork : GenericUnitOfWork<Employee>, IEmployeeUnitOfWork
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeUnitOfWork(IEmployeeRepository employeeRepository) : base(employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ActionResponse<IEnumerable<Employee>>> SearchAsync(string letter)
    {
        return await _employeeRepository.SearchAsync(letter);
    }
}