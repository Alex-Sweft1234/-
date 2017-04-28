using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NNet
{
    public class Neuron
    {
        const int n = 54;
        
        double[] Xi = new double[n];
        double[] Weight = new double[n];
        public Neuron(double[] Xi)
        {
            this.Xi = Xi;
        }
        
        public double[] InitWeight()
        {
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                Weight[i] = r.NextDouble();
            }
            return Weight;
        }

        public double Summator(double[] Weight)
        {
            double S = 0;
            for (int i = 0; i < n; i++)
            {
                double c = Xi[i] * Weight[i];
                S += c;
            }
            return S;
        }

        public double FuncActivation(double s)
        {
            double y = 1 / (1 + Math.Exp(-s));
            return y;
        }
    }

    public class NeuroNet
    {
        Random r = new Random();
        const int n = 54;
        public NeuroNet(double[] Xi)
        {
            this.Xi = Xi;
        }
        double[] Xi = new double[n];
        double[,] Wet = new double[10, 54];
        //инициализация и расчет выходного значения сети
        public double[] InitNet()
        {
            double[] net = new double[10];
            Neuron N = new Neuron(Xi);
            for (int i = 0; i < 10; i++)
            {
                double[] W = N.InitWeight();
                for (int j = 0; j < 54; j++)
                {
                    Wet[i, j] = W[j];
                }
                double Sum = N.Summator(W);
                net[i] = N.FuncActivation(Sum);
            }
            return net;
        }
        public double[] trainNet()
        {
            //при обучении обычно сходится функция на 0,5; редко достигает 0,3
            double err = 0.3;
            double functag = 0;
            double f = 0;
            double[] Target = {0,1,0,0,0,0,0,0,0,0};
            double[] Yi = InitNet();
            //расчет целевой функции
            for (int i = 0; i < 10; i++)
            {
                f = 0.5 * Math.Pow((Yi[i] - Target[i]),2);
                functag = functag + f;
            }
            double[] net = new double[10];
            Neuron N = new Neuron(Xi);
            //обучение сети
            while (functag > err)
            {
                //случайное изменение весов
               for (int i = 0; i < 10; i++)
               {
                    double step = 0.1;
                    int j = r.Next(53);
                    int d = r.Next(1);
                    if (d == 0) Wet[i, j] = Wet[i, j] - step;
                    else Wet[i, j] = Wet[i, j] + step;

                }
                //инициализация сети с использованием случайно изменнных весов
                for (int i = 0; i < 10; i++)
                {
                     double[] Wet2 = new double[54];
                     for (int j = 0; j < 54; j++)
                     {
                        Wet2[j] = Wet[i, j];
                     }
                double Sum = N.Summator(Wet2);
                net[i] = N.FuncActivation(Sum);
                }
                //рассчет целевой функции 
                functag = 0;
                for (int i = 0; i < 10; i++)
                {
                    f = 0.5 * Math.Pow((net[i] - Target[i]), 2);
                    functag = functag + f;
                }
                Console.WriteLine(functag);
                //if (functag == 0.5) break;
            
            }
            StreamWriter sw = new StreamWriter("d:\\Веса.txt");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 54; j++)
                {
                    sw.Write("{0};", Wet[i, j]);
                }
                sw.WriteLine();
            }
            return net;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //распознанние символа "2"
            Console.WriteLine("Обучение сети для распознания символа '2'");
            Console.WriteLine("Если обучение останавливается на значении 0,5 перезапустите программу");
            Console.WriteLine("Нажмите клавишу 'Enter' и дождитеть конца обучения");
            Console.ReadKey();
            double[] X = { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 };
            NeuroNet p = new NeuroNet(X);
            double[] c = p.trainNet();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}-й выход: {1}", i+1, c[i]);
            }
            Console.ReadKey();
        }
    }
}
