using BasicApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Data;

public class SeedDb
{
    private readonly DataContext _context;

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
            _context.Employees.Add(new Employee
            {
                FirstName = "Juan",
                LastName = "Guisao",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Juan",
                LastName = "Perez",
                IsActive = true,
                HireDate = new DateTime(2025, 1, 15),
                Salary = 1000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Juan",
                LastName = "Perez",
                IsActive = true,
                HireDate = new DateTime(2025, 6, 15),
                Salary = 15000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Julian",
                LastName = "Goez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 10000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Jorge",
                LastName = "Castro",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 10100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Carolina",
                LastName = "Gonzalez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 6000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Andrea",
                LastName = "Cruz",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 8000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Deisy",
                LastName = "Perez",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 8000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Deisy",
                LastName = "Perez",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 8000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Juan",
                LastName = "Zuluaga",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 25000000
            });
        }
        await _context.SaveChangesAsync();
    }
}