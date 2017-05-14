using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGenetic
{
    /// <summary>
    /// Events from LifeEviroment, theses events are shared between all students
    /// </summary>
    public abstract class EnviroEvent
    {
        /// <summary>
        /// Plan event in student's calendar
        /// </summary>
        public abstract void Plan(Student student);
    }

    /// <summary>
    /// Global generated test
    /// </summary>
    class TestEnviroEvent : EnviroEvent
    {
        private int when;
        private double amount;

        public TestEnviroEvent(int when, double amount)
        {
            this.when = when;
            this.amount = amount;
        }

        public override void Plan(Student s)
        {
            // Student has some time for learning for test
            double part = amount / s.Gens.LengthOfPlanning;
            for (int i = 1; i <= s.Gens.LengthOfPlanning - 1; i++)
            {
                s.PlanEvent(new LernForTestEvent(part * i), when);
            }
            s.PlanEvent(new TestEvent(amount), when);

        }
    }

    /// <summary>
    /// Global homework
    /// </summary>
    class HomeworkEnviroEvent : EnviroEvent
    {
        private int when;

        public HomeworkEnviroEvent(int when)
        {
            this.when = when;
        }

        public override void Plan(Student s)
        {
            s.PlanEvent(new HomeworkEvent(), when);
        }
    }

    /// <summary>
    /// Global (free-time) public event
    /// </summary>
    class PublicEnviroEvent : EnviroEvent
    {
        private int when;
        private int duration;

        public PublicEnviroEvent(int when)
        {
            this.duration = 1 + Core.rn.Next(3);
            this.when = when;
        }

        public override void Plan(Student s)
        {
            s.PlanEvent(new PublicEvent(), when);
        }
    }
}
