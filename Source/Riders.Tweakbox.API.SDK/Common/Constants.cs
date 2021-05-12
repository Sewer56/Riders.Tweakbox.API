using System.Text.Json;
using Refit;

namespace Riders.Tweakbox.API.SDK.Common
{
    public static class RefitConstants
    {
        public const string BearerAuthentication = "Authorization: Bearer";
        public static readonly JsonSerializerOptions SerializerOptions;
        public static readonly SystemTextJsonContentSerializer ContentSerializer;

        static RefitConstants()
        {
            SerializerOptions = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                WriteIndented = false,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            ContentSerializer = new SystemTextJsonContentSerializer(SerializerOptions);
        }
    }
}
