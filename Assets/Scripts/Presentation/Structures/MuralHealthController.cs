using UnityEngine;

public class MuralHealthController : StructureHealthController, IPoolable
{
    GameObject IPoolable.Owner => gameObject;

    public override void Death()
    {
        ObjectPool.Instance.Despawn(ObjectPoolTags.OBJECT_POOL_STRUCTURE_MURAL, gameObject);
    }

    void IPoolable.OnDespawn()
    {
        gameObject.SetActive(false);
    }

    void IPoolable.OnInstanciate(Transform parent)
    {
        //TODO ver de mejorar esta injeccion, exponiendo datos en gameManager
        Configuration(new HealthServices(this), new EconomyService(FindObjectOfType<EcomonyController>()));
        transform.parent = parent;
        gameObject.SetActive(false);
    }

    void IPoolable.OnSpawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }
}