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
           
            for(int i=0;i<oldAlfa.Length;i++)
            {
                newAlfa[i] = oldAlfa[i];    //copiez vechile valori
            }


            double suma = 0.0;
            for(int j = 0; j < oldAlfa.Length; j++) { 
           
                suma += oldAlfa[j] * dataSet.y[j];
            }
            double sumaPozitiva, sumaNegativa;

            while (suma != 0)
            {
                sumaPozitiva = 0;
                sumaNegativa = 0;
                for (int j = 0; j < newAlfa.Length; j++)
                {
                    if(dataSet.y[j] == 1)
                       sumaPozitiva += newAlfa[j] * dataSet.y[j];
                    else
                        sumaNegativa += newAlfa[j] * dataSet.y[j];
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
                    newAlfa[indexk] = 0.0;
                }


                suma = 0.0;
                for (int j = 0; j < newAlfa.Length; j++)
                {
                    suma += newAlfa[j] * dataSet.y[j];
                }

            }

           
           double suma1 = 0.0; //prima suma din functie
            for (int j = 0; j < newAlfa.Length; j++)
            {
                suma1 += newAlfa[j];
            }
            double suma2 = 0.0;

            for(int i=0;i<newAlfa.Length;i++)
            {
                for(int j=0;j<newAlfa.Length;j++)
                {
                    suma2 += dataSet.y[i] * dataSet.y[j] * newAlfa[i] * newAlfa[j] * produsScalar(dataSet.instanta[i].x, dataSet.instanta[j].x);
              
                }
            }

            double fitness = suma1 - (1.0 / 2.0) * suma2;
            chromosome.Fitness = fitness;
        }
        public static double produsScalar(double [] xi, double [] xj)
        {
            double rezultat = 0.0;
            for(int i=0;i<xi.Length;i++)
            {
                rezultat += xi[i] * xj[i];
            }
            return rezultat;
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
