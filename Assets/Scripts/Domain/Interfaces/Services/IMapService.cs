using UnityEngine;

public interface IMapService
{
    MapData GenerateMapData(int xSize, int ySize);

    Vector3 GetFinaliEnemyPoint();

    Vector3 GetInitialEnemyPoint();
}