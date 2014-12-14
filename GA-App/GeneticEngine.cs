using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public class GeneticEngine : IGeneticEngine
    {
        public SelectionFunc SelectionFunction { set; get; }
        public int MaxSteps { set; get; }
        public int TermsCount { set; get; }
        public MathFunc Function { set; get; }
        public double MinValue { set; get; }
        public double MaxValue { set; get; }
        public int ControlPointNumber { set; get; }

        // private
        private List<IIndividual> individuals;
        private int maxIndividualsPerGeneration = 50;
        private Random random = new Random();
        private int individualsInNewGeneration = 100;
        
        public void Start()
        {
            // первое поколение
            GenerateFirst();

            //цикл
            for (int i = 0; i < MaxSteps; i++)
            {
                // скрещивание
                while(individuals.Count < individualsInNewGeneration)
                    SelectionFunction(individuals, maxIndividualsPerGeneration);

                // мутация среди потомков
                for (int j = maxIndividualsPerGeneration; j < individualsInNewGeneration; j++) //maxIndividualsPerGeneration
                {
                    if (random.NextDouble() < 0.5)
                        individuals[j].Mutate();

                    //Console.WriteLine("{0} mutate!!", j);
                }
                //Console.WriteLine("--------------");

                // отбор
                individuals.ForEach(ind => ind.FitnessValue = GetDelta(ind));
                individuals = individuals.OrderBy(ind => ind.FitnessValue).Take(maxIndividualsPerGeneration).ToList();

                //Console.WriteLine("STEP {0} :\t{1}\t{2}\t{3}", i, individuals[0].FitnessValue.ToString("F6"), individuals.Average(ind => ind.FitnessValue).ToString("F6"), individuals.Max(ind => ind.FitnessValue).ToString("F6"));

                if (individuals.First().FitnessValue < 0.0001)
                    break;
            }
        }

        // Показывает результат
        public void PrintResult()
        {
            var winner = individuals.First();

            // print points
            /*var stepSize = (MaxValue - MinValue) / ControlPointNumber;
            for (double num = MinValue; num <= MaxValue; num += stepSize)
            {
                var g1 = Function(num);
                var g2 = IndividualFunc(num, winner);
                var delta = Math.Abs(g1 - g2);

                Console.WriteLine("{0}\t{1}\t{2}\t{3}", num.ToString("F6"), g1.ToString("F6"), g2.ToString("F6"), delta.ToString("F6"));
            }*/

            // print formula
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            Console.Write("y = " + winner.Get(0).ToString("F6", cultureInfo));
            for (int i = 1; i < TermsCount; i++)
            {
                Console.Write(" + {0}*x^{1}", winner.Get(i).ToString("F6", cultureInfo), i);
            }
            Console.WriteLine("");

            Console.WriteLine("Delta is {0}", winner.FitnessValue);
        }

        // определение первого поколения
        protected void GenerateFirst()
        {
            individuals = new List<IIndividual>();
            for (int i = 0; i < individualsInNewGeneration; i++)
            {
                var individualItem = new Individual(TermsCount, random);
                individualItem.Generate();
                individuals.Add(individualItem);
            }

            // ОТБОР ПЕРВОГО ПОКОЛЕНИЯ
            individuals.ForEach(ind => ind.FitnessValue = GetDelta(ind));
            individuals = individuals.OrderBy(ind => ind.FitnessValue).Take(maxIndividualsPerGeneration).ToList();
        }

        // определение отклонения
        protected double GetDelta(IIndividual individual)
        {
            var stepSize = (MaxValue - MinValue) / ControlPointNumber;

            double deltaValue = 0.0;
            for (double num = MinValue; num <= MaxValue; num += stepSize)
            {
                var g1 = Function(num);
                var g2 = IndividualFunc(num, individual);
                var newDelta = Math.Abs(g1 - g2);
                if (newDelta > deltaValue)
                {
                    deltaValue = newDelta;
                }
            }
            return deltaValue;
        }

        // определение значения функции в точке
        protected double IndividualFunc(double num, IIndividual individual)
        {
            double result = individual.Get(0);
            for (int i = 1; i < TermsCount; i++)
            {
                result += Math.Pow(num, i) * individual.Get(i);
            }
            return result;
        }
    }
}
