using System;
using System.Linq;

namespace CubeCoverLib
{
    public class Cube : ICube
    {
        private readonly State[] _stateSet;

        public Cube(byte size)
        {
            _stateSet = new State[size];
            InitSet(_stateSet);
        }

        public Cube(State[] stateSet)
        {
            _stateSet = stateSet;
        }

        public byte Bitness
        {
            get { return (byte) _stateSet.Length; }
        }

        public byte Power
        {
            get { return GetPower(_stateSet); }
        }

        public State this[byte index]
        {
            get { return _stateSet[index]; }
        }


        public bool IsNeighbor(ICube neighborCube)
        {
            if (neighborCube.Bitness != Bitness || neighborCube.Power != Power)
            {
                return false;
            }
            // cubes distinguish by only one coord
            try
            {
                ICube mergedCube = StatewiseMerge(neighborCube);
                return Power + 1 == mergedCube.Power;
            }

            catch (Exception)
            {
                return false;
            }
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

        private static void InitSet(State[] stateSet)
        {
            for (byte i = 0; i < stateSet.Length; i++)
            {
                stateSet[i] = State.F;
            }
        }

        private static byte GetPower(State[] stateSet)
        {
            return (byte) stateSet.Count(s => (s.Equals(State.X)));
        }

        private ICube StatewiseMerge(ICube addendCube)
        {
            if (addendCube.Bitness != Bitness || addendCube.Power != Power)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with similar params are mergeable");
            var tempStateSet = new State[Bitness];
            for (byte i = 0; i < Bitness; i++)
            {
                tempStateSet[i] = _stateSet[i].Merge(addendCube[i]);
            }
            return new Cube(tempStateSet);
        }

        private ICube StatewiseIntersection(ICube addendCube)
        {
            if (addendCube.Bitness != Bitness)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with equal sizes are intersectable");
            var tempStateSet = new State[Bitness];
            for (byte i = 0; i < Bitness; i++)
            {
                tempStateSet[i] = _stateSet[i].Intersection(addendCube[i]);
            }
            return new Cube(tempStateSet);
        }

        private static bool CompareIsSubset(ICube subCube, ICube superCube)
        {
            if (subCube.Bitness != superCube.Bitness || superCube.Power <= subCube.Power)
            {
                return false;
            }
            for (byte i = 0; i < subCube.Bitness; i++)
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

            if (p.Bitness == Bitness && p.Power == Power)
            {
                for (byte i = 0; i < Bitness; i++)
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
            if (p.Bitness == Bitness && p.Power == Power)
            {
                for (byte i = 0; i < Bitness; i++)
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
            int hc = 0;
            for (byte i = 0; i < Bitness; i++)
            {
                hc += _stateSet[i].GetHashCode();
            }
            return hc;
        }
    }
}