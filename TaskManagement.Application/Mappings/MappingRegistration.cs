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
        }
    }
}
