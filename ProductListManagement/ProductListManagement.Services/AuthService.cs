using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProductListManagement.Data.Contracts;
using ProductListManagement.Service;
using ProductListManagement.Service.Contracts;
using ProductListManagement.Service.Mappers.Contracts;
using ProductListManagement.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductListManagement.Services
{
    public class AuthService : AbstractService, IAuthService
    {
        private readonly IValidationService _validationService;
        private readonly ProductListManagementSettings _settings;

        public AuthService(ILogger<AuthService> logger, IMapperFactory mapperFactory, IDataContextManager dataContextManager
            , ProductListManagementSettings settings, IValidationService validationService) 
            : base(logger, mapperFactory, dataContextManager)
        {
            _validationService = validationService;
            _settings = settings;   
        }

        public async Task<string> LogInAsync(string login, string password)
        {
            try
            {
                Logger.LogInformation("AuthService.LogInAsync started");

                await _validationService.ValidateAdminLogin(login, _settings.AdminStrings.Login);
                await _validationService.ValidateAdminPassvord(password, _settings.AdminStrings.Password);

                var claims = await CreateClaims(login);
                var token = await CreateToken(claims);

                Logger.LogInformation("AuthService.LogInAsync completed");
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Claim>> CreateClaims(string login)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            return authClaims;
        }

        public async Task<string> CreateToken(IEnumerable<Claim> authClaims)
        {
            Logger.LogInformation("AuthService.CreateToken started");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Auth.Secret));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(_settings.Auth.TokenExpireMinutes),
                    signingCredentials: signinCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            Logger.LogInformation("AuthService.CreateToken completed");
            return tokenString;
        }
    }
}
