using Mapster;
using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Account.Commands.AccountRegister
{
    public class AccountRegisterCommandHandler : IRequestHandler<AccountRegisterCommand, string>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountRegisterCommandHandler(IAccountRepository accountRepository)
            => _accountRepository = accountRepository;

        public async Task<string> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var newUser = request.Adapt<User>();

            newUser.PasswordHashed = request.Password;

            return await _accountRepository.RegisterUser(newUser, cancellationToken);
        }
    }
}
