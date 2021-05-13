using System.Threading.Tasks;
using Integration.Tests.Common;
using Xunit;

namespace Integration.Tests
{
    public class AuthenticationHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToken_RefreshedOnExpiry()
        {
            // Arrange
            await this.AuthenticateAsync();

            // Act


            // Assert


        }
    }
}
