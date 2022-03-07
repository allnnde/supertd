using UnityEngine;

public interface IMapService
{
    MapData GenerateMapData(int xSize, int ySize);

    Vector3 GetFinaliEnemyPoint(CellData[,] map);

    Vector3 GetInitialEnemyPoint(CellData[,] map);
}