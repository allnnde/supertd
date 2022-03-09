using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthServices
{
    float TakeDamage(float damage, float lifeActual, float lifeMax);
}