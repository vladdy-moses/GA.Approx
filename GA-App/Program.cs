using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var geneticEngine = new GeneticEngine()
                {
                    MaxSteps = 30,
                    TermsCount = 5,
                    MinValue = 8.3,
                    MaxValue = 10.3,
                    ControlPointNumber = 10,
                    SelectionFunction = SelectionFunctions.First,
                    CrossFunction = CrossFuncions.First,
                    Function = i => Math.Log(2*i)+Math.Cos(i/2)-Math.Tan(i)
                };

            Console.WriteLine("= ЗАПУСК ГЕНЕТИЧЕСКОГО АЛГОРИТМА =");
            Console.WriteLine("Число шагов: {0}", geneticEngine.MaxSteps);
            Console.WriteLine("Число точек: {0}", geneticEngine.ControlPointNumber);
            Console.WriteLine("Число слагаемых: {0}", geneticEngine.TermsCount);
            Console.WriteLine("");

            var list = new List<double>();
            for (int i = 0; i < 100; i++)
            {
                geneticEngine.Start();
                list.Add(geneticEngine.GetWinner().FitnessValue);
            }

            Console.WriteLine("Завершено!");

            Console.WriteLine("{0} {1} {2}", list.Min(), list.Average(), list.Max());

            Console.ReadKey();
        }
    }
}
