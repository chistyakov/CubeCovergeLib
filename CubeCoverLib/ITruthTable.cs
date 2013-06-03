using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeCoverLib
{
    public interface ITruthTable
    {
        bool[,] Table { set; get; }
        byte ArgCount { get; }
        Coverage GetNullCoverage();
        void Sort();
    }
}
