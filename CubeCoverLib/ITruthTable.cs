namespace CubeCoverLib
{
    public interface ITruthTable
    {
        bool[,] Table { set; get; }
        byte ArgCount { get; }
        ICoverage GetNullCoverage();
        void Sort();
    }
}