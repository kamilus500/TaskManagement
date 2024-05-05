using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Models.Dtos;
namespace TaskManagement.Application.TaskJob.Commands.Remove
{
    public class RemoveCommand : RemoveTaskJobDto, IRequest
    {
    }
}
