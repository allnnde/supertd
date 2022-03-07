using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    public float LifeMax = 1000;
    private float _lifeActual;

    [SerializeField]
    private Slider _slider;

    private void Awake()
    {
        _lifeActual = LifeMax;
        _slider.GetComponentInParent<Canvas>().worldCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyController>();
            _lifeActual -= enemy.Damage;
            _slider.value = _lifeActual / LifeMax;
            ObjectPool.Instance.Despawn("enemy", other.gameObject);
        }
    }
}