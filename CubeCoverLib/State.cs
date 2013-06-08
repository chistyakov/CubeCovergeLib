using System;

namespace CubeCoverLib
{
    public struct State
    {
        // The three possible State values.
        public static readonly State F = new State(0);
        public static readonly State T = new State(1);
        public static readonly State X = new State(2);

        private static readonly State Empty = new State(-1);

        // Private field that stores 0, 1, 2, -1 for F, T, X, Empty.
        readonly sbyte _value;
        // Private instance constructor. The value parameter must be 0, 1 or 2.
        State(int value)
        {
            _value = (sbyte)value;
        }
        public bool IsX { get { return _value == 2; } }
        public bool IsFalse { get { return _value == 0; } }
        public bool IsTrue { get { return _value == 1; } }

        public override bool Equals(object obj)
        {
            if (!(obj is State)) return false;
            return _value == ((State)obj)._value;
        }

        // Implicit conversion from bool to State. Maps true to State.T and
        // false to State.F.
        public static implicit operator State(bool x)
        {
            return x ? T : F;
        }

        public override int GetHashCode()
        {
            return _value;
        }
        public override string ToString()
        {
            if (_value == 0) return "0";
            if (_value == 1) return "1";
            return "x";
        }

        private static readonly State[,] MergeTable =
            {
                // F,   T,      X 
                {F,     X,      Empty}, // F
                {X,     T,      Empty}, // T
                {Empty, Empty,  X}      // X
            };

        private static readonly State[,] IntersectTable =
            {
                // F,   T,      X 
                {F,     Empty,  F},     // F
                {Empty, T,      T},     // T
                {F,     T,      X}      // X
            };

        private static readonly bool[,] SubsetTable =
            {
                // F,   T,      X
                {true,  false,  true},  // F
                {false, true,   true},  // T
                {false, false,  true}   // X
            };

        public State Merge(State s2)
        {
            var res = MergeTable[_value, s2._value];
            if (res.Equals(Empty))
                throw new InvalidOperationException();
            return res;
        }

        public State Intersection(State s2)
        {
            var res = IntersectTable[_value, s2._value];
            if (res.Equals(Empty))
                throw new InvalidOperationException();
            return res;
        }

        public bool IsSubstate(State superS)
        {
            return SubsetTable[_value, superS._value];
        }
    }
}
