using System;
using System.Linq;

namespace CubeCoverLib
{
    public class Coverage : ICoverage
    {
        private ICube[] _cubes;

        public Coverage()
        {
        }

        public Coverage(ICube[] cubes)
        {
            Cubes = cubes;
        }

        protected ICube[] Cubes
        {
            get { return _cubes; }
            set
            {
                if (!CheckBitness(value))
                    throw new ArgumentException();
                _cubes = value;
            }
        }

        public ICube this[byte index]
        {
            get
            {
                //if (index >= Size) throw new IndexOutOfRangeException();
                return Cubes[index];
            }
        }

        public byte Size
        {
            get { return (byte) Cubes.Length; }
        }

        public byte MaxPower
        {
            get { return Cubes.Max(s => s.Power); }
        }

        public ICube[] Intersection(ICoverage intrsctCov)
        {
            if (Bitness != intrsctCov.Bitness)
                throw new ArgumentException();
            return Cubes.Intersect(intrsctCov.ToCubesArray()).ToArray();
        }

        public ICube[] Except(ICoverage excptCov)
        {
            if (Bitness != excptCov.Bitness)
                throw new ArgumentException();
            return Cubes.Except(excptCov.ToCubesArray()).ToArray();
        }

        public ICube[] GetCubesByPow(byte pow)
        {
            if (Size == 0)
            {
                return (new EmptyCoverage()).ToCubesArray();
            }

            return Cubes.Where(s => s.Power == pow).ToArray();
        }


        public ICube[] ToCubesArray()
        {
            return (ICube[]) Cubes.Clone();
        }

        public override string ToString()
        {
            if (Cubes == null) return (new EmptyCoverage().ToString());
            return string.Join("\n", Cubes.Select(s => s.ToString()));
        }

        public bool Contains(ICube contCube)
        {
            return contCube.Bitness == Bitness && Cubes.Contains(contCube);
        }

        public byte Bitness
        {
            get { return Cubes[0].Bitness; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as Coverage;
            if (p == null)
            {
                return false;
            }

            return (p.Size == Size && Intersection(p).Length == Size);
        }

        public bool Equals(ICoverage coverage)
        {
            return (coverage.Bitness == Bitness &&
                    coverage.Size == Size &&
                    Intersection(coverage).Length == Size);
        }

        public override int GetHashCode()
        {
            int hc = 0;
            for (byte i = 0; i < Size; i++)
            {
                hc += Cubes[i].GetHashCode();
            }
            return hc;
        }

        private static bool CheckBitness(ICube[] cubes)
        {
            if (cubes.Length == 0)
                return true;
            byte bitness = cubes[0].Bitness;
            return cubes.All(s => s.Bitness == bitness);
        }

        public bool IsSubCoverage(ICoverage superCov)
        {
            if (Bitness != superCov.Bitness)
                throw new ArgumentException();
            return Intersection(superCov).Equals(this);
        }

        public bool IsSuperCoverage(ICoverage subCov)
        {
            if (Bitness != subCov.Bitness)
                throw new ArgumentException();
            return Intersection(subCov).Equals(subCov);
        }

        public static Coverage GetNullCoverage(ITruthTable truthTable)
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
            return new Coverage(cubes);
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