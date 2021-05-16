using Riders.Tweakbox.API.Infrastructure.Helpers;
using Riders.Tweakbox.API.Infrastructure.Services;
using Xunit;

namespace Application.Tests.Helpers
{
    public class CronJobTests
    {
        [Fact]
        public void CronJob_ShouldScheduleNextJob_WhenScheduleRequested()
        {
            // Arrange
            bool hasExecuted = false;
            using var runner = new CronJobRunner("*/5 * * * *", () => hasExecuted = true, new DateTimeService());

            // Act
            Assert.True(runner.IsScheduled);
            runner.ManualFireEvent();

            // Assert
            Assert.True(hasExecuted);
            Assert.True(runner.IsScheduled);
            Assert.True(runner.NextTime.HasValue);
            Assert.True(runner.UntilNext.HasValue);
        }

        [Fact]
        public void CronJob_ShouldNotScheduleNextJob_WhenScheduleNotRequested()
        {
            // Arrange
            bool hasExecuted = false;
            using var runner = new CronJobRunner("*/5 * * * *", () => hasExecuted = true, new DateTimeService());

            // Act
            Assert.True(runner.IsScheduled);
            runner.ManualFireEvent(false);

            // Assert
            Assert.True(hasExecuted);
            Assert.False(runner.IsScheduled);
            Assert.False(runner.NextTime.HasValue);
            Assert.False(runner.UntilNext.HasValue);
        }
    }
}
