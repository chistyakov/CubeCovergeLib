using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeCoverLib
{
    class EmptyCube : ICube
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

        public override string ToString()
        {
            return "empty cube";
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
    }
}
