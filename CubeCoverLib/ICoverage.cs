namespace CubeCoverLib
{
    public interface ICoverage
    {
        ICube[] CubeSet { get; }
        ICube[] Intersection(ICoverage intrsctCov);
        ICube[] Subtract(ICoverage subCov);
        ICube[] GetCubesByPow(byte pow);
    }
}