using System;

namespace CubeCoverLib
{
    internal class CubesComplex : Coverage
    {
        private ICube[] _cubes;

        public CubesComplex() {}

        public CubesComplex(ICube[] cubes)
        {
            _cubes = cubes;
        }

        public bool IsCubeMax(ICube maxCube)
        {
            return true;
        }

        public Coverage GetMaxCubesCoverage()
        {
            throw new NotImplementedException();
        }
    }
}