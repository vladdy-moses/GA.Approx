using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public class Individual : IIndividual
    {
        private int genesCount;
        private double[] gene;
        private Random random;

        public double FitnessValue { set; get; }

        public Individual(int count, Random rnd)
        {
            genesCount = count;
            gene = new double[count];
            random = rnd;
        }

        public Individual(int count) : this(count, new Random()) { }

        public void Generate()
        {
            for (int i = 0; i < genesCount; i++)
            {
                gene[i] = random.NextDouble() * 10 - 5;
            }
        }

        public void Mutate()
        {
            for (int i = 0; i < genesCount; i++)
            {
                var mutateNum = random.NextDouble() * 4 - 2;
                Set(i, Get(i) * mutateNum);
            }
        }

        public IIndividual Selection(IIndividual partner)
        {
            if (!(partner is Individual))
                throw new NotImplementedException();

            Individual newIndividual = new Individual(genesCount, random);
            for (int i = 0; i < genesCount; i++)
            {
                //var rndVal = (random.NextDouble() < 0.5);
                //newIndividual.Set(i, rndVal ? Get(i) : partner.Get(i));
                newIndividual.Set(i, (this.Get(i) + partner.Get(i)) / 2.0);
            }

            return newIndividual;
        }

        public double Get(int i)
        {
            if (i < 0 || i >= genesCount)
                throw new ArgumentOutOfRangeException();
            return gene[i];
        }

        public void Set(int i, double value)
        {
            if (i < 0 || i >= genesCount)
                throw new ArgumentOutOfRangeException();
            gene[i] = value;
        }

        public override string ToString()
        {
            return this.FitnessValue.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Individual))
                return false;

            var second = obj as Individual;
            for (int i = 0; i < genesCount; i++)
            {
                if (Math.Abs(this.Get(i) - second.Get(i)) > double.Epsilon)
                    return false;
            }

            return true;
        }
    }
}
