using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthServices : IHealthServices
{
    private readonly IHealthController _healthController;

    public HealthServices(IHealthController healthController)
    {
        _healthController = healthController;
    }

    public float TakeDamage(float damage, float lifeActual, float lifeMax)
    {
        lifeActual -= damage;
        _healthController.ShowLife(lifeActual, lifeMax);

        if (lifeActual <= 0)
            _healthController.Death();

        return lifeActual;
    }
}