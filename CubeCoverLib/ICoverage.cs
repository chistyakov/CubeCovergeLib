namespace CubeCoverLib
{
    public interface ICoverage
    {
        ICube this[byte index] { get; }
        byte Size { get; }
        byte MaxPower { get; }
        byte Bitness { get; }
        ICube[] ToCubesArray();
        ICube[] Intersection(ICoverage intrsctCov);
        ICube[] Except(ICoverage subCov);
        ICube[] GetCubesByPow(byte pow);
        string ToString();
        bool Contains(ICube contCube);
    }
}