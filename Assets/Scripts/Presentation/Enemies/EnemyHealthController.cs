using UnityEngine;

public class EnemyHealthController : HealthController
{
    [SerializeField] private int coinForDeath = 5;

    public void InitHealth()
    {
        _lifeActual = LifeMax;
        ShowLife(_lifeActual, LifeMax);
    }

    public override void Death()
    {
        ObjectPool.Instance.Despawn(ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY, gameObject);
        ObjectPool.Instance.Spawn(ObjectPoolTags.OBJECT_POOL_ENEMY_DEATH_PS, transform.position, Quaternion.Euler(-90, 0, 0));
        _ecomonyService.UpdateCoins(coinForDeath);
    }
}