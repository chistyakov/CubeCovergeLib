namespace CubeCoverLib
{
    public interface ITruthTable
    {

        bool this [byte index1, byte index2] { get; }
        byte ArgCount { get; }
        ICube[] GetNullCoverage();
        void Sort();
        string ToString();
    }
}