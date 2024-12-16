using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.DbContext;
using TestTask.Data.Models;
using TestTask.Data.Repositories.ContractRepository;
using TestTask.Data.Repositories.EquipmentTypeRepository;
using TestTask.Data.Repositories.ProductFacilityRepository;
using TestTask.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsetings.Secret.json", optional: true, reloadOnChange: true);

builder.Services.AddDbContext<TestTaskDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IProductFacilityRepository, ProductFacilityRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

    db.Database.Migrate();
    app.UseDeveloperExceptionPage();
}

app.MapControllers();
app.UseRouting();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.MapGet("/", () => "TestTaskApi");

app.Run();
