using System;
using System.Collections.Generic;

namespace LinearEquationsSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the coefficients of the linear equations (enter END when finished):");

            List<double[]> coefficients = new List<double[]>();
            int numVariables = -1;

            while (true)
            {
                Console.WriteLine("Enter the coefficients of the next equation:");

                string input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                string[] inputs = input.Split();
                if (numVariables == -1)
                {
                    numVariables = inputs.Length - 1;
                }
                else if (inputs.Length - 1 != numVariables)
                {
                    Console.WriteLine("Error: number of variables does not match previous equations");
                    return;
                }

                double[] equation = new double[inputs.Length];
                for (int i = 0; i < inputs.Length; i++)
                {
                    equation[i] = double.Parse(inputs[i]);
                }

                coefficients.Add(equation);
            }

            int numEquations = coefficients.Count;
            if (numEquations != numVariables)
            {
                Console.WriteLine("Error: number of equations does not match number of variables");
                return;
            }

            Console.WriteLine("The system of linear equations before elimination is:");
            for (int i = 0; i < numEquations; i++)
            {
                Console.Write("Equation {0}: ", i + 1);
                for (int j = 0; j < numVariables; j++)
                {
                    Console.Write("{0}x{1} + ", coefficients[i][j], j + 1);
                }
                Console.WriteLine("= {0}", coefficients[i][numVariables]);
            }

            for (int i = 0; i < numVariables - 1; i++)
            {
                int maxRow = i;
                for (int j = i + 1; j < numEquations; j++)
                {
                    if (Math.Abs(coefficients[j][i]) > Math.Abs(coefficients[maxRow][i]))
                    {
                        maxRow = j;
                    }
                }

                if (coefficients[maxRow][i] == 0)
                {
                    Console.WriteLine("The system has no unique solution.");
                    return;
                }

                if (maxRow != i)
                {
                    for (int k = i; k < numVariables + 1; k++)
                    {
                        double temp = coefficients[maxRow][k];
                        coefficients[maxRow][k] = coefficients[i][k];
                        coefficients[i][k] = temp;
                    }
                }

                for (int j = i + 1; j < numEquations; j++)
                {
                    double k = coefficients[j][i] / coefficients[i][i];
                    for (int p = i; p < numVariables + 1; p++)
                    {
                        coefficients[j][p] = coefficients[j][p] - k * coefficients[i][p];
                    }
                }
            }

            double[] solutions = new double[numVariables];
            for (int i = numVariables - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < numVariables; j++)
                {
                    sum += solutions[j] * coefficients[i][j];
                }
                solutions[i] = (coefficients[i][numVariables] - sum) / coefficients[i][i];
            }

            Console.WriteLine("The solutions to the system of linear equations are:");
            for (int i = 0; i < numVariables; i++)
            {
                Console.WriteLine("x{0} = {1}", i + 1, solutions[i]);
            }
        }
    }
}