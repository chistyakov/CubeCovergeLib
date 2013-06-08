namespace CubeCoverLib
{
    public interface ICoverage
    {
        ICube this[byte index] { get; }
        byte Size { get; }
        byte MaxPower { get; }
        ICube[] ToCubesArray();
        ICube[] Intersection(ICoverage intrsctCov);
        ICube[] Subtract(ICoverage subCov);
        ICube[] GetCubesByPow(byte pow);
        string ToString();
    }
}