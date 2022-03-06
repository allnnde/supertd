using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IPoolable
{
    private NavMeshAgent _meshAgent;
    private MapController _mapController;

    GameObject IPoolable.Owner => gameObject;

    private void Awake()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _mapController = FindObjectOfType<MapController>();
    }

    void IPoolable.OnDespawn()
    {
        gameObject.SetActive(false);
    }

    void IPoolable.OnInstanciate(Transform parent)
    {
        transform.parent = parent;
        gameObject.SetActive(false);
    }

    void IPoolable.OnSpawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
        _meshAgent.SetDestination(_mapController.GetFinaliEnemyPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (_meshAgent.remainingDistance < .01F)
        {
            ObjectPool.Instance.Despawn("enemy", gameObject);
        }
    }
}