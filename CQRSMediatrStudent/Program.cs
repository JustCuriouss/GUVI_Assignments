using CQRSMediatrStudent.Data;
using CQRSMediatrStudent.Behaviors;
using CQRSMediatrStudent.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("StudentsDb"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Register FluentValidation validators
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Register pipeline behavior
// typeof(IPipelineBehavior<,>)  Tells.NET to apply this registration to all MediatR request/response pairs.
// typeof(ValidationBehavior<,>)    The actual class that implements validation logic.

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
