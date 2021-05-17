namespace Riders.Tweakbox.API.Domain.Common
{
    public static class Constants
    {
        public static class Race
        {
            public const int MinStageNo = 0;
            public const int MaxStageNo = 20;

            public const int MinCharacterNo = 0;
            public const int MaxCharacterNo = 17;

            public const int MinGearNo = 0;
            public const int MaxGearNo = 41;
        }

        public static class User
        {
            public const int UserNameMaxLength = 32;
        }

        public static class Auth
        {
            public const int MinPasswordLength       = 6;
            public const int MaxFailedAccessAttempts = 15;
            public const int LockoutTimeSpan         = 5;
            public const int RefreshTokenExpiryDays  = 180;
        }

        public static class ServerBrowser
        {
            public const int DeletedServerPollTimeSeconds = 30;
            public const int RefreshTimeSeconds = 60;
        }

        public static class Skill
        {        
            public const double DefaultBeta                     = DefaultInitialMean / 6.0;
            public const double DefaultDrawProbability          = 0.10;
            public const double DefaultDynamicsFactor           = DefaultInitialMean / 300.0;
            public const double DefaultInitialMean              = 1000.0;
            public const double DefaultInitialStandardDeviation = DefaultInitialMean / 3.0;
        }
    }
}
