using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeCoverLib
{
    public class CubesComplex : Coverage
    {
        public CubesComplex() {}

        public CubesComplex(ICube[] cubes)
        {
            Cubes = cubes;
        }

        public bool IsCubeMax(ICube maxCube)
        {
            throw new NotImplementedException();
        }

        public Coverage GetMaxCubesCoverage()
        {
            throw new NotImplementedException();
        }

        public static CubesComplex GetCubesComplex(ITruthTable truthTable)
        {
            return GetCubesComplex(Coverage.GetNullCoverage(truthTable));
        }

        public static CubesComplex GetCubesComplex(Coverage nullCoverage)
        {
            var cubesComplex = new List<ICube>(nullCoverage.ToCubesArray());
            var mrgdCubesPow = new List<ICube>(nullCoverage.ToCubesArray());
            for (byte pow = 0; pow < nullCoverage.Size; pow++)
            {
                mrgdCubesPow = new List<ICube>(GetMergedMajorCubes(mrgdCubesPow.ToArray()));
                if(mrgdCubesPow.Count == 0) break;
                cubesComplex.AddRange(mrgdCubesPow);
            }
            return new CubesComplex(cubesComplex.ToArray());
        }
        private static ICube[] GetMergedMajorCubes(ICube[] minorCubes)
        {
            var mrdCubes = new List<ICube>();
            for (byte i = 0; i < minorCubes.Length; i++)
            {
                for (byte j = i; j < minorCubes.Length; j++)
                {
                    if (minorCubes[i].IsNeighbor(minorCubes[j]))
                    {
                        mrdCubes.Add(minorCubes[i].Merge(minorCubes[j]));
                    }
                }
            }
            return mrdCubes.Distinct().ToArray();
        }
    }
}