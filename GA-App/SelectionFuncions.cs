using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public static class SelectionFuncions
    {
        static Random random = new Random();

        static public void First(List<IIndividual> individuals, int parentsCount)
        {
            while (true)
            {
                var p1Index = random.Next() % parentsCount;
                var p2Index = random.Next() % parentsCount;

                var partner1 = individuals[p1Index];
                var partner2 = individuals[p2Index];

                if (p1Index != p2Index && !partner1.Equals(partner2))
                {
                    individuals.Add(partner1.Selection(partner2));
                    return;
                }
            }
        }

        static public void Second(List<IIndividual> individuals, int parentsCount)
        {
            individuals.Add(individuals[0].Selection(individuals[1]));
            return;
        }
    }
}
