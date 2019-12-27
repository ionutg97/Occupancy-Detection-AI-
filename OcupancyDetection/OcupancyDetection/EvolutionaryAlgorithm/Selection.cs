using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    class Selection
    {
        private static Random _rand = new Random();

        public static Chromosome Tournament(Chromosome[] population)
        {
            int indexi = _rand.Next(0, population.Length);
            int indexj;
            do
            {
                indexj = _rand.Next(0, population.Length);
            } while(indexi == indexj);
            if (population[indexi].Fitness > population[indexj].Fitness)
               return population[indexi];

            return population[indexj];
        }

        public static Chromosome GetBest(Chromosome[] population)
        {
            return new Chromosome(population.OrderByDescending(item => item.Fitness).First());
        }
    }
}
