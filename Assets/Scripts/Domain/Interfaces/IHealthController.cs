using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public interface IHealthController
{
    void Death();

    void ShowLife(float lifeActual, float lifeMax);
}