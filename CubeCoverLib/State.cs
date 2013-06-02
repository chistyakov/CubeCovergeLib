using System;
using System.ComponentModel;

namespace CubeCoverLib
{
    public enum State : byte
    {
        [Description("0")]
        F = 0x00,
        [Description("1")]
        T = 0x01,
        [Description("x")]
        X = 0x02,
        [Description("e")]
        Empty = 0x03,
        [Description("i")]
        Invalid = 0x04
    }
    static class StateUtil
    {
        public static string GetDescription(State value)
        {
            System.Reflection.FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        private static State[,] mergeTable = {  // F,           T,              X 
                                                {State.F,       State.X,        State.Invalid}, // F
                                                {State.X,       State.T,        State.Invalid}, // T
                                                {State.Invalid, State.Invalid,  State.X}        // X
                                             };

        private static State[,] intersectTable = {// F,         T,              X 
                                                 {State.F,      State.Empty,    State.F},       // F
                                                 {State.Empty,  State.T,        State.T},       // T
                                                 {State.F,      State.T,        State.X}        // X
                                                 };
        private static bool[,] subsetTable = { // F,            T,              X
                                                 {true,         false,          true},          // F
                                                 {false,        true,           true},          // T
                                                 {false,        false,          true}           // X
                                             };

        public static State Merge(State s1, State s2)
        {
            if (IsBasis(s1) && IsBasis(s2))
            {
                return mergeTable[(byte)s1, (byte)s2];
            }
            else
            {
                throw new ArgumentOutOfRangeException("States should be in basis (T, F, X)");
            }
        }
        public static State Intersection(State s1, State s2)
        {
            if (IsBasis(s1) && IsBasis(s2))
            {
                return intersectTable[(byte)s1, (byte)s2];
            }
            else
            {
                throw new ArgumentOutOfRangeException("States should be in basis (T, F, X)");
            }
        }
        private static bool IsBasis(State s)
        {
            return (s == State.F || s == State.T || s == State.X);
        }

        public static bool IsSubset(State subS, State superS)
        {
            if (IsBasis(subS) && IsBasis(superS))
            {
                return subsetTable[(byte)subS, (byte)superS];
            }
            else
            {
                throw new ArgumentOutOfRangeException("States should be in basis (T, F, X)");
            }
        }
    }
}
