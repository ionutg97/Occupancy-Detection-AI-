﻿using System;
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

        public static DataSet readData(String filename)
        {
            string[] lines = System.IO.File.ReadAllLines("Dataset/"+filename);

            DataSet dataset = new DataSet(lines.Length - 1);
            String[] args;
            InputData inputData;
            int nrAtributes;

            for (int i = 1; i < lines.Length; i++) //prima linie este capul de tabel
            {
                args = lines[i].Replace("\"", "").Split(','); //campurile instantei
                nrAtributes = args.Length - 2; //primul e un id ce poate fi ignorat, iar ultimul este clasa
                inputData = new InputData(nrAtributes);
                inputData.x[0] = DateTime.Parse(args[1]).ToOADate();
                for (int j = 1; j < nrAtributes; j++)
                {
                    inputData.x[j] = Double.Parse(args[j + 1]);
                }
                dataset.instanta[i - 1] = inputData;
                double y = Double.Parse(args[args.Length - 1]);
                dataset.y[i - 1] = y == 0.0 ? -1.0 : 1.0;   //transformarea in -1 sau 1
            }
            return dataset;
        }

        public static double ComputeB(double [] alfa, InputData [] x, double [] y)
        {
            int S = 0; //numarul vectorilor suport
            double sumat = 0.0, sumas = 0.0;
            for(int s=0;s<alfa.Length;s++)
            {
                if(alfa[s]!=0.0)    //atunci este vector suport
                {
                    S++;
                    sumat = 0.0;
                    for(int t=0;t<alfa.Length;t++)
                    {
                        if(alfa[t]!=0.0)
                        {
                            sumat += alfa[t] * y[t] * utils.Util.gaussianKernel(x[t].x,x[s].x,1);

                        }
                        
                    }
                    sumas += y[s] - sumat;

                }
            }
            return (1.0 / S) * sumas;
        }

        public static double functieDiscriminant(double []x, double [] alfa, DataSet dataset, double b,double gama)
        {
            double suma = 0.0;
            for(int i=0;i<alfa.Length;i++)
            {
               if(alfa[i]!=0.0) //altfel produsul ar fi 0
                {
                     suma += alfa[i] * dataset.y[i] * utils.Util.gaussianKernel(dataset.instanta[i].x, x,gama);
                  //  suma += alfa[i] * dataset.y[i] * utils.Util.produsScalar(dataset.instanta[i].x, x );
                }
            }
            return suma + b;
        }

        private void button1_Click(object sender, EventArgs e)
        {

             //DataSet dataSet = readData("datatraining.txt");
             DataSet dataSet = readData("test.txt"); //200 instante pentru rapiditate

            EvolutionaryAlgorithm.EvolutionaryAlgorithm ea = new EvolutionaryAlgorithm.EvolutionaryAlgorithm();

            int populationNumber = Int32.Parse(populationSize.Text);
            int generations = Int32.Parse(maxGenerations.Text);
            double crossover = Double.Parse(crossoverRate.Text);
            double mutation = Double.Parse(mutationRate.Text);
            double cost = Double.Parse(C.Text);
            double gama = Double.Parse(gamma.Text);

            Chromosome solution = ea.Solve(dataSet, populationNumber, generations, crossover, mutation,cost,gama);
            output.Text += solution.ToString()  ;
            double b = ComputeB(solution.alfa, dataSet.instanta, dataSet.y);

            DataSet datatest = readData("datatest.txt");

            double nrTotal = 0.0, nrCorect = 0.0;
            for(int j=0;j<datatest.size;j++)
            {
                double nou = functieDiscriminant(datatest.instanta[j].x, solution.alfa, dataSet, b,gama);
                nou = nou > 0.0 ? 1.0 : -1.0;
                if (nou == datatest.y[j])
                    nrCorect++;

                nrTotal++;
            }
            output.Text += "\n Procent clasificare: " +( (nrCorect/nrTotal)*100).ToString()+"%\n";
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
            output.Text += "\n Procent clasificare2: " + ((nrCorect / nrTotal) * 100).ToString() + "%\n";
        }
    }
}
