using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection
{
    public class Chromosome
    {
        public int noGenes { get; set; } // egal cu numarul de instante de antrenare

        public double[] alfa { get; set; } // valorile alfa i

        public double minValue { get; set; }
        public double maxValue { get; set; } //adica parametrul C

        public double Fitness { get; set; } // valoarea functiei de adaptare a individului

        private static Random _rand = new Random();

        public Chromosome(int noGenes, double minValue, double maxValue)
        {
            this.noGenes = noGenes;
            alfa = new double[noGenes];
            this.minValue =minValue;
            this.maxValue = maxValue;

            for (int i = 0; i < noGenes; i++)
            {          
                alfa[i] = minValue + _rand.NextDouble() * (maxValue - minValue); // initializare aleatorie a genelor
            }
        }

        public Chromosome(Chromosome c) // constructor de copiere
        {
            noGenes = c.noGenes;
            Fitness = c.Fitness;

            alfa = new double[c.noGenes];
            maxValue = c.maxValue;
            minValue = c.minValue;

            for (int i = 0; i < c.alfa.Length; i++)
            {
                alfa[i] = c.alfa[i];
            }
        }

    }
}
