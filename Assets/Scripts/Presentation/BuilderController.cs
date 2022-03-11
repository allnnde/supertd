using System;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    private BuildTypeEnum buildType;
    private Dictionary<BuildTypeEnum, string> _buildLayer;
    private Dictionary<BuildTypeEnum, string> _buildObjTag;

    public void Configuration(Dictionary<BuildTypeEnum, string> buildLayer, Dictionary<BuildTypeEnum, string> buildObjTag)
    {
        _buildLayer = buildLayer;
        _buildObjTag = buildObjTag;
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
                ObjectPool.Instance.Spawn(_buildObjTag[buildType], hit.transform.position, hit.transform.rotation);
                buildType = BuildTypeEnum.None;
            }
        }
    }

    public void ChangeBuildType(string buildTypeSelected)
    {
        Enum.TryParse(buildTypeSelected, out buildType);
    }
}