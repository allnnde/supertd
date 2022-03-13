using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public int sizeWave = 10;
    private int sizeWaveInstacianted;
    private List<GameObject> enemiesInstaciented;

    private float timeForSpawn = 0.5F;
    private float timeForSpawnCheck;

    private IMapService _mapService;

    private void Awake()
    {
        timeForSpawnCheck = timeForSpawn;
        enemiesInstaciented = new List<GameObject>();
    }

    public void Configuration(IMapService mapService)
    {
        _mapService = mapService;
    }

    // Update is called once per frame
    void Update()
    {
        timeForSpawnCheck += Time.deltaTime;
        if (timeForSpawnCheck > timeForSpawn)
        {
            if (sizeWave != sizeWaveInstacianted)
            {
                var obj = ObjectPool.Instance.Spawn(ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY, _mapService.GetInitialEnemyPoint() + Vector3.up, Quaternion.Euler(0, 90, 0));
                enemiesInstaciented.Add(obj);
                sizeWaveInstacianted++;
            }
            timeForSpawnCheck = 0;
        }

        if (enemiesInstaciented.All(p => !p.activeSelf))
        {
            sizeWaveInstacianted = 0;
            enemiesInstaciented.Clear();
        }
    }
}