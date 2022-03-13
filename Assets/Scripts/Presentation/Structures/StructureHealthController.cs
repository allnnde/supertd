using UnityEngine;

public class StructureHealthController : HealthController
{
    public override void Death()
    {
        Debug.Log("Game Over");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.TAG_ENEMY))
        {
            var enemy = other.GetComponent<EnemyController>();
            TakeDamage(enemy.Damage);
            ObjectPool.Instance.Despawn(ObjectPoolTags.OBJECT_POOL_SKELETON_ENEMY, other.gameObject);
        }
    }
}