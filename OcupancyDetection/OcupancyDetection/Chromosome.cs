using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection
{
    public class Chromosome
    {
        public int NoGenes { get; set; } // numarul de gene ale individului

        public double[] Genes { get; set; } // valorile genelor

        public double[] MinValues { get; set; } // valorile minime posibile ale genelor

        public double[] MaxValues { get; set; } // valorile maxime posibile ale genelor

        public double Fitness { get; set; } // valoarea functiei de adaptare a individului

        private static Random _rand = new Random();
    }
}
