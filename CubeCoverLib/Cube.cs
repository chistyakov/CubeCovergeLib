using System;
using System.Linq;

namespace CubeCoverLib
{
    public class Cube : ICube
    {
        private readonly byte _size;

        private readonly State[] _stateSet;
        private byte _power;

        public Cube(byte size)
        {
            _power = 0;
            _stateSet = new State[size];
            InitSet(_stateSet);
        }

        public Cube(State[] stateSet)
        {
            _stateSet = stateSet;
            _size = (byte) stateSet.Length;
        }

        public byte Size
        {
            get { return _size; }
        }

        public byte Power
        {
            get { return GetPower(StateSet); }
        }

        public State[] StateSet
        {
            get { return _stateSet; }
        }


        public bool IsNeighbor(ICube neighborCube)
        {
            if (neighborCube.Size != Size || neighborCube.Power != Power)
            {
                return false;
            }
            // cubes distinguish by only one coord
            ICube mergedCube = StatewiseMerge(neighborCube);
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
            var isEmpty = StatewiseIntersection(intrsctCube).IsEmpty();
            return isEmpty ? new EmptyCube() : StatewiseIntersection(intrsctCube);
        }

        public bool IsValid()
        {
            return !Array.Exists(_stateSet, (State s) => s == State.Invalid);
        }

        public bool IsEmpty()
        {
            return Array.Exists(_stateSet, (State s) => s == State.Empty);
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
            return string.Join(",", _stateSet.Select(s => StateUtil.GetDescription(s)));
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
            return (byte) stateSet.Count(s => (s == State.X));
        }

        private ICube StatewiseMerge(ICube addendCube)
        {
            if (addendCube.Size != Size || addendCube.Power != Power)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with similar params are mergeable");

            ICube resCube = new Cube(Size);
            for (byte i = 0; i < Size; i++)
            {
                resCube.StateSet[i] = StateUtil.Merge(StateSet[i], addendCube.StateSet[i]);
            }
            return resCube;
        }

        private ICube StatewiseIntersection(ICube addendCube)
        {
            if (addendCube.Size != Size)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with equal sizes are intersectable");
            ICube resCube = new Cube(Size);
            for (byte i = 0; i < Size; i++)
            {
                resCube.StateSet[i] = StateUtil.Intersection(StateSet[i], addendCube.StateSet[i]);
            }
            return resCube;
        }

        private static bool CompareIsSubset(ICube subCube, ICube superCube)
        {
            if (subCube.Size != superCube.Size || superCube.Power <= subCube.Power)
            {
                return false;
            }
            for (byte i = 0; i < subCube.Size; i++)
            {
                if (!StateUtil.IsSubset(subCube.StateSet[i], superCube.StateSet[i]))
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

            return _stateSet.SequenceEqual(p.StateSet);

            //if (p.Size == this.size && p.Power == this.power)
            //{
            //    for (byte i = 0; i < this.Size; i++)
            //    {
            //        if (this.StateSet[i] != p.StateSet[i])
            //        {
            //            return false;
            //        }
            //    }
            //    return true;
            //}
            //return false;
        }

        public bool Equals(Cube p)
        {
            return p != null && _stateSet.SequenceEqual(p.StateSet);
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