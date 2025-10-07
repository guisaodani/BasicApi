using BasicApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly Random _random = new Random();

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckEmployeesAsync();
        }

        private async Task CheckEmployeesAsync()
        {
            if (!_context.Employees.Any())
            {
                var firstNames = new[]
                {
                    "Juan", "Carolina", "Andrés", "Valentina", "Camilo", "Laura", "Mateo", "Sara", "Felipe", "Mariana",
                    "Santiago", "Natalia", "Jorge", "Diana", "Alejandro", "Paula", "Daniel", "Isabella", "Samuel", "Juliana"
                };

                var lastNames = new[]
                {
                    "González", "Rodríguez", "López", "Martínez", "Pérez", "Gómez", "Torres", "Ramírez", "Castro", "Hernández",
                    "Moreno", "Vargas", "Jiménez", "Cruz", "Mendoza", "Ortega", "Suárez", "Romero", "Rojas", "Zapata"
                };

                var employees = new List<Employee>();

                for (int i = 0; i < 50; i++)
                {
                    var employee = new Employee
                    {
                        FirstName = firstNames[_random.Next(firstNames.Length)],
                        LastName = lastNames[_random.Next(lastNames.Length)],
                        IsActive = _random.Next(0, 2) == 1, // true o false aleatorio
                        HireDate = RandomHireDate(),
                        Salary = RandomSalary()
                    };

                    employees.Add(employee);
                }

                _context.Employees.AddRange(employees);
                await _context.SaveChangesAsync();
            }
        }

        private DateTime RandomHireDate()
        {
            int year = _random.Next(2015, DateTime.Now.Year + 1);
            int month = _random.Next(1, 13);
            int day = _random.Next(1, 28);
            return new DateTime(year, month, day);
        }

        private decimal RandomSalary()
        {
            return _random.Next(1000000, 25000001);
        }
    }
}