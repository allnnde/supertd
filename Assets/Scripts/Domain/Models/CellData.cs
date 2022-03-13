using UnityEngine;

public class CellData
{
    public CellData(CellType type, Vector3 position, string name)
    {
        CellType = type;
        Position = position;
        Name = name;
    }

    public CellType CellType { get; set; }
    public bool IsInitialPath { get; set; }
    public bool IsFinalyPath { get; set; }
    public Vector3 Position { get; set; }
    public string Name { get; set; }
}