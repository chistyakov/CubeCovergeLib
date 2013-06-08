using System;

namespace CubeCoverLib
{
    internal class EmptyCube : ICube
    {
        public byte Bitness
        {
            get { return 0; }
        }

        public byte Power
        {
            get { return 0; }
        }

        public ICube Merge(ICube neighborCube)
        {
            return this;
        }

        public ICube Intersection(ICube intrsctCube)
        {
            return this;
        }

        public bool IsNeighbor(ICube neighborCube)
        {
            return false;
        }

        public bool IsSubcube(ICube superCube)
        {
            return true;
        }

        public bool IsSupercube(ICube subCube)
        {
            return subCube.GetType() == typeof(EmptyCube);
        }

        public ICube[] Subtract(ICube subCube)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "empty cube";
        }


        public State this[byte index]
        {
            get { throw new IndexOutOfRangeException(); }
        }
    }
}