using ProductListManagement.Service;

namespace ProductListManagement
{
    public class ApiSettings : ProductListManagementSettings
    {
        public string[] AllowedOrigins { get; set; }
    }
}
