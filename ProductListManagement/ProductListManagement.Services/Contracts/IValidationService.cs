
namespace ProductListManagement.Service.Contracts
{
    public interface IValidationService
    {
        Task ValidateAdminLogin(string login, string adminLogin);
        Task ValidateAdminPassvord(string password, string adminPassword);
    }
}
