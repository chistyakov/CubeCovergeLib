using System;
using System.ComponentModel;
using System.Reflection;

namespace CubeCoverLib
{
    public enum State : byte
    {
        [Description("0")] F = 0x00,
        [Description("1")] T = 0x01,
        [Description("x")] X = 0x02,
        [Description("e")] Empty = 0x03,
        [Description("i")] Invalid = 0x04
    }

    internal static class StateUtil
    {
        private static readonly State[,] MergeTable =
            {
                // F,           T,              X 
                {State.F,       State.X,        State.Invalid}, // F
                {State.X,       State.T,        State.Invalid}, // T
                {State.Invalid, State.Invalid,  State.X}        // X
            };

        private static readonly State[,] IntersectTable =
            {
                // F,           T,              X 
                {State.F,       State.Empty,    State.F},       // F
                {State.Empty,   State.T,        State.T},       // T
                {State.F,       State.T,        State.X}        // X
            };

        private static readonly bool[,] SubsetTable =
            {
                // F,            T,             X
                {true,          false,          true},          // F
                {false,         true,           true},          // T
                {false,         false,          true}           // X
            };

        public static string GetDescription(State value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute
                = Attribute.GetCustomAttribute(field, typeof (DescriptionAttribute))
                  as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static State Merge(State s1, State s2)
        {
            if (IsBasis(s1) && IsBasis(s2))
            {
                return MergeTable[(byte) s1, (byte) s2];
            }
            throw new ArgumentOutOfRangeException("s1");
        }

        public static State Intersection(State s1, State s2)
        {
            if (IsBasis(s1) && IsBasis(s2))
            {
                return IntersectTable[(byte) s1, (byte) s2];
            }
            throw new ArgumentOutOfRangeException("s1", "States should be in basis (T, F, X)");
        }

        private static bool IsBasis(State s)
        {
            return (s == State.F || s == State.T || s == State.X);
        }

        public static bool IsSubset(State subS, State superS)
        {
            if (IsBasis(subS) && IsBasis(superS))
            {
                return SubsetTable[(byte) subS, (byte) superS];
            }
            throw new ArgumentOutOfRangeException("subS", "States should be in basis (T, F, X)");
        }
    }
}