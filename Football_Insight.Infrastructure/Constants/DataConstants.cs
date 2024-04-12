namespace Football_Insight.Infrastructure.Constants
{
    public static class DataConstants
    {
        public const int PersonFirstNameMaxLength = 50;
        public const int PersonFirstNameMinLength = 2;

        public const int PersonLastNameMaxLength = 50;
        public const int PersonLastNameMinLength = 2;

        public const int LeagueNameMaxLength = 25;
        public const int LeagueNameMinLength = 2;

        public const int TeamNameMaxLength = 50;
        public const int TeamNameMinLength = 5;

        public const int StadiumNameMaxLength = 50;
        public const int StadiumNameMinLength = 3;

        public const int StadiumAddressMaxLength = 96;
        public const int StadiumAddressMinLength = 10;

        public const int StadiumMinRange = 1;
        public const int StadiumMaxRange = 150000;

        public const int TotalDigitsDecimalPrecision = 14;
        public const int DigitsAfterDecimalPoint = 2;

        public const int CoachNameMaxLength = 100;
        public const int CoachNameMinLength = 2;

        public const int YearFoundMax = 2024;
        public const int YearFoundMin = 1700;

        public static readonly string UserGUID = Guid.NewGuid().ToString();
        public static readonly string AdminGUID = Guid.NewGuid().ToString();
        public static readonly string RoleAdminGUID = Guid.NewGuid().ToString();
        public static readonly string RoleUserGUID = Guid.NewGuid().ToString();
    }
}
