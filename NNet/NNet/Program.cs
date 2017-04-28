using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNet
{
    public class Neuron
    {
        const int n = 54;
        public Random r = new Random();
        double[] Xi = new double[n];
        double[] Weight = new double[n];
        public Neuron(double[] Xi)
        {
            this.Xi = Xi;
        }
        
        double[] InitWeight()
        {
            for (int i = 0; i < n; i++)
            {
                Weight[i] = r.NextDouble() * 0.2;
            }
            return Weight;
        }

        double Summator()
        {
            double S = 0;
            InitWeight();
            for (int i = 0; i < n; i++)
            {
                double c = Xi[i] * Weight[i];
                S += c;
            }
            return S;
        }

        public double FuncActivation()
        {
            double s = Summator();
            double y = 1 / (1 + Math.Exp(-s));
            return y;
        }
    }

    public class NeuroNet
    {
        
        const int n = 54;
        public NeuroNet(double[] Xi)
        {
            this.Xi = Xi;
        }
        double[] Xi = new double[n];

        //инициализация и расчет выходного значения сети
        public double[] InitNet()
        {
            double[] net = new double[10];
            Neuron N = new Neuron(Xi);

            for (int i = 0; i < 10; i++)
            {
                net[i] = N.FuncActivation();
            }
            return net;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            double[] X = { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 };
            NeuroNet p = new NeuroNet(X);
            double[] c = p.InitNet();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}-й выход: {1}", i+1, c[i]);
            }
            Console.ReadKey();
        }
    }
}
