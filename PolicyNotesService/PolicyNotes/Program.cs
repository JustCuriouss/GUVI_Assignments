using Microsoft.EntityFrameworkCore;
using PolicyNotes.Data;
using PolicyNotes.DTOs;
using PolicyNotes.Repositories;
using PolicyNotes.Services;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(static opts =>
    opts.UseInMemoryDatabase("PolicyNotesDb"));

builder.Services.AddScoped<IPolicyNoteRepository, PolicyNoteRepository>();
builder.Services.AddScoped<IPolicyNotesService, PolicyNotesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/notes", async (PolicyNoteCreateDto dto, IPolicyNotesService service, HttpContext ctx) =>
{
    var created = await service.AddNoteAsync(dto.PolicyNumber, dto.Note);
    return Results.Created($"/notes/{created.Id}", created);
})
.WithName("CreateNote")
.Produces(201)
.Accepts<PolicyNoteCreateDto>("application/json");

app.MapGet("/notes", async (IPolicyNotesService service) =>
{
    var notes = await service.GetAllNotesAsync();
    return Results.Ok(notes);
})
.WithName("GetAllNotes")
.Produces(200);

app.MapGet("/notes/{id:int}", async (int id, IPolicyNotesService service) =>
{
    var note = await service.GetNoteByIdAsync(id);
    return note is not null ? Results.Ok(note) : Results.NotFound();
})
.WithName("GetNoteById")
.Produces<PolicyNotes.Models.PolicyNote>(200)
.Produces(404);

// simple health
app.MapGet("/", () => Results.Ok("PolicyNotesService is running"));

app.Run();

// Needed so WebApplicationFactory<Program> can find Program class
public partial class Program { }
