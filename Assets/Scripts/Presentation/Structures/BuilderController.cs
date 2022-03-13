using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuilderController : MonoBehaviour
{
    private BuildTypeEnum buildType;
    private Dictionary<BuildTypeEnum, string> _buildLayer;
    private Dictionary<BuildTypeEnum, string> _buildObjTag;
    private Dictionary<BuildTypeEnum, int> _buildTypeCost;
    private IEcomonyService _ecomonyService;

    public void Configuration(
        Dictionary<BuildTypeEnum, string> buildLayer,
        Dictionary<BuildTypeEnum, string> buildObjTag,
        Dictionary<BuildTypeEnum, int> buildTypeCost,
        IEcomonyService ecomonyService)
    {
        _buildLayer = buildLayer;
        _buildObjTag = buildObjTag;
        _buildTypeCost = buildTypeCost;
        _ecomonyService = ecomonyService;
    }

    // Update is called once per frame
    void Update()
    {
        bool isOverUI = EventSystem.current.IsPointerOverGameObject();
        if (Input.GetMouseButtonDown(0) && !isOverUI)
        {
            if (!_buildLayer.ContainsKey(buildType))
                return;
            var cost = _buildTypeCost[buildType];
            var balance = _ecomonyService.GetBalance();
            if (balance < cost)
                return;

            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var mapMask = LayerMask.GetMask(_buildLayer[buildType]);

            if (Physics.Raycast(inputRay, out hit, 1000, mapMask))
            {
                ObjectPool.Instance.Spawn(_buildObjTag[buildType], hit.transform.position, hit.transform.rotation);
                _ecomonyService.UpdateCoins(-cost);
                buildType = BuildTypeEnum.None;
            }
        }
    }

    public void ChangeBuildType(string buildTypeSelected)
    {
        Enum.TryParse(buildTypeSelected, out buildType);
    }
}