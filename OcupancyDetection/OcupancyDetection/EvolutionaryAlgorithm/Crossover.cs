using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    class Crossover
    {
        private static Random _rand = new Random();

        public static Chromosome Arithmetic(Chromosome mother, Chromosome father, double rate)
        {
            double prop = _rand.NextDouble();
            Chromosome child;
            if (prop < 0.5)
                child = new Chromosome(mother);
            else
                child = new Chromosome(father);

            double probability = _rand.NextDouble();
            if (probability <= rate)
            {
                double multiplicator = _rand.NextDouble();

                for(int j =0; j<mother.noGenes;j++)
                {
                    child.alfa[j] = multiplicator * mother.alfa[j] + (1 - multiplicator) * father.alfa[j];
                }
              
            }
          return  child;
        }
    }
}
