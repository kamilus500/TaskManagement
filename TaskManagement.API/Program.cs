using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Account.Commands.AccountRegister;
using TaskManagement.Application.Account.Queries.AccountLogin;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.TaskJob.Commands.Create;
using TaskManagement.Application.TaskJob.Commands.Remove;
using TaskManagement.Application.TaskJob.Commands.Update;
using TaskManagement.Application.TaskJob.Queries.Get;
using TaskManagement.Application.TaskJob.Queries.GetById;
using TaskManagement.Domain.Models.Dtos;
using TaskManagement.Infrastructure.Extensions;
using TaskManagement.Infrastructure.Middlewares;

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

app.MapPost("/login", async (IMediator mediator, [FromBody] LoginDto LoginDto, CancellationToken cancellationToken) =>
{
    var loginResponse = await mediator.Send(new AccountLoginQuery()
    {
        LoginDto = LoginDto
    }, cancellationToken);

    return Results.Ok(loginResponse);
})
.WithName("Login")
.WithOpenApi();

app.MapPost("/register", async (IMediator mediator, [FromBody] AccountRegisterCommand registerCommand, CancellationToken cancellationToken) =>
{
    var userId = await mediator.Send(registerCommand, cancellationToken);

    return Results.Created($"/register{userId}", userId);
})
.WithName("Register")
.WithOpenApi();

app.MapPut("/update", async (IMediator mediator, [FromBody] UpdateCommand updateCommand, CancellationToken cancellationToken) =>
{
    await mediator.Send(updateCommand, cancellationToken);

    return Results.Ok();
})
.WithName("Update")
.RequireAuthorization()
.WithOpenApi();

app.MapDelete("/remove", async (IMediator mediator, [FromBody] RemoveCommand removeCommand, CancellationToken cancellationToken) =>
{
    await mediator.Send(removeCommand, cancellationToken);

    return Results.Ok();
})
.WithName("Remove")
.RequireAuthorization()
.WithOpenApi();

app.MapGet("/get/{userId}", async (IMediator mediator, [FromRoute] string userId, CancellationToken cancellationToken) =>
{
    var taskJobs = await mediator.Send(new GetQuery(userId), cancellationToken);

    return Results.Ok(taskJobs);
})
.WithName("Get")
.RequireAuthorization()
.WithOpenApi();

app.MapGet("/getbyid/{taskJobId}", async (IMediator mediator, [FromRoute] string taskJobId, CancellationToken cancellationToken) =>
{
    var taskJob = await mediator.Send(new GetByIdQuery(taskJobId), cancellationToken);

    if (taskJob is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(taskJob);
})
.WithName("GetById")
.RequireAuthorization()
.WithOpenApi();

app.MapPost("/create", async (IMediator mediator, [FromBody] CreateCommand createCommand, CancellationToken cancellationToken) =>
{
    var taskJobId = await mediator.Send(createCommand, cancellationToken);

    return Results.Created($"/create/{taskJobId}", taskJobId);
})
.WithName("Create")
.RequireAuthorization()
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("applicationCors");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.Run();
