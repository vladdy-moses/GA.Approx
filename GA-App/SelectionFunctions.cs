using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public static class SelectionFunctions
    {
        static public List<IIndividual> First(List<IIndividual> individuals, int maxIndividuals)
        {
            return individuals.OrderBy(ind => ind.FitnessValue).Take(maxIndividuals).ToList();
        }
    }
}
