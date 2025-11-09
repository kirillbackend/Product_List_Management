using ProductListManagement.Service.Contracts;

namespace ProductListManagement.Service
{
    public class ValidationService : IValidationService
    {
        public async Task ValidateAdminLogin(string login, string adminLogin)
        {
            if (login != adminLogin) throw new ArgumentException("Wrong login");
        }

        public async Task ValidateAdminPassvord(string password, string adminPassword)
        {
            if (password != adminPassword) throw new ArgumentException("Wrong password");
        }
    }
}
