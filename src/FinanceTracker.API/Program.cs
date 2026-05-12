using FinanceTracker.Application.Accounts.Commands.CreateAccount;
using FinanceTracker.Application.Categories.Commands.CreateCategory;
using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Transactions.Commands.CreateTransaction;
using FinanceTracker.Application.Transactions.Commands.DeleteTransaction;
using FinanceTracker.Application.Transactions.Commands.UpdateTransaction;
using FinanceTracker.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles;
});

builder.Services.AddMediatR(typeof(CreateTransactionCommand));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "FinanceTracker API works!");

app.MapGet("/transactions", async (AppDbContext db) =>
{
    return await db.Transactions
        .Include(t => t.Account)
        .Include(t => t.Category)
        .ToListAsync();
});

app.MapGet("/accounts", async (IAppDbContext db) =>
{
    return await db.Accounts.ToListAsync();
});

app.MapGet("/categories", async (IAppDbContext db) =>
{
    return await db.Categories.ToListAsync();
});

app.MapPost("/transactions", async (
    CreateTransactionCommand command,
    IMediator mediator) =>
{
    var id = await mediator.Send(command);

    return Results.Ok(new { Id = id });
});

app.MapPost("/accounts", async (
    CreateAccountCommand command,
    IMediator mediator) =>
{
    var id = await mediator.Send(command);

    return Results.Ok(new { Id = id });
});

app.MapPost("/categories", async (
    CreateCategoryCommand command,
    IMediator mediator) =>
{
    var id = await mediator.Send(command);

    return Results.Ok(new { Id = id });
});

app.MapDelete("/transactions/{id}", async (
    Guid id,
    IMediator mediator) =>
{
    await mediator.Send(new DeleteTransactionCommand(id));

    return Results.Ok();
});

app.MapPut("/transactions/{id}", async (
    Guid id,
    UpdateTransactionCommand command,
    IMediator mediator) =>
{
    command.Id = id;

    await mediator.Send(command);

    return Results.Ok();
});

app.Run();