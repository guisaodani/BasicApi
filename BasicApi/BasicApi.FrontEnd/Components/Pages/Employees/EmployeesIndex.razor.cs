using BasicApi.FrontEnd.Repositories;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace BasicApi.FrontEnd.Components.Pages.Employees;

public partial class EmployeesIndex
{
    [Inject] private IRepository Repository { get; set; } = null!;
    private List<Employee>? employees;

    protected override async Task OnInitializedAsync()
    {
        var httpResult = await Repository.GetAsync<List<Employee>>("api/Employes");
        employees = httpResult.Response;
    }
}