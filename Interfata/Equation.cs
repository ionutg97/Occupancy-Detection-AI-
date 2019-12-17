/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2016, Florin Leon                                    *
 *  E-mail:      florin.leon@tuiasi.ro                                    *
 *  Website:     http://florinleon.byethost24.com/lab_ia.htm              *
 *  Description: Evolutionary Algorithms                                  *
 *               (Artificial Intelligence lab 9)                          *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;

namespace EvolutionaryAlgorithm
{
    /// <summary>
    /// Clasa care reprezinta problema din prima aplicatie: rezolvarea ecuatiei
    /// </summary>
    public class Equation : IOptimizationProblem
    {
        public Chromosome MakeChromosome()
        {
            // un cromozom are o gena (x) care poate lua valori in intervalul (-5, 5)
            return new Chromosome(1, new double[] { -5 }, new double[] { 5 });
        }

        public void ComputeFitness(Chromosome c)
        {
            throw new Exception("Aceasta metoda trebuie completata");

            double x = c.Genes[0];
            // c.Fitness = functia care va fi maximizata
        }
    }
}