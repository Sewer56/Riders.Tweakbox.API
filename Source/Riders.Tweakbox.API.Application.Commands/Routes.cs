namespace Riders.Tweakbox.API.Application.Commands
{
    public static class Routes
    {
        public const string RestGetAll = "";
        public const string RestGet    = "{id}";
        public const string RestUpdate = "{id}";
        public const string RestCreate = "";
        public const string RestDelete = "{id}";

        public static class Match
        {
            public const string Base = "v1/Match";
        }

        public static class Identity
        {
            public const string Register = "v1/Identity/register";
            public const string Login    = "v1/Identity/login";
            public const string Refresh  = "v1/Identity/refresh";
        }

        public static class Browser
        {
            public const string Base = "v1/Browser";
        }

        public static string ToRestGetAll(this string basePath) => basePath + RestGetAll;
        public static string ToRestGet(this string basePath) => basePath + RestGet;
        public static string ToRestUpdate(this string basePath) => basePath + RestUpdate;
        public static string ToRestCreate(this string basePath) => basePath + RestCreate;
        public static string ToRestDelete(this string basePath) => basePath + RestDelete;
    }
}
