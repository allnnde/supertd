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
    [SerializeField] private EnemyDeathPS _enemyDeathPS;

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

        var _buildObjPool = new Dictionary<BuildTypeEnum, string>();
        _buildObjPool.Add(BuildTypeEnum.Tower_Ballesta, ObjectPoolTags.OBJECT_POOL_TOWER_BALLESTA);
        _buildObjPool.Add(BuildTypeEnum.Mural, ObjectPoolTags.OBJECT_POOL_STRUCTURE_MURAL);

        var _buildTypeCost = new Dictionary<BuildTypeEnum, int>();
        _buildTypeCost.Add(BuildTypeEnum.Tower_Ballesta, 30);
        _buildTypeCost.Add(BuildTypeEnum.Mural, 200);

        ObjectPool.Instance.Pools.Add(new Pool() { Item = _enemyModel, Parent = transform, Size = 10, Tag = ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY });
        ObjectPool.Instance.Pools.Add(new Pool() { Item = _towerModel, Parent = transform, Size = 20, Tag = ObjectPoolTags.OBJECT_POOL_TOWER_BALLESTA });
        ObjectPool.Instance.Pools.Add(new Pool() { Item = _muralModel, Parent = transform, Size = 20, Tag = ObjectPoolTags.OBJECT_POOL_STRUCTURE_MURAL });
        ObjectPool.Instance.Pools.Add(new Pool() { Item = _enemyDeathPS, Parent = transform, Size = 5, Tag = ObjectPoolTags.OBJECT_POOL_ENEMY_DEATH_PS });
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

        var economyController = FindObjectOfType<EcomonyController>();
        var economyServices = new EconomyService(economyController);
        economyController.Configuration(economyServices);

        //TODO los enemigos al tenes dos scritps, uno hace el OnInstaciate y el otro no
        // esto puede suponer un error, ver de unificar o
        // de mejorar como esta distribuida la logica del pool
        var healthController = FindObjectsOfType<HealthController>(true);
        foreach (var item in healthController)
        {
            var healthServices = new HealthServices(item);
            item.Configuration(healthServices, economyServices);
        }

        var builderController = FindObjectsOfType<BuilderController>(true);
        foreach (var item in builderController)
        {
            item.Configuration(_buildLayer, _buildObjPool, _buildTypeCost, economyServices);
        }

        var cameraController = FindObjectsOfType<CameraController>(true);
        foreach (var item in cameraController)
        {
            var cameraSevices = new CameraService(item);
            item.Configuration(cameraSevices);
        }
    }
}