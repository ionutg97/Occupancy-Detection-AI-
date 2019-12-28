using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection.utils
{
    public class Util
    {
        private static Random _rand = new Random();
        public static double produsScalar(double[] xi, double[] xj)
        {
            double rezultat = 0.0;
            for (int i = 0; i < xi.Length; i++)
            {
                rezultat += xi[i] * xj[i];
            }
            return rezultat;
        }
       
        
        public static double gaussianKernel(double[] x, double[] z, double gamma)
        {
            double[] difference = new double[x.Length];
            for(int i=0;i<x.Length;i++)
            {
                difference[i] = x[i] - z[i];
            }
            return -gamma * produsScalar(difference, difference);
        }
    
       
        public static double[] AdjustmentAlgorithm(double[] oldAlfa, double[] y)
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
    }
}
