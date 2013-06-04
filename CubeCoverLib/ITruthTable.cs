namespace CubeCoverLib
{
    public interface ITruthTable
    {
        bool[,] Table { set; get; }
        byte ArgCount { get; }
        ICube[] GetNullCoverage();
        void Sort();
        string ToString();
    }
}