using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    class EvolutionaryAlgorithm
    { 
        private static Random _rand = new Random();
        public static void ComputeFitness(Chromosome chromosome,DataSet dataSet)
        {
            //trebuie fortata constrangerea
            double[] oldAlfa = chromosome.alfa;
            double[] newAlfa = new double[oldAlfa.Length];
            int i;
            for(i=0;i<oldAlfa.Length;i++)
            {
                newAlfa[i] = oldAlfa[i];    //copiez vechile valori
            }


            double suma = 0;
            for(int j=0;j<oldAlfa.Length;j++)
            {
                suma += oldAlfa[i] * dataSet.y[i];
            }
            double sumaPozitiva, sumaNegativa;

            while (suma != 0)
            {
                sumaPozitiva = 0;
                sumaNegativa = 0;
                for (int j = 0; j < newAlfa.Length; j++)
                {
                    if(dataSet.y[i] == 1)
                       sumaPozitiva += newAlfa[i] * dataSet.y[i];
                    else
                        sumaNegativa += newAlfa[i] * dataSet.y[i];
                }
                int indexk;
                if (sumaPozitiva>sumaNegativa)
                {
                    
                    do
                    {
                        indexk = _rand.Next(0, newAlfa.Length);

                    } while (dataSet.y[indexk] != 1);   //aleg o valoare cu plus
                }
                else
                {
                    do
                    {
                        indexk = _rand.Next(0, newAlfa.Length);

                    } while (dataSet.y[indexk] != -1);   //aleg o valoare cu minus
                }

                if(newAlfa[indexk] > suma)
                {
                    newAlfa[indexk] -= suma;    
                }
                else
                {
                    newAlfa[indexk] = 0;
                }


                suma = 0;
                for (int j = 0; j < newAlfa.Length; j++)
                {
                    suma += newAlfa[i] * dataSet.y[i];
                }

            }



        }

        public Chromosome Solve(DataSet dataSet, int populationSize, int maxGenerations, double crossoverRate, double mutationRate)
        {

            double C = 100;
            Chromosome[] population = new Chromosome[populationSize];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Chromosome(dataSet.size, 0, C);
                ComputeFitness(population[i],dataSet);
            }

            for (int gen = 0; gen < maxGenerations; gen++)
            {
                Chromosome[] newPopulation = new Chromosome[populationSize];
                newPopulation[0] = Selection.GetBest(population); // elitism

                for (int i = 1; i < populationSize; i++)
                {

                    Chromosome parent1 = Selection.Tournament(population);
                    Chromosome parent2 = Selection.Tournament(population);

                    Chromosome copil = Crossover.Arithmetic(parent1, parent2, crossoverRate);

                    Mutation.Reset(copil, mutationRate);

                    ComputeFitness(copil,dataSet);

                    newPopulation[i] = copil;
                }

                for (int i = 0; i < populationSize; i++)
                    population[i] = newPopulation[i];
            }

            return Selection.GetBest(population);
        }
    }
}
