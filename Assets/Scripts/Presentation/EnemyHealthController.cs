using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    public void InitHealth()
    {
        _lifeActual = LifeMax;
    }

    public override void Death()
    {
        ObjectPool.Instance.Despawn(ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY, gameObject);
    }
}