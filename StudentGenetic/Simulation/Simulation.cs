using System;

namespace StudentGenetic
{
    /// <summary>
    /// Simulation object - sets SimulationCalendar to work with LifeEnviroment and 
    /// provides API for simulation management
    /// </summary>
    class Simulation
    {
        enum FulfilledStops { Unknown, Stop, Continue };

        // Public simulation members
        public SimulationCalendar Calendar;
        public LifeEnvironment LifeEnv;
        /// <summary>
        /// Percentage of excepted amount of known informations of best student when the simulation 
        /// is ended. If is equal to zero then simulation never stop automatically.
        /// </summary>
        public int ExceptedKnowlidge { get; set; }
        /// <summary>
        /// Excepted percentage of positive points of best student when the simulation is ended. If is equal to zero
        /// then simulation never stop automatically.
        /// </summary>
        public int ExceptedScore { get; set; }
        /// <summary>
        /// Performs event on end of simulation
        /// </summary>
        public event EventHandler SimulationFinishedEvent;

        /// <summary>
        /// Creates new simulation with specified arguments
        /// </summary>
        /// <param name="numberOfStudents">Number of students in simulation</param>
        public Simulation(int numberOfStudents) : this(numberOfStudents, 0, 0)
        {
        }

        /// <summary>
        /// Creates new simulation with specified arguments
        /// </summary>
        /// <param name="numberOfStudents">Number of students in simulation</param>
        /// <param name="exceptedKnowlidge">Percentage of excepted amount of known informations of best 
        /// student when the simulation is ended.<see cref="ExceptedKnowlidge"/></param>
        /// <param name="exceptedPoints">Excepted amount of points of best student when the simulation is 
        /// ended.<see cref="ExceptedScore"/></param>
        public Simulation(int numberOfStudents, int exceptedKnowlidge, int exceptedPoints)
        {
            ExceptedKnowlidge = exceptedKnowlidge;
            ExceptedScore = exceptedPoints;
            // Simulation environment
            LifeEnv = new LifeEnvironment(numberOfStudents);

            // Simulation calendar
            Calendar = new SimulationCalendar(LifeEnv.SimulateDay);
            Calendar.DateOverflowEvent += OnDateOverflow;
        }

        /// <summary>
        /// Moves internal <c>Calendar</c> to <c>NextDay</c>
        /// </summary>
        public void NextDay()
        {
            Calendar.NextDay();
        }

        /// <summary>
        /// Event that will performed on the end of simulation
        /// </summary>
        public void OnEndOfGeneration()
        {
            Student s = LifeEnv.GetStudent(LifeEnv.FindBestStudentId());
            FulfilledStops stopState = FulfilledStops.Unknown;
            // If there is any knowledge threshold
            if (ExceptedKnowlidge > 0)
            {
                if (s.KnowladgePercentage >= ExceptedKnowlidge)
                {
                    stopState = FulfilledStops.Stop;
                }
                else
                {
                    stopState = FulfilledStops.Continue;
                }
            }
            // If there is any point threshold
            if (ExceptedScore > 0)
            {
                if (s.Points.Score >= ExceptedScore)
                {
                    if (stopState != FulfilledStops.Continue) // It is stop or unknown
                        stopState = FulfilledStops.Stop; // So you can stop
                }
                else
                {
                    stopState = FulfilledStops.Continue;
                }
            }
            if (stopState == FulfilledStops.Stop)
            {
                OnFinished(EventArgs.Empty);
                return;
            }
            LifeEnv.DoReproduction();
            LifeEnv.RestartLife();
        }

        /// <summary>
        /// Event that is performed if there is any date overflow in calendar
        /// </summary>
        /// <param name="sender">Not needed</param>
        /// <param name="args">Date overflow type eg. EndOfWeek</param>
        protected void OnDateOverflow(object sender, DateOverflowEventArgs args)
        {
            switch (args.Type)
            {
                case DateOverflowEventArgs.OverflowType.EndOfSemester:
                    LifeEnv.SimulateExams();
                    break;
                case DateOverflowEventArgs.OverflowType.EndOfGeneration:
                    OnEndOfGeneration();
                    break;
            }
        }

        /// <summary>
        /// Sends event that simulation is finished
        /// </summary>
        /// <param name="e">Arguments of event (not needed)</param>
        protected void OnFinished(EventArgs e)
        {
            if(SimulationFinishedEvent != null)
            {
                SimulationFinishedEvent(this, e);
            }
        }

    }
}
