using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private float timeForSpawn = 1.5F;
    private float timeForSpawnCheck = 1.5F;

    private IMapService _mapService;

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
            ObjectPool.Instance.Spawn(ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY, _mapService.GetInitialEnemyPoint() + Vector3.up, Quaternion.Euler(0, 90, 0));
            timeForSpawnCheck = 0;
        }
    }
}