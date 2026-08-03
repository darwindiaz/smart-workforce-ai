using SmartWorkforce.Application.Conciliations;
using SmartWorkforce.Infrastructure.Conciliations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<CreateConciliationHandler>();
builder.Services.AddScoped<IConciliationRepository, ConciliationRepository>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();