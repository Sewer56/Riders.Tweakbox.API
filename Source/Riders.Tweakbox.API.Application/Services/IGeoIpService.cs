using System.Net;
using MaxMind.GeoIP2.Responses;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IGeoIpService
    {
        /// <summary>
        /// Gets details for a given IP address.
        /// </summary>
        /// <param name="address">The IP Address</param>
        /// <returns>Null if not found, otherwise city details.</returns>
        public CityResponse GetDetails(IPAddress address);
    }
}