using System.Net;
using System.Threading.Tasks;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Infrastructure.Services;
using Xunit;

namespace Application.Tests.GeoIP
{
    public class GeoIpServiceTests
    {
        [Fact]
        public void GeoIP_ReturnsCorrectDetails()
        {
            // Note: This test might fail if default database is altered.
            // Arrange
            using var service = new GeoIpService(new GeoIpSettings()
            {
                CronUpdateScheduleUtc = "0 6 ? * WED"
            }, new DateTimeService(), null);

            // Act
            var details = service.GetDetails(IPAddress.Parse("128.101.101.101"));

            // Assert
            Assert.Equal("US", details.Country.IsoCode);
            Assert.Equal("United States", details.Country.Name);
            Assert.Equal("Minnesota", details.MostSpecificSubdivision.Name);
            Assert.Equal("MN", details.MostSpecificSubdivision.IsoCode);
        }

        [Fact]
        public async Task GeoIP_WithoutLicense_DoesNotReplaceReader()
        {
            // Note: This test might fail if default database is altered.
            // Arrange
            using var service = new GeoIpService(new GeoIpSettings()
            {
                CronUpdateScheduleUtc = "0 6 ? * WED"
            }, new DateTimeService(), null);

            
            // Act
            var failed  = await service.UpdateDatabaseAsync();
            var details = service.GetDetails(IPAddress.Parse("128.101.101.101"));

            // Assert
            Assert.False(failed);
            Assert.Equal("US", details.Country.IsoCode);
            Assert.Equal("United States", details.Country.Name);
            Assert.Equal("Minnesota", details.MostSpecificSubdivision.Name);
            Assert.Equal("MN", details.MostSpecificSubdivision.IsoCode);
        }
    }
}
