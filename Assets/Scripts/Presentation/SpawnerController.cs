using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private float timeForSpawn = 1.5F;
    private float timeForSpawnCheck = 1.5F;

    private IMapService _mapService;
    private MapData _map;

    public void Configuration(IMapService mapService, MapData map)
    {
        _mapService = mapService;
        _map = map;
    }

    // Update is called once per frame
    void Update()
    {
        timeForSpawnCheck += Time.deltaTime;
        if (timeForSpawnCheck > timeForSpawn)
        {
            ObjectPool.Instance.Spawn("enemy", _mapService.GetInitialEnemyPoint(_map.CellsData) + Vector3.up, Quaternion.Euler(0, 90, 0));
            timeForSpawnCheck = 0;
        }
    }
}