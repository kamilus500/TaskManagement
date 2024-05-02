using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Models.Configs;
using TaskManagement.Domain.Models.Dtos;
using TaskManagement.Infrastructure.Persistance;
using TaskManagement.Infrastructure.Utils;

namespace TaskManagement.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountRepository(ApplicationDbContext dbContext, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = PasswordHasher.Validate(user.PasswordHashed, dto.Password);
            if (!result)
            {
                throw new ArgumentException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Login", user.Login.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.Secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.TokenExpiryTimeInHour);

            var token = new JwtSecurityToken(_authenticationSettings.ValidIssuer,
                _authenticationSettings.ValidIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> RegisterUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();

            var hashedPassword = PasswordHasher.Hash(user.PasswordHashed);

            user.PasswordHashed = hashedPassword;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
