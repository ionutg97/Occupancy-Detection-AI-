using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection
{
    public class InputData
    {
       public int atributesNumber { get; set; } 
       public double[] x { get; set; }  //valoarea fiecarui atribut pentru o instanta
    
        public InputData(int nrAtributes)
        {
            atributesNumber = nrAtributes;
            x = new double[atributesNumber];
        }
    }
}
