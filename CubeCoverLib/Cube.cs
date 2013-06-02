using System;
using System.Linq;

namespace CubeCoverLib
{
    public class Cube : ICube
    {
        private byte size;
        public byte Size
        {
            get { return size; }
        }

        private byte power;
        public byte Power
        {
            get
            {
                return GetPower(StateSet);
            }
            private set
            {
                if (power <= Size)
                {
                    power = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("power", "Power is excess of size");
                }
            }
        }

        private State[] stateSet;
        public State[] StateSet
        {
            get { return stateSet; }
        }

        public Cube(byte size)
        {
            power = 0;
            stateSet = new State[size];
            InitSet(stateSet);
        }
        public Cube(State[] stateSet)
        {
            this.stateSet = stateSet;
            this.size = (byte)stateSet.Length;
        }


        public override string ToString()
        {
            return string.Join(",", stateSet.Select(s => StateUtil.GetDescription(s)));
        }

        private static void InitSet(State[] stateSet)
        {
            for (byte i = 0; i < stateSet.Length; i++)
            {
                stateSet[i] = State.F;
            }
        }

        private static byte GetPower(State[] StateSet)
        {
            return (byte)StateSet.Count(s => (s == State.X));
        }

        private ICube StatewiseMerge(ICube addendCube)
        {
            if (addendCube.Size != this.Size || addendCube.Power != this.Power)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with similar params are mergeable");

            ICube resCube = new Cube(this.Size);
            for (byte i = 0; i < this.Size; i++)
            {
                resCube.StateSet[i] = StateUtil.Merge(this.StateSet[i], addendCube.StateSet[i]);
            }
            return resCube;
        }

        private ICube StatewiseIntersection(ICube addendCube)
        {
            if (addendCube.Size != this.Size)
                throw new ArgumentOutOfRangeException("addendCube", "Only cubes with equal sizes are intersectable");
            else
            {
                ICube resCube = new Cube(this.Size);
                for (byte i = 0; i < this.Size; i++)
                {
                    resCube.StateSet[i] = StateUtil.Intersection(this.StateSet[i], addendCube.StateSet[i]);
                }
                return resCube;
            }
        }

        public bool IsNeighbor(ICube neighborCube)
        {
            if (neighborCube.Size != this.Size || neighborCube.Power != this.Power)
            {
                return false;
            }
            // cubes distinguish by only one coord
            ICube mergedCube = this.StatewiseMerge(neighborCube);
            return this.Power + 1 == mergedCube.Power;
        }

        public ICube Merge(ICube neighbor)
        {
            if (IsNeighbor(neighbor))
            {
                return StatewiseMerge(neighbor);
            }
            else
            {
                throw new ArgumentOutOfRangeException("cubes aren't neighbors");
            }
        }

        public ICube Intersection(ICube intrsctCube)
        {
            if (StatewiseIntersection(intrsctCube).IsEmpty())
                return new EmptyCube();
            else
                return StatewiseIntersection(intrsctCube);
        }

        public bool IsValid()
        {
            return !Array.Exists(stateSet, (State s) => s == State.Invalid);
        }

        public bool IsEmpty()
        {
            return Array.Exists(stateSet, (State s) => s == State.Empty);
        }

        private static bool CompareIsSubset(ICube subCube, ICube superCube)
        {
            if (subCube.Size != superCube.Size)
            {
                return false;
            }
            else
            {
                if (superCube.Power <= subCube.Power)
                {
                    return false;
                }
                else
                {
                    for (byte i = 0; i < subCube.Size; i++)
                    {
                        if (!StateUtil.IsSubset(subCube.StateSet[i], superCube.StateSet[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Cube p = obj as Cube;
            if ((System.Object)p == null)
            {
                return false;
            }

            return stateSet.SequenceEqual(p.StateSet);

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
            if ((object)p == null)
            {
                return false;
            }

            return stateSet.SequenceEqual(p.StateSet);
        }

        public override int GetHashCode()
        {
            int hc = 0;
            for (byte i = 0; i < size; i++)
            {
                hc += stateSet[i].GetHashCode();
            }
            return hc;
        }
    }
}
