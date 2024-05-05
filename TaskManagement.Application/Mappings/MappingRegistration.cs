using Mapster;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.Mappings
{
    public class MappingRegistration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterUserDto, User>();

            config.NewConfig<TaskJobDto, Domain.Entities.TaskJob>();

            config.NewConfig<CreateTaskJobDto, Domain.Entities.TaskJob>();

            config.NewConfig<RemoveTaskJobDto, Domain.Entities.TaskJob>();

            config.NewConfig<UpdateTaskJobDto, Domain.Entities.TaskJob>();
        }
    }
}
