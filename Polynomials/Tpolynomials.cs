using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomials
{

    internal class Tpolynomials
    {
        double[] Original_Vector_X()
        {
            double[] vector_X = { 0 , 1.7 , 3.4 , 5.1 , 6.8 , 8.5 };
            return vector_X;
        }
        //-----------------------------------------------------------------------------------------
        double[] Original_Vector_Y()
        {
            double[] vector_Y = { 0, 3.0038, 5.2439, 7.3583, 9.4077, 11.415 };
            return vector_Y;
        }
        //-----------------------------------------------------------------------------------------
        double[] Coefficient_b_c (double [] vector_X, double[] vector_Y)
        {
            double[] coefficients = new double[2];
            double coefficient_XX = 0;
            double coefficient_X = 0;
            double coefficient_Y = 0;
            double coefficient_YX = 0;
            for (int i = 0; i < vector_X.Length; i++)
            {
                coefficient_XX += vector_X[i] * vector_X[i];
            }
            for (int i = 0; i < vector_X.Length; i++)
            {
                coefficient_X += vector_X[i];
            }
            for (int i = 0; i < vector_Y.Length; i++)
            {
                coefficient_Y += vector_Y[i];
            }
            for (int i = 0; i < vector_Y.Length; i++)
            {
                coefficient_YX += vector_Y[i] * vector_X[i];
            }
            Console.WriteLine(coefficient_XX);
            Console.WriteLine(coefficient_X);
            Console.WriteLine(coefficient_Y);
            Console.WriteLine(coefficient_YX);
            coefficients[1] = (coefficient_YX - (coefficient_XX * coefficient_Y) / coefficient_X ) / (coefficient_X - (coefficient_XX * (vector_X.Length)) / coefficient_X);
            coefficients[0] = (coefficient_Y - (vector_X.Length) * coefficients[1]) / coefficient_X;
            return coefficients;
        }
        //-----------------------------------------------------------------------------------------
        double Function_P1 (double X, double[] coefficients)
        {
            double fuction_P = coefficients[0] * X + coefficients[1];
            return fuction_P;
        }
        //-----------------------------------------------------------------------------------------
        double Error_calculation_P1(double[] vector_X, double[] vector_Y, double[] coefficients)
        {
            double error = 0;
            for (int i = 0; i < vector_X.Length; i++)
            {
                error += Math.Pow((Function_P1(vector_X[i], coefficients) - vector_Y[i]), 2); 
            }
            return error;
        }
        //-----------------------------------------------------------------------------------------
        void Polynomial_first_degree()
        {
            var vector_X = Original_Vector_X();
            var vector_Y = Original_Vector_Y();
            var coefficients = Coefficient_b_c(vector_X, vector_Y);
            Console.WriteLine("P1(x) = " + coefficients[0] + " * x + " + coefficients[1]);
            Console.WriteLine("Error = " + Error_calculation_P1(vector_X, vector_Y, coefficients));
        }
        //-----------------------------------------------------------------------------------------
        double[] Coefficient_a_b_c(double[] vector_X, double[] vector_Y)
        {
            double[] coefficients = new double[3];
            double coefficient_XXXX = 0;
            double coefficient_XXX = 0;
            double coefficient_XX = 0;
            double coefficient_X = 0;
            double coefficient_Y = 0;
            double coefficient_YX = 0;
            double coefficient_YXX = 0;
            for (int i = 0; i < vector_X.Length; i++)
            {
                coefficient_XXXX += vector_X[i] * vector_X[i] * vector_X[i] * vector_X[i];
            }
            for (int i = 0; i < vector_X.Length; i++)
            {
                coefficient_XXX += vector_X[i] * vector_X[i] * vector_X[i];
            }
            for (int i = 0; i < vector_X.Length; i++)
            {
                coefficient_XX += vector_X[i] * vector_X[i];
            }
            for (int i = 0; i < vector_X.Length; i++)
            {
                coefficient_X += vector_X[i];
            }
            for (int i = 0; i < vector_Y.Length; i++)
            {
                coefficient_Y += vector_Y[i];
            }
            for (int i = 0; i < vector_Y.Length; i++)
            {
                coefficient_YX += vector_Y[i] * vector_X[i];
            }
            for (int i = 0; i < vector_Y.Length; i++)
            {
                coefficient_YXX += vector_Y[i] * vector_X[i] * vector_X[i];
            }
            double[,] Array_coefficients =
            {
                { coefficient_XXXX, coefficient_XXX, coefficient_XX, coefficient_YXX },
                { coefficient_XXX, coefficient_XX, coefficient_X, coefficient_YX },
                { coefficient_XX, coefficient_X, vector_X.Length, coefficient_Y }
            };
            Array_coefficients = Straight_running_Jordan_Gauss(Array_coefficients);
            Array_coefficients = Reverse_running_Jordan_Gauss(Array_coefficients);
            for (int i = 0; i < coefficients.GetLength(0); i++)
            {
                coefficients[i] = Array_coefficients[i, Array_coefficients.GetLength(1) - 1];
            }
            return coefficients;
        }
        //-----------------------------------------------------------------------------------------
        double[,] Straight_running_Jordan_Gauss(double[,] Arr)
        {
            for (int i = 0; i < Arr.GetLength(0); i++)
            {
                double b = Arr[i, i];
                for (int j = i; j < Arr.GetLength(1); j++)
                {
                    Arr[i, j] /= b;
                }
                for (int k = i + 1; k < Arr.GetLength(0); k++)
                {
                    double d = Arr[k, i];
                    for (int l = i; l < Arr.GetLength(1); l++)
                    {
                        Arr[k, l] -= d * Arr[i, l];
                    }
                }
            }
            return Arr;
        }
        //-----------------------------------------------------------------------------------------
        double[,] Reverse_running_Jordan_Gauss(double[,] Arr)
        {
            for (int i = Arr.GetLength(0) - 1; i >= 1; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    double d = Arr[j, i];
                    for (int k = 0; k < Arr.GetLength(1); k++)
                    {
                        Arr[j, k] -= d * Arr[i, k];
                    }
                }
            }
            return Arr;

        }
        //-----------------------------------------------------------------------------------------
        double Function_P2(double X, double[] coefficients)
        {
            double fuction_P = coefficients[0] * X * X + coefficients[1] * X + coefficients[2];
            return fuction_P;
        }
        //-----------------------------------------------------------------------------------------
        double Error_calculation_P2(double[] vector_X, double[] vector_Y, double[] coefficients)
        {
            double error = 0;
            for (int i = 0; i < vector_X.Length; i++)
            {
                error += Math.Pow((Function_P2(vector_X[i], coefficients) - vector_Y[i]), 2);
            }
            return error;
        }
        //-----------------------------------------------------------------------------------------
        void Polynomial_second_degree()
        {
            var vector_X = Original_Vector_X();
            var vector_Y = Original_Vector_Y();
            var coefficients = Coefficient_a_b_c(vector_X, vector_Y);
            Console.WriteLine("P2(x) = " + coefficients[0] + " * x^2 + " + coefficients[1] + " * x + " + coefficients[2]);
            Console.WriteLine("Error = " + Error_calculation_P2(vector_X, vector_Y, coefficients));

        }
        //-----------------------------------------------------------------------------------------
        public void Start()
        {
            Console.WriteLine("Choose a method:");
            Console.WriteLine("1. Polynomial of the first degree." + "\n" + "2. Polynomial of the second degree.");
            int w = int.Parse(Console.ReadLine());
            switch (w)
            {
                case 1:
                    {
                        Polynomial_first_degree();
                        break;
                    }
                case 2:
                    {
                        Polynomial_second_degree();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("default");
                        break;
                    }
            }
        }
    }
}
