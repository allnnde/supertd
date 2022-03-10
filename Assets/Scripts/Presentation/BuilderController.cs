using System;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    private BuildTypeEnum buildType;
    private Dictionary<BuildTypeEnum, string> _buildLayer;

    public void Configuration(Dictionary<BuildTypeEnum, string> buildLayer)
    {
        _buildLayer = buildLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_buildLayer.ContainsKey(buildType))
                return;
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var mapMask = LayerMask.GetMask(_buildLayer[buildType]);
            if (Physics.Raycast(inputRay, out hit, 1000, mapMask))
            {
                switch (buildType)
                {
                    case BuildTypeEnum.Tower_Ballesta:
                        ObjectPool.Instance.Spawn(ObjectPoolTags.OBJECT_POOL_TOWER_BALLESTA, hit.transform.position, hit.transform.rotation);
                        break;

                    case BuildTypeEnum.Mural:
                        ObjectPool.Instance.Spawn(ObjectPoolTags.OBJECT_POOL_STRUCTURE_MURAL, hit.transform.position, hit.transform.rotation);
                        break;

                    default:
                        break;
                }
                buildType = BuildTypeEnum.None;
            }
        }
    }

    public void ChangeBuildType(string buildTypeSelected)
    {
        Enum.TryParse(buildTypeSelected, out buildType);
    }
}