using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IPoolable
{
    private NavMeshAgent _meshAgent;
    GameObject IPoolable.Owner => gameObject;
    private IMapService _mapService;
    private EnemyHealthController enemyHealthController;

    public float Damage = 10;

    public void Configuration(IMapService mapService)
    {
        _mapService = mapService;
    }

    private void Awake()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        enemyHealthController = GetComponent<EnemyHealthController>();
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
        enemyHealthController.InitHealth();
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
        _meshAgent.SetDestination(_mapService.GetFinaliEnemyPoint());
    }
}