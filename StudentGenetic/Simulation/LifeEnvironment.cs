using StudentGenetic.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static StudentGenetic.Gens;

namespace StudentGenetic {

    /// <summary>
    /// Environment where simulation is proceeded
    /// </summary>
    public class LifeEnvironment {
        /// <summary>
        /// Type of events in LifeEnviroment
        /// </summary>
        public enum EnviroEventType
        {
            HomeWork,
            Test,
            PublicEvent
        }

        /// <summary>
        /// Students in simulation
        /// </summary>
        Student[] students;
        public int LastBestKnowladge {
            get;
            private set;
        }

        /// <summary>
        /// Number of days between actual date and start of generation
        /// </summary>
        public int DayOfLife {
            get;
            private set;
        }

        /// <summary>
        /// Number of academic hours in days of week
        /// </summary>
        public int[] Timetable {
            get;
            private set;
        }

        /// <summary>
        /// Function where logs are posted
        /// </summary>
        public EventHandler<LogEventArgs> LogEvent;

        /// <summary>
        /// Create new environment with random timetable 
        /// </summary>
        /// <param name="numOfStudents">Number of students in simulation</param>
        public LifeEnvironment(int numOfStudents) {
            // Create event log
            RestartLife();

            // Creates new students
            students = new Student[numOfStudents];
            for (int i = 0; i < numOfStudents; i++) {
                students[i] = new Student(this);
            }
        }

        /// <summary>
        /// Resets simulation variables to their default and recreate timetable
        /// </summary>
        public void RestartLife() {
            MakeTimetable();
            LastBestKnowladge = 0;
            DayOfLife = 0;
        }

        /// <summary>
        /// Randomly make new timetable with respect to settings
        /// </summary>
        private void MakeTimetable() {
            Timetable = new int[Core.DAYS_IN_WEEK];

            // Limit hours in week to settings value <-1,+2>
            int numHoursRemaining = Settings.Default.timetableHoursInWeek - 1 + Core.rn.Next(3);
            int maxEnding = Core.WEEKDAYS_IN_WEEK * Settings.Default.timetableMaxHoursInDay;

            int i;
            for (i = 0; i < Core.WEEKDAYS_IN_WEEK; i++) {
                int minNum = numHoursRemaining - maxEnding;
                minNum = minNum < 0 ? 0 : minNum;
                int num = minNum + Core.rn.Next(Settings.Default.timetableMaxHoursInDay - minNum + 1);

                Timetable[i] = num;
                numHoursRemaining -= num;

                maxEnding -= 5;
            }
            for (; i < Core.DAYS_IN_WEEK; i++) {
                Timetable[i] = 0;
            }
            Log("Timetable created");
        }

        /// <summary>
        /// Sends log message to registered event receiver.
        /// </summary>
        /// <param name="msg">Log message</param>
        protected void Log(String msg)
        {
            if (LogEvent != null)
                LogEvent(this, new LogEventArgs(msg));
        }

        /// <summary>
        /// Generates new students using genetic algorithms. 
        /// See http://cgg.mff.cuni.cz/~pepca/prg022/luner.html for more details.
        /// </summary>
        public void DoReproduction() {
            // Sorts students using bubble-sort with respect to rating
            int i;
            int[] ids = new int[students.Length];
            for (int j = 0; j < students.Length; j++) {
                ids[j] = j;
            }
            bool swaped = true;
            while (swaped) {
                swaped = false;
                for (i = 0; i < students.Length - 1; i++) {
                    if (students[ids[i]].Rating < students[ids[i + 1]].Rating) {
                        int temp = ids[i];
                        ids[i] = ids[i + 1];
                        ids[i + 1] = temp;
                        swaped = true;
                    }
                }
            }

            // First 1/3 mix together
            for (i = 0; i < students.Length * 2 / 3; i += 2) {
                // I select two students
                Log("Students " + ids[i] + " and " + ids[i + 1] + "has two children");
                Gens[] gens = Gens.MixGens(students[ids[i]].Gens, students[ids[i + 1]].Gens);
                students[ids[i]] = new Student(this, gens[0]);
                students[ids[i + 1]] = new Student(this, gens[1]);
            }
            // The rest (2/3) is recreated randomly
            for (; i < students.Length; i++) {
                students[ids[i]] = new Student(this);
            }
            Log("New students created");
        }

