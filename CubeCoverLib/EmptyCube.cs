using System;

namespace CubeCoverLib
{
    internal class EmptyCube : ICube
    {
        public byte Size
        {
            get { return 0; }
        }

        public byte Power
        {
            get { return 0; }
        }

        public State[] StateSet
        {
            get { return null; }
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

        public bool IsValid()
        {
            return true;
        }

        public bool IsEmpty()
        {
            return true;
        }


        public bool IsSubcube(ICube superCube)
        {
            return true;
        }

        public bool IsSupercube(ICube subCube)
        {
            return false;
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