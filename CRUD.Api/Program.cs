using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using CRUD.Api.Extensions;
using CRUD.Api.Handler;
using CRUD.Application.Extensions;
using CRUD.infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddTransient<IExceptionHandler, ExceptionHandler>();
builder.Services.AddSwaggerGenApi();
builder.Services.AddApplicationMediator<Program>();
builder.Services.AddApplicationInjection();
builder.Services.AddInfraInjection();
builder.Services.AddLocalApplicationContext();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();
app.MigrateApplicationDataBase();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUIApi();
}

app.UseExceptionHandlerApp();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
