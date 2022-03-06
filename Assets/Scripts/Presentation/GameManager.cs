using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyModel;

    void Start()
    {
        ObjectPool.Instance.Pools.Add(new Pool() { Item = _enemyModel, Parent = transform, Size = 50, Tag = "enemy" });

        ObjectPool.Instance.Start();
    }
}