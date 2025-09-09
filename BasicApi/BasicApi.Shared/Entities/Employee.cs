using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicApi.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellido")]
    [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Estado")]
    public bool IsActive { get; set; }

    [Display(Name = "Fecha_contrato")]
    public DateTime HireDate { get; set; }

    [Display(Name = "Salario")]
    [MinLength(1000000, ErrorMessage = "El campo {0} debe ser minimo {1}. ")]
    [Required(ErrorMessage = "El campo {0} es olbigatorio.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }
}