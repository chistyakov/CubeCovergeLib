using System;

namespace CubeCoverLib
{
    internal class EmptyCoverage : ICoverage
    {
        public ICube this[byte index]
        {
            get { throw new IndexOutOfRangeException(); }
        }

        public byte Size
        {
            get { return 0; }
        }

        public byte MaxPower
        {
            get { return 0; }
        }

        public ICube[] Intersection(ICoverage intrsctCov)
        {
            return ToCubesArray();
        }

        public ICube[] Except(ICoverage subCov)
        {
            return ToCubesArray();
        }

        public ICube[] GetCubesByPow(byte pow)
        {
            return ToCubesArray();
        }


        public ICube[] ToCubesArray()
        {
            return null;
        }


        public byte Bitness
        {
            get { return 0; }
        }

        public bool Contains(ICube contCube)
        {
            return false;
        }


        public bool IsSubCoverage(ICoverage superCov)
        {
            return true;
        }

        public bool IsSuperCoverage(ICoverage subCov)
        {
            return subCov.GetType() == typeof(EmptyCube);
        }
    }
}