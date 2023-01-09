using System;
using System.Threading;

namespace Philosophy
{
    public class Philosopher
    {
        public int MealsCount { get; set; } = 5;
        public int times_eaten = 0;
        private static int count = 0;
        public string Name { get; private set; }
        public Fork LeftFork { get; set; }
        public Fork RightFork { get; set; }
        public Philosopher LeftPhilosopher { get; set; }
        public Philosopher RightPhilosopher { get; set; }
		private State state;

        public Philosopher(int mealsCount)
        {
            Name = $"Philosopher {++count}";
            MealsCount = mealsCount;
            state = State.Eating;
        }

        public void Start()
        {
            while (times_eaten < MealsCount)
            {
                Think();

                if (PickUp())
                {
                    Eat();

                    PutDown(LeftFork);
                    PutDown(RightFork);
                }
            }
        }

        private bool PickUp()
        {
            if (Monitor.TryEnter(LeftFork))
            {
                Console.WriteLine($"{Name} picked up {LeftFork.Name}");

                if (Monitor.TryEnter(RightFork))
                {
                    Console.WriteLine($"{Name} picked up {RightFork.Name}");

                    return true;
                }
                else
                {
                    PutDown(LeftFork);
                }
            }
            return false;
        }

        private void PutDown(Fork fork)
        {
            Monitor.Exit(fork);
            Console.WriteLine($"{Name} put down {fork.Name}");
        }

        private void Eat()
        {
            Thread.Sleep(1000);

			state = State.Eating;
            times_eaten++;
            Console.WriteLine($"{Name} is eating");
        }

        private void Think()
        {
            Thread.Sleep(1000);

            if (state == State.Eating)
			{
				state = State.Thinking;
				Console.WriteLine($"{Name} is thinking");
			}
        }
    }
	
	public enum State 
	{
		Thinking,
		Eating
	}
}
