using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGenetic {

    /// <summary>
    /// Basic constants about time etc
    /// </summary>
    class Core {
        public static Random rn = new Random();

        public static readonly int AH_IN_DAY = 240 / 15; // = 16
        public static readonly int DAYS_IN_WEEK = 7;
        public static readonly int WEEKDAYS_IN_WEEK = 5;
        public static readonly int WEEKS_IN_SEMESTER = 11;
        public static readonly int SEMESTERS_IN_LIFE = 5;

        public static readonly double KNOWLADGE_PER_ACADEMIC_HOUR = 100;

        public static readonly String[] DAYS = new String[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        public static readonly int COOK_TIME = 2;
        public static readonly int PERSONAL_HIGIENE_TIME = 1;
        public static readonly int MAX_SLEEP_TIME = 7;
        public static readonly int RECOMANDED_SLEEP_TIME = 4;
    }
}
