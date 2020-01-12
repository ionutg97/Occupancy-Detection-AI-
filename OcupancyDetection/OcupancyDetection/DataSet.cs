using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OcupancyDetection
{
    public class DataSet
    {
       public int size;
       public InputData[] instanta { get; set; } //lista cu instantele de antrenare
       public double [] y { get; set; } //iesirea corespunzatoare instanta[i] are iesirea y[i]

        public DataSet(int size)
        {
            this.size = size;
            instanta = new InputData[size];
            y = new double[size];
        }
    }
}