        /// <summary>
        /// Creates random new LifeEvent
        /// </summary>
        /// <returns>Stack of LifeEvent for Environment</returns>
        private Stack<EnviroEvent> MakeEvents() {
            Stack<EnviroEvent> result = new Stack<EnviroEvent>();
            // There is 30 % probability of new event
            int val = Core.rn.Next(10);
            if (val >= 3) {
                return result;
            } else {
                // I will create 3 events maximally
                while (val < 3) {
                    EnviroEventType type = (EnviroEventType)val;
                    int when = DayOfLife + Core.rn.Next(7) + Core.rn.Next(7);
                    EnviroEvent ev;
                    switch (type) {
                        case EnviroEventType.HomeWork:
                            ev = new HomeworkEnviroEvent(when);
                            Log($"Homework in {when-DayOfLife} days");
                            break;
                        case EnviroEventType.Test:
                            ev = new TestEnviroEvent(when, LastBestKnowladge);
                            Log($"Test in {when - DayOfLife} days");
                            break;
                        case EnviroEventType.PublicEvent:
                            ev = new PublicEnviroEvent(when);
                            Log($"Public free-time event in {when - DayOfLife} days");
                            break;
                        default:
                            throw new NotImplementedException("Unknown EviroEvent type");
                    }
                    result.Push(ev);
                    val = Core.rn.Next(9);
                }
                return result;
            }
        }

        /// <summary>
        /// Does simulation of one standard day
        /// </summary>
        public void SimulateDay() {
            Log("--- " + Core.DAYS[DayOfLife % Core.DAYS_IN_WEEK] + " ---");
            Stack<EnviroEvent> events = MakeEvents();
            Parallel.ForEach(students, student => {
                // Sends new events to students
                foreach (EnviroEvent ev in events) {
                    ev.Plan(student);
                }
                // Live day of student
                student.LiveDay(Timetable[DayOfLife % Core.DAYS_IN_WEEK]);
            });
            LastBestKnowladge += (int)Core.KNOWLADGE_PER_ACADEMIC_HOUR * Timetable[DayOfLife % Core.DAYS_IN_WEEK];
            DayOfLife++;
        }

        /// <summary>
        /// Proceed examination on the end of semester
        /// </summary>
        public void SimulateExams()
        {
            Log("--- Examination period started ---");
            EnviroEvent le = new TestEnviroEvent(DayOfLife + Core.DAYS_IN_WEEK, LastBestKnowladge);
            foreach (Student student in students)
            {                
                int backup = student.Gens.LengthOfPlanning;
                student.Gens.LengthOfPlanning = student.Gens.LengthOfPlanning * 2 > Core.DAYS_IN_WEEK ? Core.DAYS_IN_WEEK : student.Gens.LengthOfPlanning * 2;
                le.Plan(student);
                student.Gens.LengthOfPlanning = backup;
                student.LiveDay(0);
            }
            for (int i = 0; i < Core.DAYS_IN_WEEK; i++)
            {
                Log("--- " + Core.DAYS[DayOfLife % Core.DAYS_IN_WEEK] + " ---");
                foreach (Student student in students)
                {
                    student.LiveDay(0);
                }
                DayOfLife++;
            }
        }

        /// <summary>
        /// Returns student with specified ID
        /// </summary>
        /// <param name="id">ID of student</param>
        /// <returns>Student with specified ID</returns>
        public Student GetStudent(int id)
        {
            return students[id];
        }

        /// <summary>
        /// Finds student with highest rating
        /// </summary>
        /// <returns>ID of fist student with highest score</returns>
        public int FindBestStudentId()
        {
            int maxPoints = 0;
            int maxId = 0;
            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].Rating > maxPoints)
                {
                    maxPoints = students[i].Rating;
                    maxId = i;
                }
            }
            return maxId;
        }
    }

    /// <summary>
    /// Handles arguments in LogEvent
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Log message
        /// </summary>
        public String What { get; set; }

        /// <summary>
        /// Creates new object with defined initial data
        /// </summary>
        /// <param name="what">Log message</param>
        public LogEventArgs(String what)
        {
            What = what;
        }
    }
}