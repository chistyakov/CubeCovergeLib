namespace CubeCoverLib
{
    public interface ICube
    {
        byte Size { get; }
        byte Power { get; }
        State this[byte index] { get; }
        ICube Merge(ICube neighborCube);
        ICube Intersection(ICube intrsctCube);
        bool IsNeighbor(ICube neighborCube);
        bool IsSubcube(ICube superCube);
        bool IsSupercube(ICube subCube);
        ICube[] Subtract(ICube subCube);
        string ToString();
    }
}