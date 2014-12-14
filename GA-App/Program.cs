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
                    MaxSteps = 100,
                    TermsCount = 5,
                    MinValue = 2,
                    MaxValue = 22,
                    ControlPointNumber = 13,
                    SelectionFunction = SelectionFunctions.First,
                    CrossFunction = CrossFuncions.First,
                    Function = i => Math.Log(i)
                };

            Console.WriteLine("=============");
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
            }

            Console.WriteLine("=============");
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
            }

            Console.WriteLine("=============");
            geneticEngine.MaxSteps = 1000;
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
                Console.WriteLine("");
            }

            Console.WriteLine("=============");
            geneticEngine.ControlPointNumber = 3;
            geneticEngine.MaxSteps = 10000;
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
                Console.WriteLine("");
            }

            Console.ReadKey();
        }
    }
}
