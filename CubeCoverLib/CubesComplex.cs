﻿using System;

namespace CubeCoverLib
{
    class CubesComplex : Coverage
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