using MediatR;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.Account.Commands.AccountRegister
{
    public class AccountRegisterCommand : RegisterUserDto, IRequest<string>
    {
    }
}
