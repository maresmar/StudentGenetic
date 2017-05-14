using StudentGenetic.Properties;
using System;
using System.Collections.Generic;
using static StudentGenetic.Gens;

namespace StudentGenetic {

    /// <summary>
    /// Satisfaction rating of student
    /// </summary>
    public class Points {
        public int Positive = 0;
        public int Negative = 0;

        // Percentage of positive points
        public int Score { get { return Positive == 0 ? 0 : (Positive * 100) / (Positive + Negative);  } }
    }

    /// <summary>
    /// Element of simulation - student
    /// </summary>
    public class Student
    {
        // Genetic information
        public Gens Gens;

        // State of mind
        public int Knowladge = 0; //0 - nothing; MAX_KNOWLADGE - everything
        public double PosibilityToLern = 1;
        public double PosibilityToRemember = 0.90;
        public Points Points;
        public bool Learned; // Tells if learn today something
        public int ToHomeOffset = 50;

        // The length of sleep
        int lastSleepDuration = Core.RECOMANDED_SLEEP_TIME;
        public int LastSleepDuration
        {
            get => lastSleepDuration;
            set {
                // If I sleep shorter than I need to sleep longer next day
                if (value < lastSleepDuration)
                    lastSleepDuration = value;
                else
                    lastSleepDuration = (int)Math.Ceiling((Decimal)(lastSleepDuration + value) / 2);
            }
        }
        public int NextSleepDuration => (Math.Abs(LastSleepDuration - Core.RECOMANDED_SLEEP_TIME) + 2*Core.RECOMANDED_SLEEP_TIME)/2;

        // Simulation parameters (related to actual day)
        public int BestKnowledge = 0; // Best possible knowledge 
        public int RemainingTime; // Remaining time to do something

        // Calendar for public event <absolute_time, event>
        Dictionary<int, Queue<LifeEvent>> eventCalendar = new Dictionary<int, Queue<LifeEvent>>();

        // Todays tasks
        Queue<LifeEvent> tasks = new Queue<LifeEvent>(10);

        // Log for printing out
        Queue<string> log;
        LifeEnvironment lifeEnviroment;

        /// <summary>
        /// Create new student with random gens
        /// </summary>
        /// <param name="e">Life environment where student lives</param>
        public Student(LifeEnvironment e) : this(e, new Gens())  {
        }

        /// <summary>
        /// Create new student with specified gens
        /// </summary>
        /// <param name="e">Life environment where student lives</param>
        /// <param name="gens">Gens of student</param>
        public Student(LifeEnvironment e, Gens gens) {
            Points = new Points();
            lifeEnviroment = e;
            log = new Queue<string>(Settings.Default.logSize + 1);
            Gens = gens;
        }

        /// <summary>
        /// Return log of readable events
        /// </summary>
        /// <returns>Log of events, each in one entry</returns>
        public String[] GetLog() {
            lock(log) {
                return log.ToArray();
            }        
        }

        /// <summary>
        /// Ads new message event to log
        /// </summary>
        /// <param name="s">Readable message</param>
        public void Log(String s) {
            lock(log) {
                log.Enqueue(s);
                if(log.Count > Settings.Default.logSize) {
                    log.Dequeue();
                }
            }
        }

        /// <summary>
        /// Rating of student - this helps to find the best students
        /// </summary>
        public int Rating => KnowladgePercentage * 2 + Points.Score;
        
        /// <summary>
        /// Returns percentage of actual knowledge of student in contrast to best knowledge
        /// </summary>
        public int KnowladgePercentage =>
            BestKnowledge != 0 ? Knowladge * 100 / BestKnowledge : 100;

