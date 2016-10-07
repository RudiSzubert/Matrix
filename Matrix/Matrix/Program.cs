using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public class Matrix
    {
        private int[,] matrix;
        private int height, wide;
        public Matrix(int wi = 1, int wid = 50, int hei = 1, int heigh = 50)
        {
            Random rnd = new Random();
            this.height = rnd.Next(hei, heigh);
            this.wide = rnd.Next(wi, wid);
            this.matrix = new int[this.height, this.wide];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.wide; j++)
                {
                    this.matrix[i, j] = rnd.Next(0, 10);
                }
            }
        }

        //private Matrix[] IfFits(Matrix two) //Get two matrix and returns from smaller
        //{
        //    Matrix[] Matrix = new Matrix[2];
        //    if (this.wide <= two.wide && this.height <= two.height)
        //    {
        //        Matrix[0] = this;
        //        Matrix[1] = two;
        //    }
        //    else if (two.wide <= this.wide && two.height <= this.height)
        //    {
        //        Matrix[1] = this;
        //        Matrix[0] = two;
        //    }
        //    return Matrix;
        //}

        private int IfFit(Matrix two) //returns true if one matrix is smaller than another
        {
            if (this.wide <= two.wide && this.height <= two.height)
            {
                return -1;
            }
            else if (two.wide < this.wide && two.height < this.height)
            {
                return 1;
            }
            else return 0;
        }

        public int[] IfIncludes(Matrix two) //returns int[], where int[0] is number of ways matrix matches. Every next two ints are points on bigger matrix where upper left corner of smaller one matches 
        {
            int[] fits = new int[5000];
            int more = this.IfFit(two);
            if (more != 0)
            {
                Matrix[] Matrix = more == 1 ? new Matrix[] { two, this } : new Matrix[] { this, two };
                fits[0] = 0;
                for (int i = 0; i < Matrix[1].height - Matrix[0].height + 1; i++)
                {
                    for (int j = 0; j < Matrix[1].wide - Matrix[0].wide + 1; j++)
                    {

                        if (ConsoleApplication4.Matrix.IfMatch(Matrix, i, j))
                        {
                            fits[2 * fits[0] + 1] = i;
                            fits[2 * fits[0] + 2] = j;
                            fits[0]++;
                        }
                    }
                }
            }
            return fits;
        }

        public void Draw()
        {
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.wide; j++)
                {
                    if (j < (this.wide - 1))
                        System.Console.Write(this.matrix[i, j]);
                    else
                        System.Console.WriteLine(this.matrix[i, j]);
                }
            }
        } // draws matrix

        private static bool IfMatch(Matrix[] Matrix, int heigh, int wid) // check if matrix matches from given point
        {
            for (int i = 0; i < Matrix[0].height; i++)
            {
                for (int j = 0; j < Matrix[0].wide; j++)
                {
                    if (Matrix[0].matrix[i, j] != Matrix[1].matrix[i + heigh, j + wid])
                        return false;
                }
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Matrix macierz = new Matrix(2, 2, 2, 2);
            System.Threading.Thread.Sleep(100);
            Matrix macierz2 = new Matrix(60, 60, 60, 60);
            int[] result = macierz.IfIncludes(macierz2);
            if (result[0] == 0)
            {
                System.Console.WriteLine("Matrix does not fit");
            }
            else
            {
                for (int i = 0; i < 2 * result[0] + 1; i++)
                {
                    System.Console.WriteLine(result[i]);
                }
                macierz2.Draw();
                macierz.Draw();
                Console.ReadKey();
            }
        }
    }
}
