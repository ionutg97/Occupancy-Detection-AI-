using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    class Selection
    {
        private static Random _rand = new Random();
        private static int rouletteCounter = 0;
        private static double suma;
        private static double[] sumePartiale;
        public static Chromosome Tournament(Chromosome[] population)
        {
            int indexi = _rand.Next(0, population.Length);
            int indexj;
            do
            {
                indexj = _rand.Next(0, population.Length);
            } while(indexi == indexj);
            if (population[indexi].Fitness > population[indexj].Fitness)
               return new Chromosome( population[indexi]);

            return new Chromosome(population[indexj]);
        }
        public static Chromosome Roulette(Chromosome[] population)
        {
           
            if(rouletteCounter == 0 )   //initializarea se face prima data cand intra in functie pentru o populatie
            {
                suma = population[0].Fitness;
                sumePartiale = new double[ population.Length];
                sumePartiale[0] = population[0].Fitness;
                for(int i =1;i <population.Length;i++)
                {
                    suma += population[i].Fitness;
                    sumePartiale[i] = sumePartiale[i - 1] + population[i].Fitness;

                }
                Array.Sort(sumePartiale);

            }
            
           if(rouletteCounter == population.Length*2)
           {
                  rouletteCounter = -1;   //au fost selectati toti necesari
           }
            

            double ales = _rand.NextDouble() * suma; //numar aleatorie inre 0 si suma

            int j = 0;
            while (ales > sumePartiale[j] && j<population.Length-1)
            {
                j++;
            }
            rouletteCounter++;
            return new Chromosome(population[j]);
        }

        public static Chromosome GetBest(Chromosome[] population)
        {
            return new Chromosome(population.OrderByDescending(item => item.Fitness).First());
        }
    }
}
