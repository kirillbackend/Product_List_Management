
namespace ProductListManagement.Services.Contracts
{
    public interface IAuthService
    {
        Task<string> LogInAsync(string login, string password);
    }
}
