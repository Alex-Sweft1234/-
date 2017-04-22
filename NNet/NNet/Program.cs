using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNet
{
    public class Neuron
    {
        const int n = 53;
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
        
        const int n = 53;
        public NeuroNet(double[] Xi)
        {
            this.Xi = Xi;
        }
        double[] Xi = new double[n];

        //инициализация и расчет выходного значения сети
        public double InitNet(double[] Xi)
        {
            double[] net = new double[53];
            Neuron N = new Neuron(Xi);

            for (int i = 0; i < n; i++)
            {
                net[i] = N.FuncActivation();
            }
            Neuron NV = new Neuron(net);
            double y = NV.FuncActivation();
            return y;
        }

        public double[] runNet()
        {
            double[] y = new double[10];
            for (int i = 0; i < 10; i++)
            {
                y[i] = InitNet(Xi);
            }
            return y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            double[] X = { 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0 };
            NeuroNet p = new NeuroNet(X);
            double[] c = p.runNet();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}-я итерация: {1}", i+1, c[i]);
            }
            Console.ReadKey();
        }
    }
}
