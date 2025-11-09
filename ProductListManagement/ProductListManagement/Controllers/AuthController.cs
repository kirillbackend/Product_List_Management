using Microsoft.AspNetCore.Mvc;
using ProductListManagement.Models;
using ProductListManagement.Services.Contracts;

namespace ProductListManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AbstractController
    {
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService) : base(logger)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(LoginModel loginModel)
        {
            try
            {
                Logger.LogInformation("AuthController.LogIn started");

                if (!ModelState.IsValid)
                {
                    Logger.LogWarning($"AuthController.LogInAsync failed: {ModelState}");
                    return BadRequest(ModelState);
                }

                var token = await _authService.LogInAsync(loginModel.Login, loginModel.Password);

                Logger.LogInformation("AuthController.LogInAsync completed");
                return Ok(new
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                Logger.LogWarning("AuthController.LogIn completed; invalid request");
                return BadRequest(ex);
            }
        }
    }
}
