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
        private int[] ConstTestVal = { 1, 25, 40, 55, 70, 85, 100 };
        private double gamaTest = 0.00001;
        private int PopulationSizeTest = 50;
        private int NumGenTest = 7000;
        private string[] outValues = new string[11];
        public Form1()
        {
            InitializeComponent();
        }

        public static DataSet readData(String filename)
        {
            string[] lines = System.IO.File.ReadAllLines("Dataset/" + filename);
            int size = lines.Length - 1;
         

            DataSet dataset = new DataSet(size);
            String[] args;
            InputData inputData;
            int nrAtributes;
            int index = 1;
            for (int i = 1; i <= lines.Length - 1; i += 1) //prima linie este capul de tabel
            {
                args = lines[i].Replace("\"", "").Split(','); //campurile instantei
                nrAtributes = args.Length - 2; //primul e un id ce poate fi ignorat, iar ultimul este clasa
                inputData = new InputData(nrAtributes);
                inputData.x[0] = DateTime.Parse(args[1]).ToOADate();
                for (int j = 1; j < nrAtributes; j++)
                {
                    inputData.x[j] = Double.Parse(args[j + 1]);
                }
                dataset.instanta[index - 1] = inputData;
                double y = Double.Parse(args[args.Length - 1]);
                dataset.y[index - 1] = y == 0.0 ? -1.0 : 1.0;   //transformarea in -1 sau 1
                index++;
            }
            return dataset;
        }

        public static double ComputeB(double[] alfa, InputData[] x, double[] y, double gamma)
        {
            int S = 0; //numarul vectorilor suport
            double sumat = 0.0, sumas = 0.0;
            for (int s = 0; s < alfa.Length; s++)
            {
                if (alfa[s] != 0.0)    //atunci este vector suport
                {
                    S++;
                    sumat = 0.0;
                    for (int t = 0; t < alfa.Length; t++)
                    {
                        if (alfa[t] != 0.0)
                        {

                            sumat += alfa[t] * y[t] * utils.Util.gaussianKernel(x[t].x, x[s].x, gamma);
                        }

                    }
                    sumas += y[s] - sumat;

                }
            }
            return (1.0 / (double)S) * sumas;
        }

        public static double functieDiscriminant(double[] x, double[] alfa, DataSet dataset, double b, double gama)
        {
            double suma = 0.0;
            for (int i = 0; i < alfa.Length; i++)
            {
                if (alfa[i] != 0.0) //altfel produsul ar fi 0
                {

                    suma += alfa[i] * dataset.y[i] * utils.Util.gaussianKernel(dataset.instanta[i].x, x, gama);

                }
            }
            return suma + b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clasification();
           // TestCases();
        }


        private  void Clasification()
        {
            DataSet dataSet = readData("datatraining2.txt");

            EvolutionaryAlgorithm.EvolutionaryAlgorithm ea = new EvolutionaryAlgorithm.EvolutionaryAlgorithm();

            int populationNumber = Int32.Parse(populationSize.Text);
            int generations = Int32.Parse(maxGenerations.Text);
            double crossover = Double.Parse(crossoverRate.Text);
            double mutation = Double.Parse(mutationRate.Text);
            double cost = Double.Parse(C.Text);
            double gama = Double.Parse(gamma.Text);


            Chromosome solution = ea.Solve(dataSet, populationNumber, generations, crossover, mutation, cost, gama);
            output.Text += solution.ToString();

            double b = ComputeB(solution.alfa, dataSet.instanta, dataSet.y, gama);

            DataSet datatest = readData("datatest.txt");

            double nrTotal = 0.0, nrCorect = 0.0;
            for (int j = 0; j < datatest.size; j++)
            {
                double nou = functieDiscriminant(datatest.instanta[j].x, solution.alfa, dataSet, b, gama);
                nou = nou > 0.0 ? 1.0 : -1.0;
                if (nou == datatest.y[j])
                    nrCorect++;

                nrTotal++;
            }
            output.Text += "\nProcent clasificare datatest1 : " + ((nrCorect / nrTotal) * 100).ToString() + "%\n";
            datatest = readData("datatest2.txt");

            nrTotal = 0.0;
            nrCorect = 0.0;
            for (int j = 0; j < datatest.size; j++)
            {
                double nou = functieDiscriminant(datatest.instanta[j].x, solution.alfa, dataSet, b, gama);
                nou = nou > 0.0 ? 1.0 : -1.0;
                if (nou == datatest.y[j])
                    nrCorect++;

                nrTotal++;
            }
            output.Text += "\nProcent clasificare datatest2 : " + ((nrCorect / nrTotal) * 100).ToString() + "%\n";

        }
        private void TestCases()
        {
            
              DataSet dataSet = readData("datatraining2.txt");
              DataSet datatest1 = readData("datatest.txt");
              DataSet datatest2 = readData("datatest2.txt");

              populationSize.Text = PopulationSizeTest.ToString();
              maxGenerations.Text = NumGenTest.ToString();

              int populationNumber = Int32.Parse(populationSize.Text);
              int generations = Int32.Parse(maxGenerations.Text);
              double crossover = Double.Parse(crossoverRate.Text);
              double mutation = Double.Parse(mutationRate.Text);
              int testIndex = 1;
              double nrOne = 10;

              outValues[1] = "\t populationSize: " + populationSize.Text;
              outValues[2] = "\t maxGenerations: " + maxGenerations.Text;
              outValues[3] = "\t crossoverRate: " + crossoverRate.Text;
              outValues[4] = "\t mutationRate: " + mutationRate.Text;

            while (gamaTest <= 0.00001)
            {
                for (int costIndex = 0; costIndex < 1; costIndex++)
                {
                    outValues[0] = "Test: " + testIndex.ToString() + " \n { \n Input:";
                    EvolutionaryAlgorithm.EvolutionaryAlgorithm ea = new EvolutionaryAlgorithm.EvolutionaryAlgorithm();

                    C.Text = ConstTestVal[costIndex].ToString();
                    gamma.Text = gamaTest.ToString();

                    double cost = ConstTestVal[costIndex];
                    double gama = gamaTest;

                    outValues[5] = "\t cost: " + C.Text;
                    outValues[6] = "\t gama: " + gamma.Text;

                    Chromosome solution = ea.Solve(dataSet, populationNumber, generations, crossover, mutation, cost, gama);

                    output.Text += "\n" + solution.ToString();
                    outValues[7] = "\n Output: \n " + solution.ToString();

                    double b = ComputeB(solution.alfa, dataSet.instanta, dataSet.y, gama);

                    double nrTotal = 0.0, nrCorect = 0.0;
                    for (int j = 0; j < datatest1.size; j++)
                    {
                        double nou = functieDiscriminant(datatest1.instanta[j].x, solution.alfa, dataSet, b, gama);
                        nou = nou > 0.0 ? 1.0 : -1.0;
                        if (nou == datatest1.y[j])
                            nrCorect++;

                        nrTotal++;
                    }
                    output.Text += "\nProcent clasificare datatest1 : " + ((nrCorect / nrTotal) * 100).ToString() + "%\n";
                    outValues[8] = "\t Procent clasificare datatest1: " + ((nrCorect / nrTotal) * 100).ToString();
                    nrTotal = 0.0;
                    nrCorect = 0.0;
                    for (int j = 0; j < datatest2.size; j++)
                    {
                        double nou = functieDiscriminant(datatest2.instanta[j].x, solution.alfa, dataSet, b, gama);
                        nou = nou > 0.0 ? 1.0 : -1.0;
                        if (nou == datatest2.y[j])
                            nrCorect++;

                        nrTotal++;
                    }
                    output.Text += "\nProcent clasificare datatest2 : " + ((nrCorect / nrTotal) * 100).ToString() + "%\n";
                    outValues[9] = "\t Procent clasificare datatest2 : " + ((nrCorect / nrTotal) * 100).ToString();
                    outValues[10] = "}\n";

                    using (var tw = System.IO.File.AppendText("Dataout/outData.txt"))
                    {
                        foreach (string val in outValues)
                        {
                            // tw.WriteLine(val);
                        }
                    }
                    testIndex++;
                }
                if (gamaTest >= 1)
                {
                    gamaTest = gamaTest + (1.0 / nrOne);
                    nrOne *= 10;
                }
                else
                {
                    gamaTest *= 10;
                }

            }
        }
    }
        
    
}
