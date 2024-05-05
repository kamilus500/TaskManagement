using MediatR;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.TaskJob.Commands.Update
{
    public class UpdateCommand : UpdateTaskJobDto, IRequest
    {
    }
}
