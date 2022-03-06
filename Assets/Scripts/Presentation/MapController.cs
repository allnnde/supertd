using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour
{
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private GameObject _cellModel;
    [SerializeField] private GameObject _cellStraightModel;
    private GameObject[,] _map;
    private Transform _initialEnemyCell;
    public Dictionary<CellType, GameObject> _cellTypeObj;

    private Transform _finaleEnemyCell;

    private void Awake()
    {
        _cellTypeObj = new Dictionary<CellType, GameObject>();
        _cellTypeObj.Add(CellType.Street, _cellStraightModel);
        _cellTypeObj.Add(CellType.Ground, _cellModel);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();

        ReBakeNavMesh();
    }

    private void GenerateMap()
    {
        var MapService = new MapService();
        var mapData = MapService.GenerateMapData(_xSize, _ySize);
        _map = new GameObject[_xSize, _ySize];

        for (int i = 0; i < mapData.GetLength(0); i++)
        {
            GameObject cell;
            for (int j = 0; j < mapData.GetLength(1); j++)
            {
                var cellData = mapData[i, j];

                cell = Instantiate(_cellTypeObj[cellData.CellType], cellData.Position, _cellStraightModel.transform.rotation);

                if (cellData.IsInitialPath)
                {
                    _initialEnemyCell = cell.transform;
                    cell.AddComponent<SpawnerController>();
                }
                if (cellData.IsFinalyPath)
                {
                    _finaleEnemyCell = cell.transform;
                }

                cell.name = cellData.Name;
                cell.transform.SetParent(transform);
                _map[i, j] = cell;
            }
        }
    }

    private void ReBakeNavMesh()
    {
        //TODO: Revisar bien
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                var mesh = _map[i, j].GetComponent<NavMeshSurface>();
                mesh?.BuildNavMesh();
            }
        }
    }

    public Vector3 GetFinaliEnemyPoint()
    {
        return _finaleEnemyCell.position;
    }

    public Vector3 GetInitialEnemyPoint()
    {
        return _initialEnemyCell.position;
    }
}