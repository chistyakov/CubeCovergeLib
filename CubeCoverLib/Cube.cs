using System;
using System.Linq;

namespace CubeCoverLib
{
    public class Cube : ICube
    {
        private readonly byte _size;

        private readonly State2[] _stateSet;

        public Cube(byte size)
        {
            _stateSet = new State2[size];
            InitSet(_stateSet);
        }

        public Cube(State2[] stateSet)
        {
            _stateSet = stateSet;
            _size = (byte)stateSet.Length;
        }

        public byte Size
        {
            get { return _size; }
        }

        public byte Power
        {
            get { return GetPower(_stateSet); }
        }

        public State2 this[byte index]
        {
            get { return _stateSet[index]; }
        }


        public bool IsNeighbor(ICube neighborCube)
        {
            if (neighborCube.Size != Size || neighborCube.Power != Power)
            {
                return false;
            }
            // cubes distinguish by only one coord
            var mergedCube = StatewiseMerge(neighborCube);
            return Power + 1 == mergedCube.Power;
        }

        public ICube Merge(ICube neighbor)
        {
            if (IsNeighbor(neighbor))
            {
                return StatewiseMerge(neighbor);
            }
            throw new ArgumentOutOfRangeException("neighbor", "cubes aren't neighbors");
        }

        public ICube Intersection(ICube intrsctCube)
        {
            try
            {
                return StatewiseIntersection(intrsctCube);
            }
            catch (InvalidOperationException)
            {
                return new EmptyCube();
            }
        }

        public bool IsSubcube(ICube superCube)
        {
            return CompareIsSubset(this, superCube);
        }

        public bool IsSupercube(ICube subCube)
        {
            return CompareIsSubset(subCube, this);
        }


        public ICube[] Subtract(ICube subCube)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Join(",", _stateSet.Select(s => s.ToString()));
        }

        private static void InitSet(State2[] stateSet)
        {
            for (byte i = 0; i < stateSet.Length; i++)
            {
                stateSet[i] = State2.F;
            }
        }

        private static byte GetPower(State2[] stateSet)
        {
            return (byte)stateSet.Count(s => (s.Equals(State2.X)));
        }

        private ICube StatewiseMerge(ICube addendCube)
        {
            if (addendCube.Size != Size || addendCube.Power != Power)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with similar params are mergeable");
            var tempStateSet = new State2[Size];
            for (byte i = 0; i < Size; i++)
            {
                tempStateSet[i] = _stateSet[i].Intersection(addendCube[i]);
            }
            return new Cube(tempStateSet);
        }

        private ICube StatewiseIntersection(ICube addendCube)
        {
            if (addendCube.Size != Size)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with equal sizes are intersectable");
            var tempStateSet = new State2[Size];
            for (byte i = 0; i < Size; i++)
            {
                tempStateSet[i] = _stateSet[i].Intersection(addendCube[i]);
            }
            return new Cube(tempStateSet);
        }

        private static bool CompareIsSubset(ICube subCube, ICube superCube)
        {
            if (subCube.Size != superCube.Size || superCube.Power <= subCube.Power)
            {
                return false;
            }
            for (byte i = 0; i < subCube.Size; i++)
            {
                if (!subCube[i].IsSubstate(superCube[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as Cube;
            if (p == null)
            {
                return false;
            }

            if (p.Size == _size && p.Power == Power)
            {
                for (byte i = 0; i < _size; i++)
                {
                    if (!_stateSet[i].Equals(p[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool Equals(Cube p)
        {
            if (p == null)
                return true;
            if (p.Size == _size && p.Power == Power)
            {
                for (byte i = 0; i < _size; i++)
                {
                    if (!_stateSet[i].Equals(p[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hc = 0;
            for (byte i = 0; i < _size; i++)
            {
                hc += _stateSet[i].GetHashCode();
            }
            return hc;
        }
    }
}