        /// <summary>
        /// Plans event of live environment into student's calendar, student can throw away the event if
        /// already has some plans for that day
        /// </summary>
        /// <param name="ev">Event to be planed</param>
        /// <param name="when">Absolute day of life when the event should be planed</param>
        public void PlanEvent(LifeEvent ev, int when) {
            if (ev.Category == LifeEvent.LifeEventCategory.Freetime)
            {
                int events;
                switch (Gens.LerningPreferences)
                {
                    case LerningPreferencesEnum.Challenging:
                        events = 0;
                        break;
                    case LerningPreferencesEnum.Normal:
                        events = 1;
                        break;
                    case LerningPreferencesEnum.EasyGoing:
                        events = 2;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                if (eventCalendar.ContainsKey(when) && eventCalendar[when].Count > events)
                {
                    Log("I cannot handle as many events, I already have things to do");
                    Points.Negative += 20;
                    return;
                }
            }
            SaveEvent(ev, when);
        }

        /// <summary>
        /// Puts event into calendar
        /// </summary>
        /// <param name="ev">Event to be planed</param>
        /// <param name="when">Absolute day of life when the event should be planed</param>
        private void SaveEvent(LifeEvent ev, int when)
        {
            if (eventCalendar.ContainsKey(when))
            {
                eventCalendar[when].Enqueue(ev);
            }
            else
            {
                Queue<LifeEvent> queue = new Queue<LifeEvent>();
                queue.Enqueue(ev);
                eventCalendar.Add(when, queue);
            }
        }

        // Free time events

        /// <summary>
        /// Plans all free time events from calendar to today's tasks
        /// </summary>
        private void PlanFreetimeEvents() {
            bool wasFun = false;
            // Events from calendar
            Queue<LifeEvent> events;
            Queue<LifeEvent> elseEvents = new Queue<LifeEvent>();
            if(eventCalendar.ContainsKey(lifeEnviroment.DayOfLife)) {
                events = eventCalendar[lifeEnviroment.DayOfLife];
                eventCalendar.Remove(lifeEnviroment.DayOfLife);
            } else {
                events = new Queue<LifeEvent>();
            }
            foreach(LifeEvent ev in events) {
                if(ev.Category == LifeEvent.LifeEventCategory.Freetime) {
                    wasFun = true;
                    tasks.Enqueue(ev);
                } else  {
                    elseEvents.Enqueue(ev);
                }
            }
            if(elseEvents.Count > 0) {
                eventCalendar.Add(lifeEnviroment.DayOfLife, elseEvents);
            }
            // Everyday resting
            if (wasFun == false)
                tasks.Enqueue(new FunEvent(Gens.FreetimePreferences));
        }

        /// <summary>
        /// Plans all school related events from calendar to today's tasks
        /// </summary>
        private void DoSchoolEvents() {
            bool wasLerning = false;
            // Events from calendar
            Queue<LifeEvent> events;
            Queue<LifeEvent> elseEvents = new Queue<LifeEvent>();
            if(eventCalendar.ContainsKey(lifeEnviroment.DayOfLife)) {
                events = eventCalendar[lifeEnviroment.DayOfLife];
                eventCalendar.Remove(lifeEnviroment.DayOfLife);
            } else {
                events = new Queue<LifeEvent>();
            }
            foreach(LifeEvent ev in events) {
                if (ev.Category == LifeEvent.LifeEventCategory.School)
                {
                    tasks.Enqueue(ev);
                }
                else
                {
                    elseEvents.Enqueue(ev);
                }
            }
            if(elseEvents.Count > 0) {
                eventCalendar.Add(lifeEnviroment.DayOfLife, elseEvents);
            }
            // Every day continuous learning
            if (wasLerning == false && Gens.LearningHabits == LearningHabitsEnum.Continuous)
                tasks.Enqueue(new ContinuousLerningEvent()); ;
        }

        /// <summary>
        /// Simulate living of next day
        /// </summary>
        /// <param name="hoursInSchool">Time spend in school</param>
        public void LiveDay(int hoursInSchool) {
            Log("--- " + Core.DAYS[lifeEnviroment.DayOfLife % Core.DAYS_IN_WEEK] + " ---");
            BestKnowledge = lifeEnviroment.LastBestKnowladge;
            RemainingTime = Core.AH_IN_DAY;
            tasks.Clear();
            Learned = false;

            // Every day I forget something
            int oldKnowladge = Knowladge;
            Knowladge = (int)(Knowladge * PosibilityToRemember);
            Log($"I forget something, I know only {KnowladgePercentage} %");

            // Life Events
            PlanDay(hoursInSchool);
            while(tasks.Count > 0)
            {
                LifeEvent ev = tasks.Dequeue();
                if(ev.CanHandleIt(this))
                {
                    ev.DoIt(this);
                } else
                {
                    ev.CantDoIt(this);
                }
            }

            // Change short-term memory to long-term memory
            if(Learned && oldKnowladge != 0) {
                double knowladge = oldKnowladge;
                PosibilityToRemember = ((knowladge * PosibilityToRemember)
                    + (knowladge - (knowladge * PosibilityToRemember)) * Settings.Default.coefLongTermMemory)
                    / oldKnowladge;
            }

        }

        /// <summary>
        /// Plans simulation of next day
        /// </summary>
        /// <param name="hoursInSchool">Time spend in school</param>
        public void PlanDay(int hoursInSchool)
        {
            // Toothbrushing
            tasks.Enqueue(personalHygiene);
            // Breakfast
            tasks.Enqueue(cookingEvent);
            // School or way to home
            if (hoursInSchool != 0)
            {  // I go to school         
                tasks.Enqueue(new SchoolEvent(hoursInSchool));
            }
            else
            { 
                // I go home
                tasks.Enqueue(transportEvent);
            }

            // Lunch
            tasks.Enqueue(cookingEvent);

            // After school events
            switch (Gens.LifePriorities)
            {
                case LifePrioritiesEnum.FirstWork:
                    DoSchoolEvents();
                    PlanFreetimeEvents();
                    break;
                case LifePrioritiesEnum.FirstFun:
                    PlanFreetimeEvents();
                    DoSchoolEvents();
                    break;
                default:
                    throw new NotImplementedException();
            }

            // Some events in rest of time
            tasks.Enqueue(freetimeEvent);

            // Dinner
            tasks.Enqueue(cookingEvent);

            // Toothbrushing
            tasks.Enqueue(personalHygiene);

            // Long sleep event
            tasks.Enqueue(sleepEvent);
        }

        // Runtime speed up
        static readonly LifeEvent sleepEvent = new SleepEvent();
        static readonly LifeEvent personalHygiene = new PersonalHygieneEvent();
        static readonly LifeEvent cookingEvent = new CookingEvent();
        static readonly LifeEvent freetimeEvent = new FreetimeEvent();
        static readonly LifeEvent transportEvent = new TranspoertEvent();
    }
}
