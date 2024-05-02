using MediatR;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.Account.Queries.AccountLogin
{
    public class AccountLoginQuery : IRequest<LoginResponse>
    {
        public LoginDto LoginDto { get; set; }
    }
}
