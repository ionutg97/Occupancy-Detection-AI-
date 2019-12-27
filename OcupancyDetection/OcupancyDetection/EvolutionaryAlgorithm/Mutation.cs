using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    class Mutation
    {
        private static Random _rand = new Random();

        public static void Reset(Chromosome child, double rate)
        {
            for (int i = 0; i < child.noGenes; i++)
            {
                double prob = _rand.NextDouble();
                if (prob < rate)
                {
                    child.alfa[i] = child.minValue + _rand.NextDouble() * (child.maxValue - child.minValue);
                }
            }
        }
    }
}
