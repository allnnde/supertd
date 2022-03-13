using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathPS : MonoBehaviour, IPoolable
{
    GameObject IPoolable.Owner => gameObject;
    private ParticleSystem _explosion;

    [SerializeField]
    private float _lifeMaxSeg = 5;

    private float _lifeTime;

    void IPoolable.OnDespawn()
    {
        gameObject.SetActive(false);
    }

    void IPoolable.OnInstanciate(Transform parent)
    {
        transform.parent = parent;
        _explosion = GetComponent<ParticleSystem>();
        gameObject.SetActive(false);
    }

    void IPoolable.OnSpawn(Vector3 position, Quaternion rotation)
    {
        _lifeTime = 0;
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
        _explosion.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _lifeMaxSeg)
        {
            ObjectPool.Instance.Despawn(ObjectPoolTags.OBJECT_POOL_ENEMY_DEATH_PS, gameObject);
        }
    }
}