using System;
using System.Diagnostics;
using static StudentGenetic.DateOverflowEventArgs;

namespace StudentGenetic
{
    /// <summary>
    /// Simulation calendar date (day, week, semester, generation), fix overflow of dates and 
    /// provides events on end of interesting date periods.
    /// </summary>
    class SimulationCalendar
    {
        // Internal date representation
        uint dayOfWeek = 0;
        uint week = 0;
        uint semeseter = 0;
        uint generation = 0;

        /// <summary>
        /// An action that will be executed every day - inside 
        /// <c>Next...</c> function.
        /// </summary>
        public event Action EveryDayAction;
        /// <summary>
        /// An event that clients can use to be notified whenever the
        /// date overflow is reached
        /// </summary>
        public event EventHandler<DateOverflowEventArgs> DateOverflowEvent;

        /// <summary>
        /// Creates new calendar with "default date" (fist day, fist week, first generation)
        /// </summary>
        /// <param name="everyDayAction">Action that will be executed every day - inside 
        /// <c>Next...</c> function. </param>
        public SimulationCalendar(Action everyDayAction) {
            Debug.Assert(everyDayAction != null);
            EveryDayAction = everyDayAction;
        }

        /// <summary>
        /// Provides access for calendar data. Event is only performed if actual day has overflow eg. if day 
        /// is set to 7 (and only once)
        /// </summary>
        public uint DayOfWeek
        {
            get
            {
                return dayOfWeek;
            }
            set
            {
                dayOfWeek = value;
                CheckDayOverflow();
            }
        }

        /// <summary>
        /// Provides access for calendar data. No overflow control is performed.
        /// </summary>
        public uint Week
        {
            get
            {
                return week;
            }
            set
            {
                Debug.Assert(value < Core.WEEKS_IN_SEMESTER);
                week = value;
            }
        }

        /// <summary>
        /// Provides access for calendar data. No overflow control is performed.
        /// </summary>
        public uint Semester
        {
            get
            {
                return semeseter;
            }
            set
            {
                Debug.Assert(value < Core.SEMESTERS_IN_LIFE);
                semeseter = value;
            }
        }

        /// <summary>
        /// Provides access for calendar data. No overflow control is performed.
        /// </summary>
        public uint Generation
        {
            get
            {
                return generation;
            }
            set
            {
                generation = value;
            }
        }

        /// <summary>
        /// Performs events calling. The <c>EventArgs.Empty</c> is used as <c>EventArgs</c>.
        /// </summary>
        /// <param name="type"></param>
        protected virtual void OnEvent(OverflowType type)
        {
            DateOverflowEventArgs args = new DateOverflowEventArgs();
            args.Type = type;
            // Event is only performed if any listener is registered
            if (DateOverflowEvent != null)
                DateOverflowEvent(this, args);
        }

        /// <summary>
        /// Fix day overflow end performs events on the end of important periods
        /// eg. 8. dayOfWeek 1. week is changed to 1. day of 2. week 
        /// and EndOfWeekEvent is performed. Please note: only one overflow is always performed 
        /// eg. <c>dayOfTheWeek = 50</c> -> <c>dayOfWeek = 0</c> and <c>week++</c>
        /// </summary>
        protected void CheckDayOverflow()
        {
            if (dayOfWeek == Core.DAYS_IN_WEEK)
            {
                dayOfWeek = 0;
                week++;
                OnEvent(OverflowType.EndOfWeek);
            }
            else
                return;
            if (week == Core.WEEKS_IN_SEMESTER)
            {
                week = 0;
                semeseter++;
                OnEvent(OverflowType.EndOfSemester);
            }
            else
                return;
            if (semeseter == Core.SEMESTERS_IN_LIFE)
            {
                semeseter = 0;
                generation++;
                OnEvent(OverflowType.EndOfGeneration);
            }
            else
                return;
        }

        /// <summary>
        /// Moves calender to next day and performs <c>EveryDayAction</c>.
        /// </summary>
        public void NextDay()
        {
            EveryDayAction();
            dayOfWeek++;
            CheckDayOverflow();
        }

    }

    /// <summary>
    /// Handles arguments in DateOverflowEvent
    /// </summary>
    public class DateOverflowEventArgs : EventArgs
    {
        // Calendar date overflow event types
        public enum OverflowType
        {
            EndOfDay, EndOfWeek, EndOfSemester, EndOfGeneration
        }

        /// <summary>
        /// Type of date overflow
        /// </summary>
        public OverflowType Type { get; set; }
    }
}
