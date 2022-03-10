using UnityEngine;
using UnityEngine.UI;

public abstract class HealthController : MonoBehaviour, IHealthController
{
    public float LifeMax = 1000;
    protected float _lifeActual;

    [SerializeField]
    protected Slider _slider;

    protected IHealthServices _healthServices;

    public void Configuration(IHealthServices healthServices)
    {
        _healthServices = healthServices;
    }

    private void Awake()
    {
        _lifeActual = LifeMax;
        _slider.GetComponentInParent<Canvas>().worldCamera = Camera.main;
    }

    public abstract void Death();

    public void ShowLife(float lifeActual, float lifeMax)
    {
        _slider.value = lifeActual / lifeMax;
    }

    public float TakeDamage(float damage)
    {
        _lifeActual = _healthServices.TakeDamage(damage, _lifeActual, LifeMax);
        return _lifeActual;
    }
}