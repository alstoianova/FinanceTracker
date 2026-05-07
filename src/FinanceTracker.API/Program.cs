using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Transactions.Commands.CreateTransaction;
using FinanceTracker.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(CreateTransactionCommand));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "FinanceTracker API works!");

app.MapGet("/transactions", async (AppDbContext db) =>
{
    return await db.Transactions.ToListAsync();
});

app.MapPost("/transactions", async (
    CreateTransactionCommand command,
    IMediator mediator) =>
{
    var id = await mediator.Send(command);

    return Results.Ok(new { Id = id });
});

app.Run();