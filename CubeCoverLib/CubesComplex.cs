using System;
using System.Linq;

namespace CubeCoverLib
{
    public class CubesComplex : Coverage
    {
        public CubesComplex() {}

        public CubesComplex(ICube[] cubes)
        {
            _cubes = cubes;
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
            var cubes = new ICube[GetSgnfcntCornersCnt(truthTable)];
            for (byte i = 0, cubesI = 0; i < truthTable.GetRowCount(); i++)
            {
                if (!truthTable[i, truthTable.ArgCount]) continue;
                var row = new State[truthTable.ArgCount];
                for (byte j = 0; j < truthTable.ArgCount; j++)
                {
                    row[j] = truthTable[i, j];
                }
                cubes[cubesI] = new Cube(row);
                cubesI++;
            }
            return new CubesComplex(cubes);
        }

        private static byte GetSgnfcntCornersCnt(ITruthTable truthTable)
        {
            byte sgnfcntCubeCornersCnt = 0;
            for (byte i = 0; i < truthTable.GetRowCount(); i++)
            {
                if (truthTable[i, truthTable.ArgCount]) sgnfcntCubeCornersCnt++;
            }
            return sgnfcntCubeCornersCnt;
        }
    }
}