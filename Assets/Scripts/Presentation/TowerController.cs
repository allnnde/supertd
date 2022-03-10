using UnityEngine;

public class TowerController : MonoBehaviour, IPoolable
{
    private EnemyHealthController _target;
    private float timeShoot = 1F;
    private float timeShootCheck = 1F;
    public float Damage = 30;

    GameObject IPoolable.Owner => gameObject;

    // Start is called before the first frame update
    private void Update()
    {
        if (_target && !_target.gameObject.activeSelf)
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.TAG_ENEMY) && !_target)
        {
            _target = other.gameObject.GetComponent<EnemyHealthController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.TAG_ENEMY) && other.name.Equals(_target?.name))
        {
            _target = null;
        }
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
    }

    void IPoolable.OnDespawn()
    {
        gameObject.SetActive(false);
    }
}