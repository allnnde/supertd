using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TowerService : ITowerService
{
    public bool IsTargetActive(EnemyHealthController target)
    {
        return target && !target.gameObject.activeSelf;
    }

    public bool VerifyTargetInRange(Collider other, EnemyHealthController target)
    {
        return other.CompareTag(Tags.TAG_ENEMY) && !target;
    }

    public bool VerifyTargetOutRange(Collider other, EnemyHealthController target)
    {
        return other.CompareTag(Tags.TAG_ENEMY) && other.name.Equals(target?.name);
    }
}