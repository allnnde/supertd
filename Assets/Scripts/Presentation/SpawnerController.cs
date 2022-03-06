using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private float timeForSpawn = 1.5F;
    private float timeForSpawnCheck = 1.5F;

    private MapController _mapController;

    // Start is called before the first frame update
    void Awake()
    {
        _mapController = FindObjectOfType<MapController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeForSpawnCheck += Time.deltaTime;
        if (timeForSpawnCheck > timeForSpawn)
        {
            ObjectPool.Instance.Spawn("enemy", _mapController.GetInitialEnemyPoint() + Vector3.up, Quaternion.Euler(0, 90, 0));
            timeForSpawnCheck = 0;
        }
    }
}