using System;
using System.Threading;
using Cronos;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Infrastructure.Services;

namespace Riders.Tweakbox.API.Infrastructure.Helpers
{
    /// <summary>
    /// Helper class that executes a task at a specified Cron interval.
    /// </summary>
    public class CronJobRunner : IDisposable
    {
        /// <summary>
        /// The schedule.
        /// The new schedule is applied the next time the event is fired.
        /// </summary>
        public string Schedule { get; set; }
        
        /// <summary>
        /// The action to executed.
        /// </summary>
        public Action Action   { get; set; }

        /// <summary>
        /// Stores the date/time of when the event is going to be fired.
        /// </summary>
        public DateTime? NextTime { get; private set; }

        /// <summary>
        /// Stores the time between the last firing of the event and the next time it will be fired.
        /// </summary>
        public TimeSpan? UntilNext { get; private set; }

        /// <summary>
        /// True if a task is scheduled, else false.
        /// </summary>
        public bool IsScheduled { get; private set; }

        private Timer _timer;
        private IDateTimeService _dateTimeService;

        private CronJobRunner() {}
        public CronJobRunner(string schedule, Action action, IDateTimeService dateTimeService = null)
        {
            Schedule = schedule;
            Action   = action;
            _dateTimeService = dateTimeService ?? new DateTimeService();
            _timer = new Timer(RunAction);
            ScheduleNext();
        }


        /// <inheritdoc />
        public void Dispose() => _timer?.Dispose();

        /// <summary>
        /// Forcefully fires the event ahead of schedule.
        /// </summary>
        /// <param name="scheduleNext">Automatically schedules the next event.</param>
        public void ManualFireEvent(bool scheduleNext = true)
        {
            Action();
            IsScheduled = false;

            if (scheduleNext)
            {
                ScheduleNext();
            }
            else
            {
                NextTime  = null;
                UntilNext = null;
            }
        }

        private void ScheduleNext()
        {
            NextTime = GetNextJobTime();
            UntilNext = NextTime - _dateTimeService.GetCurrentDateTime();
            _timer.Change(UntilNext.Value, Timeout.InfiniteTimeSpan);
            IsScheduled = true;
        }

        private DateTime GetNextJobTime()
        {
            var expression = CronExpression.Parse(Schedule);
            return expression.GetNextOccurrence(_dateTimeService.GetCurrentDateTime()).Value;
        }

        private void RunAction(object? state) => ManualFireEvent();
    }
}
