using System;
using System.Linq;

namespace CubeCoverLib
{
    class Coverage : ICoverage
    {
        private readonly ICube[] _cubes;

        public Coverage() {}

        public Coverage(ICube[] cubes)
        {
            _cubes = cubes;
        }

        public ICube this[byte index]
        {
            get { throw new NotImplementedException(); }
        }

        public byte Size
        {
            get { return (byte) _cubes.Length; }
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
            if (Size == 0)
            {
                return (new EmptyCoverage()).ToCubesArray();
            }

            return _cubes.Where(s => s.Power == pow).ToArray();
        }


        public ICube[] ToCubesArray()
        {
            return _cubes;
        }
    }
}