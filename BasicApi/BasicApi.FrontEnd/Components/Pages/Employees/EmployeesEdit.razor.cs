using BasicApi.FrontEnd.Repositories;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BasicApi.FrontEnd.Components.Pages.Employees;

public partial class EmployeesEdit
{
    private Employee? employee;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Employee>($"api/Employes/{Id}");

        if (responseHttp.Error)
        {
            Snackbar.Add("Error al cargar el empleado.", Severity.Error);
            NavigationManager.NavigateTo("/employees");
            return;
        }

        employee = responseHttp.Response!;
        StateHasChanged(); // 👈 fuerza actualización visual del formulario
    }

    private async Task EditAsync()
    {
        Console.WriteLine($"[DEBUG] FRONTEND: IsActive antes de enviar = {employee!.IsActive}");

        var responseHttp = await Repository.PutAsync("api/Employes", employee);

        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message ?? "Error al actualizar el empleado.", Severity.Error);
            return;
        }

        Snackbar.Add("Empleado actualizado correctamente.", Severity.Success);
        Return();
    }

    private void Return()
    {
        NavigationManager.NavigateTo("/employees");
    }
}