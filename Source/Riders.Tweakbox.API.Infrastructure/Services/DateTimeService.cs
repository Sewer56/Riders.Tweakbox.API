using System;
using Riders.Tweakbox.API.Application.Services;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        /// <inheritdoc />
        public DateTime GetCurrentDateTime() => DateTime.UtcNow;
    }
}
