using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.Repositories.Implementations;
using BasicApi.BackEnd.Repositories.Interfaces;
using BasicApi.BackEnd.UnitsOfWork.Implementations;
using BasicApi.BackEnd.UnitsOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BasicApiConnection"),
        sqlOptions =>
        {
            sqlOptions.CommandTimeout(300);      // ⏱ 5 minutos de timeout
            sqlOptions.EnableRetryOnFailure(5);  // 🔁 reintenta si hay fallos de red/transitorios
        }));

builder.Services.AddTransient<SeedDb>();

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeUnitOfWork, EmployeeUnitOfWork>();

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();

builder.Services.AddScoped<IStatesRepository, StatesRepository>();
builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();

var app = builder.Build();

SeedData(app);
void SeedData(WebApplication app)
{
    var ScopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = ScopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();