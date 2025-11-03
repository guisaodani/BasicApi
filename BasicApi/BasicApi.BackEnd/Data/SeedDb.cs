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
            await CheckCountriesFullAsync();
            await CheckEmployeesAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = [
                        new State()
                {
                    Name = "Antioquia",
                    Cities = [
                        new City() { Name = "Medellín" },
                        new City() { Name = "Itagüí" },
                        new City() { Name = "Envigado" },
                        new City() { Name = "Bello" },
                        new City() { Name = "Rionegro" },
                    ]
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = [
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Useme" },
                        new City() { Name = "Bosa" },
                    ]
                },
            ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = [
                        new State()
                {
                    Name = "Florida",
                    Cities = [
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    ]
                },
                new State()
                    {
                        Name = "Texas",
                        Cities = [
                            new City() { Name = "Houston" },
                            new City() { Name = "San Antonio" },
                            new City() { Name = "Dallas" },
                            new City() { Name = "Austin" },
                            new City() { Name = "El Paso" },
                        ]
                    },
                ]
                });
            }
            await _context.SaveChangesAsync();
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