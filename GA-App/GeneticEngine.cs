using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public class GeneticEngine
    {
        public CrossFunc CrossFunction { set; get; }
        public SelectionFunc SelectionFunction { set; get; }
        public int MaxSteps { set; get; }
        public int TermsCount { set; get; }
        public MathFunc Function { set; get; }
        public double MinValue { set; get; }
        public double MaxValue { set; get; }
        public int ControlPointNumber { set; get; }
        public int MaxIndividualsPerGeneration { set; get; }
        public int IndividualsInNewGeneration { set; get; }

        // private
        private List<IIndividual> individuals;
        private Random random = new Random();

        public GeneticEngine()
        {
            MaxIndividualsPerGeneration = 50;
            IndividualsInNewGeneration = 100;
        }
        
        public void Start()
        {
            // первое поколение
            GenerateFirst();

            //цикл
            for (int i = 0; i < MaxSteps; i++)
            {
                // скрещивание
                while (individuals.Count < IndividualsInNewGeneration)
                {
                    var individual = CrossFunction(individuals, MaxIndividualsPerGeneration);
                    if (individual != null)
                        individuals.Add(individual);
                }

                // мутация среди потомков
                for (int j = MaxIndividualsPerGeneration; j < IndividualsInNewGeneration; j++) //maxIndividualsPerGeneration
                {
                    if (random.NextDouble() < 0.5)
                        individuals[j].Mutate();
                }

                // отбор
                individuals.ForEach(ind => ind.FitnessValue = GetDelta(ind));
                individuals = SelectionFunction(individuals, MaxIndividualsPerGeneration);

                if (individuals.First().FitnessValue < 0.0001)
                    break;
            }
        }

        // Показывает результат
        public void PrintResult()
        {
            var winner = GetWinner();

            // print formula
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            Console.Write("y = " + winner.Get(0).ToString("F6", cultureInfo));
            for (int i = 1; i < TermsCount; i++)
            {
                Console.Write(" + {0}*x^{1}", winner.Get(i).ToString("F6", cultureInfo), i);
            }
            Console.WriteLine("");
        }

        // Возвращает первого
        public IIndividual GetWinner()
        {
            if (individuals == null || individuals.Count == 0)
                return null;
            return individuals.OrderBy(i => i.FitnessValue).FirstOrDefault();
        }

        // определение первого поколения
        protected void GenerateFirst()
        {
            individuals = new List<IIndividual>();
            for (int i = 0; i < IndividualsInNewGeneration; i++)
            {
                var individualItem = new Individual(TermsCount, random);
                individualItem.Generate();
                individuals.Add(individualItem);
            }

            // отбор первого поколения
            individuals.ForEach(ind => ind.FitnessValue = GetDelta(ind));
            individuals = SelectionFunction(individuals, MaxIndividualsPerGeneration);
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
