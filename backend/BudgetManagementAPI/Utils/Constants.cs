namespace BudgetManagementAPI.Utils
{
    public static class Constants
    {
        public const string DbConnectionString = "BudgetManagementDb";

        public static class Files
        {
            private static readonly string? Storage =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "BudgetManagementApi"
                    );

            public static readonly string ProfilePictures = $@"{Storage}\ProfilePictures";
        }

        public static class Token
        {
            public const string UserIdClaim = "id";

            public const string JwtKey = "Jwt:Key";
            public const string JwtIssuer = "Jwt:Issuer";
            public const string JwtAudience = "Jwt:Audience";

            public const int OtpDigits = 4;

            public static readonly TimeSpan TokenLife = TimeSpan.FromDays(1);
            public static readonly TimeSpan RefreshTokenLife = TimeSpan.FromDays(10);
            public static readonly TimeSpan OtpLife = TimeSpan.FromMinutes(5);
        }
    }
}
