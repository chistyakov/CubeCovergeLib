using System;

namespace CubeCoverLib
{
    internal class EmptyCoverage : ICoverage
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

        public ICube[] Except(ICoverage subCov)
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


        public byte Bitness
        {
            get { throw new NotImplementedException(); }
        }

        public bool Contains(ICube contCube)
        {
            throw new NotImplementedException();
        }
    }
}