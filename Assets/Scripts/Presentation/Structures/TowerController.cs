using UnityEngine;

public class TowerController : MonoBehaviour, IPoolable
{
    private EnemyHealthController _target;
    private float timeShoot = 1F;
    private float timeShootCheck = 1F;
    public float Damage = 30;
    private ITowerService _towerService;

    public void Configuration(ITowerService towerService)
    {
        _towerService = towerService;
    }

    GameObject IPoolable.Owner => gameObject;

    // Start is called before the first frame update
    private void Update()
    {
        if (_towerService.IsTargetActive(_target))
        {
            _target = null;
        }

        timeShootCheck += Time.deltaTime;
        if (timeShootCheck > timeShoot)
        {
            if (_target)
            {
                _target.TakeDamage(Damage);
            }
            timeShootCheck = 0;
        }
        if (_target)
            transform.LookAt(_target.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_towerService.VerifyTargetInRange(other, _target))
        {
            _target = other.GetComponent<EnemyHealthController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_towerService.VerifyTargetOutRange(other, _target))
        {
            _target = null;
        }
    }

    void IPoolable.OnInstanciate(Transform parent)
    {
        Configuration(new TowerService());
        transform.parent = parent;
        gameObject.SetActive(false);
    }

    void IPoolable.OnSpawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    void IPoolable.OnDespawn()
    {
        gameObject.SetActive(false);
    }
}