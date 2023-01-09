using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophy
{
    class Program
    {
        static void Main(string[] args)
        {
            var philosophers = Init(5, 5);

            var threads = new Task[philosophers.Length];

            for (int i = 0; i < philosophers.Length; i++)
            {
                threads[i] = Task.Run(philosophers[i].Start);
            }

            Task.WaitAll(threads);

            foreach (var i in philosophers)
            {
                Console.WriteLine(i.times_eaten);
            }
            Console.ReadLine();
        }

        private static Philosopher[] Init(int philosopher_count, int meals_count)
        {
            var philosophers = new Philosopher[philosopher_count];

            for (int i = 0; i < philosopher_count; i++)
            {
                philosophers[i] = new Philosopher(meals_count);

            }

            for (int i = 0; i < philosopher_count; i++)
            {
                philosophers[i].LeftPhilosopher = i == 0 ? philosophers[philosopher_count - 1] : philosophers[i - 1];
                philosophers[i].RightPhilosopher = i == philosopher_count - 1 ? philosophers[0] : philosophers[i + 1];

                philosophers[i].LeftFork = philosophers[i].LeftPhilosopher.RightFork ?? new Fork();
                philosophers[i].RightFork = philosophers[i].RightPhilosopher.LeftFork ?? new Fork();
            }

            return philosophers;
        }
    }
}
