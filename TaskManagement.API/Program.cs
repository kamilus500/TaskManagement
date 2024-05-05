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

app.MapPut("/update", async (IMediator mediator, [FromBody] UpdateCommand updateCommand) =>
{
    await mediator.Send(updateCommand);

    return Results.Ok();
})
.WithName("Update")
.WithOpenApi();

app.MapDelete("/remove", async (IMediator mediator, [FromBody] RemoveCommand removeCommand) =>
{
    await mediator.Send(removeCommand);

    return Results.Ok();
})
.WithName("Remove")
.WithOpenApi();

app.MapGet("/get/{userId}", async (IMediator mediator, [FromRoute] string userId) =>
{
    var taskJobs = await mediator.Send(new GetQuery(userId));

    return Results.Ok(taskJobs);
})
.WithName("Get")
.WithOpenApi();

app.MapGet("/getbyid/{taskJobId}", async (IMediator mediator, [FromRoute] string taskJobId) =>
{
    var taskJob = await mediator.Send(new GetByIdQuery(taskJobId));

    if (taskJob is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(taskJob);
})
.WithName("GetById")
.WithOpenApi();

app.MapPost("/create", async (IMediator mediator, [FromBody] CreateCommand createCommand) =>
{
    var taskJobId = await mediator.Send(createCommand);

    return Results.Created($"/create/{taskJobId}", taskJobId);
})
.WithName("Create")
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("applicationCors");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.Run();
