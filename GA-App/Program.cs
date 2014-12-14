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
                    Function = i => Math.Log(i)
                };

            geneticEngine.SelectionFunction = SelectionFuncions.First;
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
                Console.WriteLine("");
            }

            Console.WriteLine("=============");

            geneticEngine.SelectionFunction = SelectionFuncions.First;
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
                Console.WriteLine("");
            }

            Console.WriteLine("=============");

            geneticEngine.SelectionFunction = SelectionFuncions.First;
            geneticEngine.MaxSteps = 1000;
            for (int i = 0; i < 10; i++)
            {
                geneticEngine.Start();
                geneticEngine.PrintResult();
                Console.WriteLine("");
            }

            Console.WriteLine("=============");

            geneticEngine.SelectionFunction = SelectionFuncions.First;
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
