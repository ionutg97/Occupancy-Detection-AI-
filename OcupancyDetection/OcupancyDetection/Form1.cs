using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OcupancyDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           // string[] lines = System.IO.File.ReadAllLines("Dataset/datatraining.txt");
            string[] lines = System.IO.File.ReadAllLines("Dataset/test.txt");//2100 instante

            DataSet dataset = new DataSet(lines.Length - 1);
            String[] args;
            InputData inputData;
            int nrAtributes;

            for (int i= 1;i< lines.Length ;i++) //prima linie este capul de tabel
            {
               args = lines[i].Replace("\"", "").Split(','); //campurile instantei
               nrAtributes = args.Length - 2; //primul e un id ce poate fi ignorat, iar ultimul este clasa
               inputData = new InputData(nrAtributes);
               inputData.x[0] = DateTime.Parse(args[1]).ToOADate();
               for(int j=1;j<nrAtributes;j++)
                {
                    inputData.x[j] = Double.Parse(args[j + 1]);
                }
                dataset.instanta[i - 1] = inputData;
                double y = Double.Parse(args[args.Length - 1]) ;
                dataset.y[i - 1] = y == 0.0 ? -1.0 : 1.0;   //transformarea in -1 sau 1
            }


            EvolutionaryAlgorithm.EvolutionaryAlgorithm ea = new EvolutionaryAlgorithm.EvolutionaryAlgorithm();

            int populationNumber = Int32.Parse(populationSize.Text);
            int generations = Int32.Parse(maxGenerations.Text);
            double crossover = Double.Parse(crossoverRate.Text);
            double mutation = Double.Parse(mutationRate.Text);
            double cost = Double.Parse(C.Text);

            Chromosome solution = ea.Solve(dataset, populationNumber, generations, crossover, mutation,cost);
            output.Text = solution.ToString();

        }
    }
}
