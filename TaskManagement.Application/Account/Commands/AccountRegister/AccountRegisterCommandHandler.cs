using Mapster;
using MapsterMapper;
using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Account.Commands.AccountRegister
{
    public class AccountRegisterCommandHandler : IRequestHandler<AccountRegisterCommand, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountRegisterCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var newUser = request.Adapt<User>();

            newUser.PasswordHashed = request.Password;

            return await _accountRepository.RegisterUser(newUser);
        }
    }
}
