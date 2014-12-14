using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.App
{
    public delegate double MathFunc(double num);
    public delegate void SelectionFunc(List<IIndividual> individuals, int parentsCount);
}
