using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyModel;
    [SerializeField] private MuralHealthController _muralModel;
    [SerializeField] private TowerController _towerModel;
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

        var _buildLayer = new Dictionary<BuildTypeEnum, string>();
        _buildLayer.Add(BuildTypeEnum.Mural, LayerNames.LAYER_MAP_STREET);
        _buildLayer.Add(BuildTypeEnum.Tower_Ballesta, LayerNames.LAYER_MAP_GROUND);

        ObjectPool.Instance.Pools.Add(new Pool() { Item = _enemyModel, Parent = transform, Size = 50, Tag = ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY });
        ObjectPool.Instance.Pools.Add(new Pool() { Item = _towerModel, Parent = transform, Size = 20, Tag = ObjectPoolTags.OBJECT_POOL_TOWER_BALLESTA });
        ObjectPool.Instance.Pools.Add(new Pool() { Item = _muralModel, Parent = transform, Size = 20, Tag = ObjectPoolTags.OBJECT_POOL_STRUCTURE_MURAL });
        ObjectPool.Instance.Start();

        var mapServices = new MapService();
        var mapData = mapServices.GenerateMapData(_xSize, _ySize);

        var mapController = FindObjectsOfType<MapController>(true);
        foreach (var item in mapController)
        {
            item.Configuration(mapData, _cellTypeObj);
        }

        var enemiesController = FindObjectsOfType<EnemyController>(true);
        foreach (var item in enemiesController)
        {
            item.Configuration(mapServices);
        }

        var spawnersController = FindObjectsOfType<SpawnerController>(true);
        foreach (var item in spawnersController)
        {
            item.Configuration(mapServices);
        }

        var healthController = FindObjectsOfType<HealthController>(true);
        foreach (var item in healthController)
        {
            var healthServices = new HealthServices(item);
            item.Configuration(healthServices);
        }
        var builderController = FindObjectsOfType<BuilderController>(true);
        foreach (var item in builderController)
        {
            item.Configuration(_buildLayer);
        }
    }
}