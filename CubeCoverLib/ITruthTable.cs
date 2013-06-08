namespace CubeCoverLib
{
    public interface ITruthTable
    {
        bool this [byte index1, byte index2] { get; }
        byte ArgCount { get; }
        void Sort();
        string ToString();
        byte GetColCount();
        byte GetRowCount();
    }
}