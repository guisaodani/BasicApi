using BasicApi.Shared.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BasicApi.FrontEnd.Components.Pages.Employees;

public partial class EmployeesForm
{
    //private EditContext editContext = null!;

    [EditorRequired, Parameter] public Employee Employee { get; set; } = null!;

    //[EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback<Employee> OnValidSubmit { get; set; } // 👈 pasa el modelo

    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    //protected override void OnInitialized()
    //{
    //    editContext = new(Employee);
    //}

    private async Task HandleValidSubmit()
    {
        await InvokeAsync(StateHasChanged);
        await OnValidSubmit.InvokeAsync();
    }
}