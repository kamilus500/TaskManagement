using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> RegisterUser(User newUser, CancellationToken cancellationToken);

        Task<string> GenerateJwt(LoginDto dto, CancellationToken cancellationToken);
    }
}
