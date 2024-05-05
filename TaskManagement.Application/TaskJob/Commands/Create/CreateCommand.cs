using MediatR;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.TaskJob.Commands.Create
{
    public class CreateCommand: CreateTaskJobDto, IRequest<string>
    {
    }
}
