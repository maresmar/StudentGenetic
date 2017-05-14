using StudentGenetic.Properties;
using System;
using System.Diagnostics;
using static StudentGenetic.Gens;

namespace StudentGenetic
{
    /// <summary>
    /// Events or task in student's day
    /// </summary>
    public abstract class LifeEvent
    {
        public enum LifeEventCategory { Freetime, School, Errands }
        /// <summary>
        /// Category of event
        /// </summary>
        public abstract LifeEventCategory Category { get; }
        /// <summary>
        /// Time that occupy event if is proceed
        /// </summary>
        public abstract int Duration(Student student);
        /// <summary>
        /// Tells if student has enough time and power to do it
        /// </summary>
        /// <param name="student">Student with this event</param>
        /// <returns>Time in hours</returns>
        public virtual bool CanHandleIt(Student student)
        {
            return student.RemainingTime - student.NextSleepDuration >= Duration(student);
        }
        /// <summary>
        /// Proceed the event
        /// </summary>
        /// <param name="student">Student which does this event</param>
        public abstract void DoIt(Student student);

        /// <summary>
        /// Proceed if the student can't do event
        /// </summary>
        /// <param name="student">Student which does this event</param>
        public virtual void CantDoIt(Student student)
        {
            student.Log($"I don't have enough time {ToString()}");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Common function that manipulates student
    /// </summary>
    public class CommonTasks
    {
        /// <summary>
        /// Improves ability to learn but it costs some time
        /// </summary>
        /// <param name="student">destination student</param>
        /// <param name="gainCoef">Coefficient how the PosibilityToLern is improved (speed, >1)</param>
        private static void GainEnertyForHour(Student student, double gainCoef)
        {
            // Points
            student.Points.Positive++;
            // Next learning will be easier
            student.PosibilityToLern *= gainCoef;
            student.PosibilityToLern = student.PosibilityToLern > 2 - Settings.Default.coefGiveUp
                ? 2 - Settings.Default.coefGiveUp : student.PosibilityToLern;
            student.RemainingTime--;
        }

        /// <summary>
        /// Improves ability to learn but it costs some time
        /// </summary>
        /// <param name="student">destination student</param>
        /// <param name="duration">Time in hour</param>
        public static void GainEnergy(Student student, int duration)
        {
            GainEnergy(student, duration, Settings.Default.coefRest);
        }

        /// <summary>
        /// Improves ability to learn but it costs some time
        /// </summary>
        /// <param name="student">destination student</param>
        /// <param name="duration">Time in hour</param>
        /// <param name="gainCoef">Coefficient how the PosibilityToLern is improved (speed, >1)</param>
        public static void GainEnergy(Student student, int duration, double gainCoef)
        {
            for (int i = 0; i < duration; i++)
            {
                GainEnertyForHour(student, Settings.Default.coefRest);
            }
        }

        /// <summary>
        /// Learn something to learn but it costs some time
        /// </summary>
        /// <param name="student">destination student</param>
        /// <param name="duration">Time in hour</param>
        /// <param name="amount">Amount of knowledge to learn (part of knowledge) <see cref="Core.KNOWLADGE_PER_ACADEMIC_HOUR"/></param>
        public static void GainKnowlidge(Student student, int duration, double amount)
        {
            for (int i = 0; i < duration; i++)
            {
                if (amount == 0)
                    return;
                else
                    student.Learned = true;
                // Points
                student.Points.Negative++;
                // Previous knowledge affect next learning
                double prevKnowladge = student.BestKnowledge == 0 ? 1 : 0.7 + (0.3 * (student.Knowladge / student.BestKnowledge));
                student.PosibilityToLern = student.PosibilityToLern < Settings.Default.coefGiveUp ? Settings.Default.coefGiveUp : student.PosibilityToLern;
                // Learning
                student.Knowladge += (int)(amount * student.PosibilityToLern * prevKnowladge);
                // Next learning will be harder
                student.PosibilityToLern *= Settings.Default.coefLerning;
                student.RemainingTime--;
            }
        }
    }

    /// <summary>
    /// Learning task
    /// </summary>
    public class LearnEvent : LifeEvent
    {
        double amount;
        int duration;

        public override LifeEventCategory Category => LifeEventCategory.School;

        public override int Duration(Student s)
        {
            return duration;
        }

        /// <summary>
        /// Create new one hour learning event
        /// </summary>
        public LearnEvent() : this(1)
        {
        }

        /// <summary>
        /// Create new learning event with specified time
        /// </summary>
        /// <param name="duration">Duration of learning</param>
        public LearnEvent(int duration) : this(Core.KNOWLADGE_PER_ACADEMIC_HOUR, duration)
        {
        }

        /// <summary>
        /// Create new learning event with specified time
        /// </summary>
        /// <param name="duration">Duration of learning</param>
        /// <param name="amountPerHour">Amount to learn per hour</param>
        public LearnEvent(double amountPerHour, int duration)
        {
            amount = amountPerHour;
            this.duration = duration;
        }

        public override void DoIt(Student student)
        {
            student.Log("Learning");
            CommonTasks.GainKnowlidge(student, duration, amount);
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for learning");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Resting task, here can student gain an energy
    /// </summary>
    public class RestEvent : LifeEvent
    {
        double restCoef;
        int duration;
        public override int Duration(Student s)
        {
            return duration;
        }

        public RestEvent() : this(1)
        {
        }
        public RestEvent(int duration) : this(Settings.Default.coefRest , duration)
        {
        }
        public RestEvent(double restCoef, int duration)
        {
            this.restCoef = restCoef;
            this.duration = duration;
        }

        public override LifeEventCategory Category => LifeEventCategory.Freetime;

        public override void DoIt(Student student)
        {
            student.Log("Resting");
            CommonTasks.GainEnergy(student, duration, restCoef);
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for rest");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Task in free time, improves possibility to learn
    /// </summary>
    public class FunEvent : LifeEvent
    {
        static readonly int FriendsTime = 3;
        static readonly int SportTime = 2;
        static readonly int NothingTime = 1;
        static readonly int ReadingTime = 1;

        FreetimePreferencesEnum freetimePreferencesEnum;

        /// <summary>
        /// Create new fun event with specific activity
        /// </summary>
        /// <param name="freetimePreferencesEnum">Activity to do in free time</param>
        public FunEvent(FreetimePreferencesEnum freetimePreferencesEnum)
        {
            this.freetimePreferencesEnum = freetimePreferencesEnum;
        }

        public override LifeEventCategory Category => LifeEventCategory.Freetime;

        public override int Duration(Student student)
        {
            switch (freetimePreferencesEnum)
            {
                case FreetimePreferencesEnum.Friends:
                    return FriendsTime;
                case FreetimePreferencesEnum.Sport:
                    return SportTime;
                case FreetimePreferencesEnum.Nothing:
                    return NothingTime;
                case FreetimePreferencesEnum.Reading:
                    return ReadingTime;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void DoIt(Student student)
        {
            switch (freetimePreferencesEnum)
            {
                case FreetimePreferencesEnum.Friends:
                    student.Log("I'm going out with friends");
                    student.Points.Positive += 8;
                    CommonTasks.GainEnergy(student, FriendsTime);
                    break;
                case FreetimePreferencesEnum.Sport:
                    student.Log("I'm going running");
                    student.Points.Positive += 4;
                    CommonTasks.GainEnergy(student, SportTime);
                    break;
                case FreetimePreferencesEnum.Nothing:
                    student.Points.Positive += 2;
                    student.Log("I will do nothing");
                    CommonTasks.GainEnergy(student, NothingTime);
                    break;
                case FreetimePreferencesEnum.Reading:
                    student.Points.Positive += 2;
                    student.Log("I read a book");
                    CommonTasks.GainEnergy(student, ReadingTime, (Settings.Default.coefRest + 1) / 2);
                    Settings.Default.Reload();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for fun");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Eating event, usually improves possibility to learn
    /// </summary>
    public class CookingEvent : LifeEvent
    {
        static readonly int EasyCookTime = 1;

        public override LifeEventCategory Category => LifeEventCategory.Errands;

        public override int Duration(Student student)
        {
            if (student.Gens.FoodHabits == FoodHabitsEnum.Cooking)
                return Core.COOK_TIME;
            else
                return EasyCookTime;
        }

        public override bool CanHandleIt(Student s)
        {
            return true;
        }

        public override void DoIt(Student student)
        {
            switch (student.Gens.FoodHabits)
            {
                case FoodHabitsEnum.Cooking:
                    student.Log("I cook food");
                    student.Points.Positive += 5;
                    CommonTasks.GainEnergy(student, Core.COOK_TIME);
                    break;
                case FoodHabitsEnum.Restaurant:
                    student.Log("I go to restaurant");
                    CommonTasks.GainEnergy(student, EasyCookTime);
                    break;
                case FoodHabitsEnum.FromHome:
                    student.Log("I have food from mum");
                    CommonTasks.GainEnergy(student, EasyCookTime);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for food :-(");
            student.Points.Negative += 100;
        }
    }

    /// <summary>
    /// Continuous learning (for future tests)
    /// </summary>
    public class ContinuousLerningEvent : LifeEvent
    {
        static readonly int MaxDuration = 2;

        public override LifeEventCategory Category => LifeEventCategory.School;

        public override int Duration(Student student)
        {
            return MaxDuration;
        }

        public override bool CanHandleIt(Student s)
        {
            return s.RemainingTime > Core.RECOMANDED_SLEEP_TIME;
        }

        public override void DoIt(Student student)
        {
            int time = 0;
            while (student.KnowladgePercentage < 100 && time < MaxDuration)
            {
                CommonTasks.GainKnowlidge(student, 1, Core.KNOWLADGE_PER_ACADEMIC_HOUR);
                time++;
                if (student.Gens.LerningPreferences == LerningPreferencesEnum.EasyGoing)
                {
                    student.Points.Negative += 5;
                }
            }
            switch (time)
            {
                case 0:
                    student.Log("I know everything, I don't need to learn");
                    break;
                case 1:
                    student.Log("I have to learn something");
                    break;
                case 2:
                    student.Log("I have to learn many things to school");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for learning");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Public events with friends, improves possibility to learn but coasts more time
    /// </summary>
    public class PublicEvent : LifeEvent
    {

        public override LifeEventCategory Category => LifeEventCategory.Freetime;

        public override int Duration(Student student)
        {
            if (student.Gens.FreetimePreferences == FreetimePreferencesEnum.Friends)
                return 5;
            else
                return 3;
        }

        public override void DoIt(Student student)
        {
            int time;
            if (student.Gens.FreetimePreferences == FreetimePreferencesEnum.Friends)
            {
                student.Log("I go out with friends");
                time = 5;
                student.Points.Positive += 15;
            }
            else
            {
                student.Log("I go out");
                time = 3;
                student.Points.Positive += 15;
            }
            for (int i = 0; i < time; i++)
            {
                student.RemainingTime--;
                student.PosibilityToLern *= (Settings.Default.coefLerning + 1) / 2;
            }
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for public event");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Homework to school
    /// </summary>
    public class HomeworkEvent : LifeEvent
    {
        public override LifeEventCategory Category => LifeEventCategory.School;

        public override int Duration(Student student)
        {
            return 1;
        }

        public override void DoIt(Student student)
        {
            student.Log("I do homework");
            student.PosibilityToLern *= Settings.Default.coefLerning;
            student.RemainingTime--;
            student.Learned = true;
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for doing homework");
            student.Points.Negative += 20;
        }
    }

    /// <summary>
    /// Event for learning to test
    /// </summary>
    public class LernForTestEvent : LifeEvent
    {
        double amount;
        public override LifeEventCategory Category => LifeEventCategory.School;

        /// <summary>
        /// Create new learning event with specific amount to learn
        /// </summary>
        /// <param name="amount">Amount of knowledge to learn</param>
        public LernForTestEvent(double amount)
        {
            this.amount = amount;
        }

        public override int Duration(Student student)
        {
            return 1;
        }

        public override void DoIt(Student student)
        {
            student.Log("I'm learning for test");
            while (student.Knowladge < amount && student.PosibilityToLern > Settings.Default.coefGiveUp)
            {
                student.Points.Negative++;
                CommonTasks.GainKnowlidge(student, 1, Core.KNOWLADGE_PER_ACADEMIC_HOUR);
                student.Learned = true;
            }
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for learning for test");
            student.Points.Negative += 10;
        }
    }

    /// <summary>
    /// Test event, does not occupy time, but decrease points if student fails 
    /// </summary>
    public class TestEvent : LifeEvent
    {
        double amount;
        public override LifeEventCategory Category => LifeEventCategory.School;

        /// <summary>
        /// Create new test event with specific amount to pass the test
        /// </summary>
        /// <param name="amount">Amount of knowledge to pass the test</param>
        public TestEvent(double amount)
        {
            this.amount = amount;
        }

        public override int Duration(Student student)
        {
            return 0;
        }

        public override bool CanHandleIt(Student student)
        {
            return true;
        }

        public override void DoIt(Student student)
        {
            if (student.Knowladge < amount)
            {
                student.Log("I failed the test");
                student.Points.Negative += 100;
            }
            else
            {
                student.Log("I passed the test");
                student.Points.Positive += 100;
            }
        }

        public override void CantDoIt(Student student)
        {
            Debug.Fail("I have to do test");
        }
    }

    /// <summary>
    /// Regular going to school
    /// </summary>
    public class SchoolEvent : LifeEvent
    {
        int duration;
        public override LifeEventCategory Category => LifeEventCategory.School;

        /// <summary>
        /// Create new event with specific time at school
        /// </summary>
        /// <param name="duration">Time in school</param>
        public SchoolEvent(int duration)
        {
            this.duration = duration;
        }

        public override int Duration(Student student)
        {
            return duration;
        }

        public override void DoIt(Student student)
        {
            double amount;
            switch (student.Gens.SchoolAtention)
            {
                case SchoolAtentionEnum.Active:
                    student.Log($"I go to school {duration} h");
                    student.Points.Positive += duration;
                    amount = Settings.Default.coefSchoolActive;
                    student.RemainingTime--; // Traveling
                    break;
                case SchoolAtentionEnum.Pasive:
                    student.Log($"I go to school {duration} h, but I don't pay much attention");
                    amount = Settings.Default.coefSchoolPasive;
                    student.RemainingTime--; // Traveling
                    break;
                case SchoolAtentionEnum.No:
                    student.Log("I won't go to school");
                    switch (student.Gens.LerningPreferences)
                    {
                        case LerningPreferencesEnum.Challenging:
                            student.Points.Negative += duration;
                            break;
                        case LerningPreferencesEnum.Normal:
                            student.Points.Negative += duration / 2;
                            break;
                    }
                    amount = 0;
                    break;
                default:
                    throw new NotImplementedException();
            }
            CommonTasks.GainKnowlidge(student, duration, amount);
            student.BestKnowledge += (int)Core.KNOWLADGE_PER_ACADEMIC_HOUR * duration;
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for school");
            if (student.Gens.SchoolAtention != SchoolAtentionEnum.No)
                student.Points.Negative += 75;
            else
                student.Points.Negative += 50;
        }
    }

    /// <summary>
    /// Event that simulates traveling to home
    /// </summary>
    public class TranspoertEvent : LifeEvent
    {
        public override LifeEventCategory Category => LifeEventCategory.Errands;

        public override int Duration(Student student)
        {
            return Settings.Default.timeToHome * 2;
        }

        public override void DoIt(Student student)
        {
            if (student.Gens.FoodHabits == FoodHabitsEnum.FromHome)
            { // I should go home because of food from mum
                student.ToHomeOffset = (student.ToHomeOffset + Settings.Default.homeFood) / 2;
            }
            else
            {
                student.ToHomeOffset = (student.ToHomeOffset + (100 - Settings.Default.homeFood)) / 2;
            }
            if (Core.rn.Next(100) < student.ToHomeOffset)
            {
                student.ToHomeOffset /= 3;
                student.Points.Positive += 5;
                if (student.Gens.LearningHabits == LearningHabitsEnum.Continuous)
                {
                    student.Log("I travel to home and learn to school");
                    CommonTasks.GainKnowlidge(student, Settings.Default.timeToHome * 2, Core.KNOWLADGE_PER_ACADEMIC_HOUR);
                }
                else
                {
                    student.Log("I go home");
                    CommonTasks.GainEnergy(student, Settings.Default.timeToHome * 2);
                }
            }
        }

        public override void CantDoIt(Student student)
        {
            student.Log("No time for going home");
            if (student.Gens.FoodHabits == FoodHabitsEnum.FromHome)
                student.Points.Negative += 250;
            else
                student.Points.Negative += 25;
        }
    }

    /// <summary>
    /// Sleep event
    /// </summary>
    public class SleepEvent : LifeEvent
    {
        int duration;

        /// <summary>
        /// Create new sleeping event, student will sleep all reasonable time he has, 
        /// the student's LastSleep will be updated 
        /// </summary>
        public SleepEvent() : this(-1) {
        }

        /// <summary>
        /// Student will sleep specific time
        /// </summary>
        /// <param name="duration">Length of sleep</param>
        public SleepEvent(int duration)
        {
            this.duration = duration;
        }

        public override LifeEventCategory Category => LifeEventCategory.Errands;

        public bool NightSleep => duration < 1;

        public override int Duration(Student student)
        {
            if (student.RemainingTime > Core.MAX_SLEEP_TIME)
                return Core.MAX_SLEEP_TIME;
            else
                return student.RemainingTime;
        }

        public override bool CanHandleIt(Student student)
        {
            return student.RemainingTime > 2;
        }

        public override void DoIt(Student student)
        {
            int time = CountSleepDuration(student);
            if (time >= 4)
            {
                student.Points.Positive += 5;
                student.Log("I will have long sleeping (" + time*1.5 + " h)");
                CommonTasks.GainEnergy(student, 8);
            }
            else if (time > 2)
            {
                student.Log("I go sleeping " + time * 1.5 + " h");
                CommonTasks.GainEnergy(student, duration);
            }
            else
                Debug.Assert(false);
        }

        public override void CantDoIt(Student student)
        {
            int time = CountSleepDuration(student);
            student.Log("I cannot sleep so long :-( (" + time*1.5 + " h)");
            student.Points.Negative += 20;
        }

        private int CountSleepDuration(Student student)
        {
            int time;
            if (NightSleep)
            {
                if (student.RemainingTime > Core.MAX_SLEEP_TIME)
                    time = Core.MAX_SLEEP_TIME;
                else
                    time = student.RemainingTime;
                // Updates sleep information in student
                student.LastSleepDuration = time;
            }
            else
                time = duration;
            return time;
        }
    }

    /// <summary>
    /// Generates random fun event if student has some free time
    /// </summary>
    public class FreetimeEvent : LifeEvent
    {
        public override LifeEventCategory Category => LifeEventCategory.Freetime;

        public override int Duration(Student student)
        {
            return -1;
        }

        public override bool CanHandleIt(Student s)
        {
            return s.RemainingTime > Core.MAX_SLEEP_TIME;
        }

        public override void DoIt(Student student)
        {
            student.Log("I home some free time today...");
            do
            {
                student.Points.Positive += 10;
                new FunEvent((FreetimePreferencesEnum)Core.rn.Next(4)).DoIt(student);
            } while (student.RemainingTime > Core.MAX_SLEEP_TIME + Core.PERSONAL_HIGIENE_TIME+1);
        }

        public override void CantDoIt(Student student)
        {
            student.Log("I don't have any free time");
            student.Points.Negative += 20;
        }
    }

    /// <summary>
    /// Personal hygiene event, very important for points
    /// </summary>
    public class PersonalHygieneEvent : LifeEvent
    {
        public override LifeEventCategory Category => LifeEventCategory.Errands;

        public override int Duration(Student student)
        {
            return Core.PERSONAL_HIGIENE_TIME;
        }

        public override void DoIt(Student student)
        {
            student.Log("I do some personal hygiene");
            student.RemainingTime -= Core.PERSONAL_HIGIENE_TIME;
        }

        public override void CantDoIt(Student student)
        {
            student.Log("I don't have time, I have to stay dirty");
            student.Points.Negative += 40;
        }
    }
}
