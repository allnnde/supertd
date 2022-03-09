using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapController : MonoBehaviour
{
    private GameObject[,] _map;
    public Dictionary<CellType, GameObject> _cellTypeObj;
    private MapData _mapData;

    public void Configuration(MapData map, Dictionary<CellType, GameObject> cellTypeObj)
    {
        _mapData = map;
        _cellTypeObj = cellTypeObj;
        //README: se inicializan aca ya que necesitamos el spawer creado para el gameinit
        GenerateMap();
        ReBakeNavMesh();
    }

    private void GenerateMap()
    {
        _map = new GameObject[_mapData.XSize, _mapData.YSize];
        for (int i = 0; i < _mapData.CellsData.GetLength(0); i++)
        {
            GameObject cell;
            for (int j = 0; j < _mapData.CellsData.GetLength(1); j++)
            {
                var cellData = _mapData.CellsData[i, j];
                var model = _cellTypeObj[cellData.CellType];

                cell = Instantiate(model, cellData.Position, model.transform.rotation);

                if (cellData.IsInitialPath)
                {
                    cell.AddComponent<SpawnerController>();
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
}