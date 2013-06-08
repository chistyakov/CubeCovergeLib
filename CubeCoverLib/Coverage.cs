using System;
using System.Linq;
using System.Text;

namespace CubeCoverLib
{
    public class Coverage : ICoverage
    {
        protected ICube[] _cubes;

        public Coverage() {}

        public Coverage(ICube[] cubes)
        {
            _cubes = (ICube[]) cubes.Clone();
        }

        public ICube this[byte index]
        {
            get
            {
                if (index >= Size) throw new IndexOutOfRangeException();
                return _cubes[index];
            }
        }

        public byte Size
        {
            get { return (byte) _cubes.Length; }
        }

        public byte MaxPower
        {
            get
            {
                return _cubes.Max(s => s.Power);
            }
        }

        public ICube[] Intersection(ICoverage intrsctCov)
        {
            //if(Size!=intrsctCov.Size)
            //    throw new ArgumentException();
            return  _cubes.Intersect(intrsctCov.ToCubesArray()).ToArray();
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

        public override string ToString()
        {
            //if (_cubes == null) return "null";
            return string.Join("\n", _cubes.Select(s => s.ToString()));
        }
    }
}