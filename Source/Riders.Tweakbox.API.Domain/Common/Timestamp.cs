using System;

namespace Riders.Tweakbox.API.Domain.Common
{
    /// <summary>
    /// Represents an individual timestamp.
    /// </summary>
    public struct Timestamp
    {
        /// <summary>
        /// The actual value of the timestamp.
        /// </summary>
        public DateTime Time;
        
        public Timestamp(DateTime timeStamp) => Time = timeStamp;

        /// <summary>
        /// Refreshes the timestamp to current time.
        /// </summary>
        public void Refresh() => Time = DateTime.UtcNow;

        /// <summary>
        /// Sets the time to the unix epoch time such that it's discarded for being too old.
        /// </summary>
        public void Discard() => Time = DateTime.UnixEpoch;

        /// <summary>
        /// Checks if an item should be discarded based on comparing the saved and current time.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public bool IsDiscard(TimeSpan timeout) => DateTime.UtcNow > Time.Add(timeout);
    }
}
