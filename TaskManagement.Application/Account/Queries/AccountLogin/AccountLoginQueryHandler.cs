using MediatR;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.Account.Queries.AccountLogin
{
    public class AccountLoginQueryHandler : IRequestHandler<AccountLoginQuery, LoginResponse>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountLoginQueryHandler(IAccountRepository accountRepository)
            => _accountRepository = accountRepository;

        public async Task<LoginResponse> Handle(AccountLoginQuery request, CancellationToken cancellationToken)
        {
            if (request.LoginDto is null)
            {
                throw new ArgumentNullException(nameof(request.LoginDto));
            }

            var token = await _accountRepository.GenerateJwt(request.LoginDto);

            return new LoginResponse
            {
                Token = token
            };
        }
    }
}
