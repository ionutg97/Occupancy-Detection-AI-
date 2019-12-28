using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    public class EvolutionaryAlgorithm
    { 
            
        public static void ComputeFitness(Chromosome chromosome, DataSet dataSet,double gamma)
        {

            //trebuie fortata constrangerea, ajustam valorile afla ca suma de alfai * yi = 0 ;
            double []newAlfa = utils.Util.AdjustmentAlgorithm(chromosome.alfa, dataSet.y);
            chromosome.alfa = newAlfa;

            double suma1 = 0.0; //prima suma din functie       
            double suma2 = 0.0;

            for (int i = 0; i < newAlfa.Length; i++)
            {
                if (newAlfa[i] != 0.0)
                {
                    suma1 += newAlfa[i];
                    for (int j = 0; j < newAlfa.Length; j++)
                    {
                        if (newAlfa[j] != 0.0)
                        {
                              suma2 += dataSet.y[i] * dataSet.y[j] * newAlfa[i] * newAlfa[j] * utils.Util.gaussianKernel(dataSet.instanta[i].x, dataSet.instanta[j].x,gamma);
                             // suma2 += dataSet.y[i] * dataSet.y[j] * newAlfa[i] * newAlfa[j] * utils.Util.produsScalar(dataSet.instanta[i].x, dataSet.instanta[j].x);

                        }
                    }
                }
            } 

            double fitness = suma1 - 0.5 * suma2;   //forma duala
            chromosome.Fitness = fitness;
        }
       

        public Chromosome Solve(DataSet dataSet, int populationSize, int maxGenerations, double crossoverRate, double mutationRate,double C,double gamma)
        {

            Chromosome[] population = new Chromosome[populationSize];
            for (int i = 0; i < population.Length; i++) //initializare populatie
            {
                population[i] = new Chromosome(dataSet.size, 0, C);
                ComputeFitness(population[i],dataSet,gamma);
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

                    ComputeFitness(copil,dataSet,gamma);

                    newPopulation[i] = copil;
                }

                for (int i = 0; i < populationSize; i++)
                    population[i] = newPopulation[i];

            }

            return Selection.GetBest(population);
        }
    }
}
