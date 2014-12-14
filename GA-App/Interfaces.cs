using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public interface IIndividual
    {
        double FitnessValue { set; get; }
        void Generate();
        void Mutate();
        IIndividual Selection(IIndividual partner);
        double Get(int i);
        void Set(int i, double value);
    }
}
