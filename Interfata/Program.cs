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
    /// Programul principal care apeleaza algoritmul
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            throw new Exception("Aceasta metoda trebuie completata");

	    EvolutionaryAlgorithm ea = new EvolutionaryAlgorithm();

            //Chromosome solution = ea.Solve(new Equation(), ...); // de completat parametrii algoritmului
            // se foloseste -solution.Fitness pentru ca algoritmul evolutiv maximizeaza, iar aici avem o problema de minimizare
            //Console.WriteLine("{0:F6} -> {1:F6}", solution.Genes[0], -solution.Fitness);

            //solution = ea.Solve(new Fence(), ...); // de completat parametrii algoritmului
            //Console.WriteLine("{0:F2} {1:F2} -> {2:F4}", solution.Genes[0], solution.Genes[1], solution.Fitness);
        }
    }
}