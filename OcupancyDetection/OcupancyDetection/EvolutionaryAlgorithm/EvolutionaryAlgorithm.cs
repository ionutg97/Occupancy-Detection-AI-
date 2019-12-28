using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OcupancyDetection.EvolutionaryAlgorithm
{
    public class EvolutionaryAlgorithm
    { 
        private static Random _rand = new Random();

        public static double[] AdjustmentAlgorithm(double [] oldAlfa,double [] y)
        {
            double[] newAlfa = new double[oldAlfa.Length];

            for (int i = 0; i < oldAlfa.Length; i++)
            {
                newAlfa[i] = oldAlfa[i];    //copiez vechile valori
            }


            double suma = 0.0;

            double sumaPozitiva = 0.0;
            double sumaNegativa = 0.0;
            for (int j = 0; j < newAlfa.Length; j++)
            {
                if (newAlfa[j] != 0.0)
                {
                    if (y[j] == 1)
                        sumaPozitiva += newAlfa[j];
                    else
                        sumaNegativa += newAlfa[j];
                }
            }
            suma = sumaPozitiva - sumaNegativa;

            while (suma != 0.0)
            {
                if (suma < 0) suma = -suma;
                int indexk;
                if (sumaPozitiva > sumaNegativa)
                {

                    do
                    {
                        indexk = _rand.Next(0, newAlfa.Length);

                    } while ((y[indexk] != 1.0) && (newAlfa[indexk] != 0.0));   //aleg o valoare cu plus
                    if (newAlfa[indexk] > suma)
                    {

                        newAlfa[indexk] -= suma;    //scad suma din coeficient
                        sumaPozitiva -= suma;    //actualizez suma coeficientilor pozitivi
                    }
                    else
                    {
                        sumaPozitiva -= newAlfa[indexk];    //din suma pozitiva se scade cat era in newalfa la positia indexk
                        newAlfa[indexk] = 0.0;
                    }
                }
                else
                {
                    do
                    {
                        indexk = _rand.Next(0, newAlfa.Length);

                    } while ((y[indexk] != -1.0) && (newAlfa[indexk] != 0.0));   //aleg o valoare cu minus
                    if (newAlfa[indexk] > suma)
                    {

                        newAlfa[indexk] -= suma;    //scad suma din coeficient
                        sumaNegativa -= suma;    //actualizez suma coeficientilor pozitivi
                    }
                    else
                    {
                        sumaNegativa -= newAlfa[indexk];    //din suma pozitiva se scade cat era in newalfa la positia indexk
                        newAlfa[indexk] = 0.0;
                    }
                }

                suma = sumaPozitiva - sumaNegativa;

            }
            return newAlfa;
        }
        public static void ComputeFitness(Chromosome chromosome, DataSet dataSet)
        {
            double gamma = 1;
            //trebuie fortata constrangerea, ajustam valorile afla ca suma de alfai * yi = 0 ;
            double []newAlfa = AdjustmentAlgorithm(chromosome.alfa, dataSet.y);
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
                            suma2 += dataSet.y[i] * dataSet.y[j] * newAlfa[i] * newAlfa[j] * gaussianKernel(dataSet.instanta[i].x, dataSet.instanta[j].x,gamma);
                        }
                    }
                }
            } 

            double fitness = suma1 - 0.5 * suma2;   //forma duala
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
        public static double gaussianKernel(double []x,double [] z,double gamma)
        {
            double[] diference = new double [x.Length];
            for(int i=0;i<x.Length;i++)
            {
                diference[i] = x[i] - z[i];
            }
            return -gamma * produsScalar(diference, diference); //se simplifica radicalul cu patratul
        }

        public Chromosome Solve(DataSet dataSet, int populationSize, int maxGenerations, double crossoverRate, double mutationRate,double C)
        {

            Chromosome[] population = new Chromosome[populationSize];
            for (int i = 0; i < population.Length; i++) //initializare populatie
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
