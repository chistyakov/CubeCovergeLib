using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeCoverLib
{
    class EmptyCoverage : ICoverage
    {
        public ICube this[byte index]
        {
            get { throw new NotImplementedException(); }
        }

        public byte Size
        {
            get { throw new NotImplementedException(); }
        }

        public byte MaxPower
        {
            get { throw new NotImplementedException(); }
        }

        public ICube[] Intersection(ICoverage intrsctCov)
        {
            throw new NotImplementedException();
        }

        public ICube[] Subtract(ICoverage subCov)
        {
            throw new NotImplementedException();
        }

        public ICube[] GetCubesByPow(byte pow)
        {
            throw new NotImplementedException();
        }


        public ICube[] ToCubesArray()
        {
            throw new NotImplementedException();
        }
    }
}
