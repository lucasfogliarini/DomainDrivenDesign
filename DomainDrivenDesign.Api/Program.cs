using DomainDrivenDesign;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEFCoreDatabase(EFCoreProvider.InMemory);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IDatabase>());
builder.Services.AddProblemDetails(x =>
{
    x.MapToStatusCode<ValidationException>(StatusCodes.Status400BadRequest);
    x.MapToStatusCode<InvalidOperationException>(StatusCodes.Status400BadRequest);
    x.IncludeExceptionDetails = (ctx, ex) =>
    {
        return true;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseProblemDetails();

app.MapControllers();

using var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<IDatabase>();
await database!.SeedAsync();

app.Run();
