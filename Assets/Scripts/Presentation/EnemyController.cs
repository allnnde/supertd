using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IPoolable
{
    private NavMeshAgent _meshAgent;
    GameObject IPoolable.Owner => gameObject;
    private IMapService _mapService;
    private MapData _map;

    public float Damage = 10;

    public void Configuration(IMapService mapService, MapData map)
    {
        _mapService = mapService;
        _map = map;
    }

    private void Awake()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
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
        _meshAgent.SetDestination(_mapService.GetFinaliEnemyPoint(_map.CellsData));
    }
}