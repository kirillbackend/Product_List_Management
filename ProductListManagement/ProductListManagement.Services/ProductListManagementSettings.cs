using ProductListManagement.Data;

namespace ProductListManagement.Service
{
    public class ProductListManagementSettings
    {
        public DbConnectionSettings ConnectionStrings { get; set; }
        public JwtSettings Auth { get; set; }

        public AdminStrings AdminStrings { get; set; }
    }

    public class JwtSettings
    {
        public string? Secret { get; set; }

        public int TokenExpireMinutes { get; set; }

        public int RefreshTokenNumber { get; set; }
    }

    public class AdminStrings()
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
