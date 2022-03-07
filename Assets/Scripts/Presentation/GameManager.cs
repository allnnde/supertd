using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyModel;
    [SerializeField] private GameObject _cellGroundModel;
    [SerializeField] private GameObject _cellStraightModel;
    [SerializeField] private GameObject _cellCastleModel;

    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;

    [SerializeField] public MapData aaa;

    void Awake()
    {
        var _cellTypeObj = new Dictionary<CellType, GameObject>();
        _cellTypeObj.Add(CellType.Street, _cellStraightModel);
        _cellTypeObj.Add(CellType.Ground, _cellGroundModel);
        _cellTypeObj.Add(CellType.Castle, _cellCastleModel);

        ObjectPool.Instance.Pools.Add(new Pool() { Item = _enemyModel, Parent = transform, Size = 50, Tag = "enemy" });
        ObjectPool.Instance.Start();

        var mapServices = new MapService();
        var mapData = mapServices.GenerateMapData(_xSize, _ySize);

        var mapController = FindObjectsOfType<MapController>(true);
        foreach (var item in mapController)
        {
            item.Configuration(mapData, _cellTypeObj);
        }

        var emeniesController = FindObjectsOfType<EnemyController>(true);
        foreach (var item in emeniesController)
        {
            item.Configuration(mapServices, mapData);
        }

        var spawnersController = FindObjectsOfType<SpawnerController>(true);
        foreach (var item in spawnersController)
        {
            item.Configuration(mapServices, mapData);
        }
    }
}