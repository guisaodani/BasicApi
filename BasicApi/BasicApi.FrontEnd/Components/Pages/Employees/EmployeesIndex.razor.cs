using BasicApi.FrontEnd.Repositories;
using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using BasicApi.FrontEnd.Components.Pages.Shared;
using BasicApi.Shared.Responses;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization;

namespace BasicApi.FrontEnd.Components.Pages.Employees;

[Authorize(Roles = "Admin")]
public partial class EmployeesIndex
{
    private List<Employee>? employees { get; set; }
    private MudTable<Employee> table = new();
    private readonly int[] pageSizeOptions = { 10, 25, 50, int.MaxValue };
    private int totalRecords = 0;
    private bool loading;
    private const string baseUrl = "api/Employes";
    private string infoFormat = "{first_item}-{last_item} => {all_items}";

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadTotalRecordsAsync();
    }

    private async Task LoadTotalRecordsAsync()
    {
        loading = true;
        var url = $"{baseUrl}/totalRecords";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"?filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<int>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return;
        }

        totalRecords = responseHttp.Response;
        loading = false;
    }

    private async Task<TableData<Employee>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"{baseUrl}/paginated/?page={page}&recordsnumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<List<Employee>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return new TableData<Employee> { Items = [], TotalItems = 0 };
        }
        if (responseHttp.Response == null)
        {
            return new TableData<Employee> { Items = [], TotalItems = 0 };
        }
        return new TableData<Employee>
        {
            Items = responseHttp.Response,
            TotalItems = totalRecords
        };
    }

    private async Task SetFilterValue(string value)
    {
        Filter = value;
        await LoadTotalRecordsAsync();
        await table.ReloadServerData();
    }

    private async Task ShowModalAsync(int id = 0, bool isEdit = false)
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            CloseButton = true
        };

        IDialogReference? dialog;

        if (isEdit)
        {
            var parameters = new DialogParameters
        {
            { "Id", id }
        };

            dialog = await DialogService.ShowAsync<EmployeesEdit>("Editar empleado", parameters, options);
        }
        else
        {
            dialog = await DialogService.ShowAsync<EmployeesCreate>("Nuevo empleado", options);
        }

        // 🔹 Verificar si dialog fue nulo (raro, pero el compilador lo exige)
        if (dialog is null)
        {
            Snackbar.Add("No se pudo abrir el cuadro de diálogo.", Severity.Error);
            return;
        }

        // 🔹 Esperar el resultado y verificarlo
        var result = await dialog.Result;

        if (result is null)
        {
            Snackbar.Add("No se obtuvo una respuesta del cuadro de diálogo.", Severity.Warning);
            return;
        }

        // 🔹 Solo recargar si el usuario NO canceló
        if (!result.Canceled)
        {
            await LoadTotalRecordsAsync();

            if (table is not null)
            {
                await table.ReloadServerData();
            }
        }
    }

    private async Task DeleteAsync(Employee employee)
    {
        var parameters = new DialogParameters
    {
        { "Message", $"¿Estás seguro de eliminar al empleado: {employee.FirstName} {employee.LastName}?" }
    };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.ExtraSmall,
            CloseOnEscapeKey = true
        };

        var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirmación", parameters, options);

        if (dialog is null)
        {
            Snackbar.Add("No se pudo abrir el cuadro de diálogo de confirmación.", Severity.Error);
            return;
        }

        var result = await dialog.Result;

        if (result is null)
        {
            Snackbar.Add("No se obtuvo respuesta del cuadro de diálogo.", Severity.Warning);
            return;
        }

        if (result.Canceled)
        {
            return;
        }

        // 🔹 Aquí continúa tu lógica de eliminación
        var responseHttp = await Repository.DeleteAsync($"{baseUrl}/{employee.Id}");

        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message ?? "Error al eliminar el empleado.", Severity.Error);
            return;
        }

        await LoadTotalRecordsAsync();
        await table!.ReloadServerData();
        Snackbar.Add("Empleado eliminado correctamente.", Severity.Success);
    }

    // (Opcional) Búsqueda por letra usando tu endpoint GET api/Employes/find?letter=J

    private async Task SearchByLetterAsync(string letter)
    {
        var url = $"{baseUrl}/find?letter={Uri.EscapeDataString(letter)}";
        var responseHttp = await Repository.GetAsync<List<Employee>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message ?? "Error buscando por letra.", Severity.Error);
            return;
        }

        employees = responseHttp.Response ?? [];
        totalRecords = employees.Count;
        // Si quieres reflejar en la tabla usando ServerData, podrías:
        await table.ReloadServerData();
    }
}