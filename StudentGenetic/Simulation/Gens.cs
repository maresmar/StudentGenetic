using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGenetic
{
    /// <summary>
    /// Stores people characteristic in vector and handles reproduction and codding
    /// </summary>
    public class Gens
    {
        public static readonly int[] GENS_MAXS = { 3, 3, 2, 2, 4, 3, Core.DAYS_IN_WEEK - 1 };
        public static readonly int GENS_LENGTH = 7;

        public enum FoodHabitsEnum
        {
            Cooking,
            Restaurant,
            FromHome
        }

        public enum LearningHabitsEnum
        {
            Continuous,
            OnLastMomnet
        }

        public enum SchoolAtentionEnum
        {
            Active,
            Pasive,
            No
        }

        public enum LifePrioritiesEnum
        {
            FirstWork, FirstFun
        }

        public enum LerningPreferencesEnum
        {
            Challenging,
            Normal,
            EasyGoing
        }

        public enum FreetimePreferencesEnum
        {
            Friends,
            Sport,
            Nothing,
            Reading
        }

        // Student's characteristics
        public SchoolAtentionEnum SchoolAtention;
        public FoodHabitsEnum FoodHabits;
        public LearningHabitsEnum LearningHabits;
        public LifePrioritiesEnum LifePriorities;
        public FreetimePreferencesEnum FreetimePreferences;
        public LerningPreferencesEnum LerningPreferences;
        public int LengthOfPlanning = 3;

        /// <summary>
        /// Creates new random Gens
        /// </summary>
        public Gens()
        {
            int[] dna = new int[GENS_LENGTH];
            for (int i = 0; i < GENS_LENGTH; i++)
            {
                dna[i] = Core.rn.Next(GENS_MAXS[i]);
            }
            SetCode(dna);
        }

        /// <summary>
        /// Create new Gens from vector of characteristic
        /// </summary>
        /// <param name="code">Encoded characteristic</param>
        public Gens(int[] code)
        {
            SetCode(code);
        }

        /// <summary>
        /// Generates new Gens for genetic algorithm
        /// See http://cgg.mff.cuni.cz/~pepca/prg022/luner.html for more details.
        /// </summary>
        /// <param name="parent1">Mather's gens</param>
        /// <param name="parent2">Father's gens</param>
        /// <returns>Two element array of gens (two children)</returns>
        static public Gens[] MixGens(Gens parent1, Gens parent2)
        {
            Gens[] result = new Gens[2];
            int[] dna1, dna2;
            dna1 = parent1.GetCode();
            dna2 = parent2.GetCode();
            bool translate;
            if (Core.rn.Next(100) < 95)
            {
                translate = true;
            }
            else
            {
                translate = false;
            }
            for (int j = 0; j < GENS_LENGTH; j++)
            {
                // Translation
                if (translate && Core.rn.Next(2) == 0)
                {
                    int temp = dna1[j];
                    dna1[j] = dna2[j];
                    dna2[j] = temp;
                }
                // Random mutation
                if (Core.rn.Next(100) < 10)
                {
                    dna1[j] = Core.rn.Next(GENS_MAXS[j]);
                }
                if (Core.rn.Next(100) < 10)
                {
                    dna2[j] = Core.rn.Next(GENS_MAXS[j]);
                }
            }
            result[0] = new Gens(dna1);
            result[1] = new Gens(dna2);
            return result;
        }

        /// <summary>
        /// Gets internal encoded characteristic
        /// </summary>
        /// <returns>Encoded characteristic</returns>
        public int[] GetCode()
        {
            int[] result = new int[] { (int)SchoolAtention, (int)FoodHabits, (int)LearningHabits, (int)LifePriorities, (int)FreetimePreferences, (int)LerningPreferences, (int)LengthOfPlanning - 1 };
            Debug.Assert(result.Length == GENS_LENGTH);
            return result;
        }

        /// <summary>
        /// Sets internal encoded characteristic
        /// </summary>
        /// <param name="gens">Encoded characteristic</param>
        private void SetCode(int[] gens)
        {
            Debug.Assert(gens.Length == GENS_LENGTH);

            Debug.Assert(gens[0] <= GENS_MAXS[0]);
            SchoolAtention = (SchoolAtentionEnum)gens[0];

            Debug.Assert(gens[1] <= GENS_MAXS[1]);
            FoodHabits = (FoodHabitsEnum)gens[1];

            Debug.Assert(gens[2] <= GENS_MAXS[2]);
            LearningHabits = (LearningHabitsEnum)gens[2];

            Debug.Assert(gens[3] <= GENS_MAXS[3]);
            LifePriorities = (LifePrioritiesEnum)gens[3];

            Debug.Assert(gens[4] <= GENS_MAXS[4]);
            FreetimePreferences = (FreetimePreferencesEnum)gens[4];

            Debug.Assert(gens[5] <= GENS_MAXS[5]);
            LerningPreferences = (LerningPreferencesEnum)gens[5];

            Debug.Assert(gens[6] <= GENS_MAXS[6]);
            LengthOfPlanning = 1 + gens[6];
        }

        /// <summary>
        /// Prints gens in readable form
        /// </summary>
        /// <returns>string representation of gens</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (SchoolAtention)
            {
                case SchoolAtentionEnum.Active:
                    sb.Append("Write notes, ");
                    break;
                case SchoolAtentionEnum.Pasive:
                    sb.Append("Goes to school, ");
                    break;
                case SchoolAtentionEnum.No:
                    sb.Append("Stay at home, ");
                    break;
                default:
                    throw new NotImplementedException("Unknown type");
            }
            switch (FoodHabits)
            {
                case FoodHabitsEnum.Cooking:
                    sb.Append("cook himself, ");
                    break;
                case FoodHabitsEnum.Restaurant:
                    sb.Append("eats from restaurants, ");
                    break;
                case FoodHabitsEnum.FromHome:
                    sb.Append("mum food, ");
                    break;
                default:
                    throw new NotImplementedException("Unknown type");
            }

            switch (LearningHabits)
            {
                case LearningHabitsEnum.Continuous:
                    sb.Append("learn continuously, ");
                    break;
                case LearningHabitsEnum.OnLastMomnet:
                    sb.Append("learn on last moment, ");
                    break;
                default:
                    throw new NotImplementedException("Unknown type");
            }

            sb.Append($"plan for {LengthOfPlanning} days, ");

            switch (LifePriorities)
            {
                case LifePrioritiesEnum.FirstWork:
                    sb.Append("first work then fun, ");
                    break;
                case LifePrioritiesEnum.FirstFun:
                    sb.Append("first fun then work, ");
                    break;
                default:
                    throw new NotImplementedException("Unknown type");
            }

            switch (FreetimePreferences)
            {
                case FreetimePreferencesEnum.Friends:
                    sb.Append("friends, ");
                    break;
                case FreetimePreferencesEnum.Sport:
                    sb.Append("sport, ");
                    break;
                case FreetimePreferencesEnum.Nothing:
                    sb.Append("nothing, ");
                    break;
                case FreetimePreferencesEnum.Reading:
                    sb.Append("reading, ");
                    break;
                default:
                    throw new NotImplementedException("Unknown type");
            }

            switch (LerningPreferences)
            {
                case LerningPreferencesEnum.Challenging:
                    sb.Append("school at first place");
                    break;
                case LerningPreferencesEnum.Normal:
                    sb.Append("school is import");
                    break;
                case LerningPreferencesEnum.EasyGoing:
                    sb.Append("school is evil");
                    break;
                default:
                    throw new NotImplementedException("Unknown type");
            }

            return sb.ToString();
        }
    }
}
