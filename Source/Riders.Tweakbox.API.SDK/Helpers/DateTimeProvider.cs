using System;

namespace Riders.Tweakbox.API.SDK.Helpers
{
    /// <summary>
    /// Provides access to the current date or time.
    /// </summary>
    public class DateTimeProvider
    {
        public virtual DateTime GetCurrentDateTimeUtc() => DateTime.UtcNow;
    }
}
