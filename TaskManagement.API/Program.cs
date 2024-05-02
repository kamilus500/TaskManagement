using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Account.Commands.AccountRegister;
using TaskManagement.Application.Account.Queries.AccountLogin;
using TaskManagement.Application.Extensions;
using TaskManagement.Domain.Models.Dtos;
using TaskManagement.Infrastructure.Extensions;
using TaskManagement.Infrastructure.Middlewares;;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/login", async (IMediator mediator, [FromBody] LoginDto LoginDto) =>
{
    var loginResponse = await mediator.Send(new AccountLoginQuery()
    {
        LoginDto = LoginDto
    });

    return Results.Ok(loginResponse);
})
.WithName("Login")
.WithOpenApi();

app.MapPost("/register", async (IMediator mediator, [FromBody] AccountRegisterCommand registerCommand) =>
{
    var userId = await mediator.Send(registerCommand);

    return Results.Created($"/register{userId}", userId);
})
.WithName("Register")
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("applicationCors");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.Run();
