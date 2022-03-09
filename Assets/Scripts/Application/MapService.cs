using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapService : IMapService
{
    private MapData _map;

    public MapData GenerateMapData(int xSize, int ySize)
    {
        _map = new MapData(xSize, ySize);

        var _mapData = new CellData[xSize, ySize];
        for (int i = 0; i < _mapData.GetLength(0); i++)
        {
            for (int j = 0; j < _mapData.GetLength(1); j++)
            {
                CellData cell = new CellData();
                cell.CellType = CellType.Ground;
                cell.Name = $"cell-{i}-{j}";
                cell.Position = new Vector3(i, 0, j);
                _mapData[i, j] = cell;
            }
        }

        var initialCell = Random.Range(0, _mapData.GetLength(1));
        _mapData[0, initialCell].CellType = CellType.Street;
        _mapData[0, initialCell].IsInitialPath = true;
        var position = new Vector3Int(0, 0, initialCell);
        while (true)
        {
            try
            {
                var action = (PathBuildDesicionEnum)Random.Range(-1, 2);
                var previousPosition = new Vector3Int(position.x, position.y, position.z);
                switch (action)
                {
                    case PathBuildDesicionEnum.Down:
                        position.z--;
                        break;

                    case PathBuildDesicionEnum.forward:
                        position.x++;
                        break;

                    case PathBuildDesicionEnum.Up:
                        position.z++;
                        break;

                    default:
                        break;
                }
                if (position.x >= _mapData.GetLength(0) - 1)
                {
                    _mapData[position.x, position.z].IsFinalyPath = true;
                    _mapData[position.x, position.z].CellType = CellType.Castle;
                    break;
                }

                if (position.z >= _mapData.GetLength(1) - 1)
                {
                    position.z = _mapData.GetLength(1) - 1;
                }

                if (position.z < 0)
                {
                    position.z = 0;
                }
                if (_mapData[position.x, position.z].CellType == CellType.Street)
                {
                    position = previousPosition;
                    continue;
                }

                _mapData[position.x, position.z].CellType = CellType.Street;
            }
            catch (System.Exception ex)
            {
                Debug.Log($"Problemas con la celda {position.x}({_mapData.GetLength(1)}) - {position.z}({_mapData.GetLength(0)})");
                Debug.LogError(ex);
            }
        }
        _map.CellsData = _mapData;
        return _map;
    }

    public Vector3 GetFinaliEnemyPoint()
    {
        return _map.CellsData.Cast<CellData>().FirstOrDefault(p => p.IsFinalyPath).Position;
    }

    public Vector3 GetInitialEnemyPoint()
    {
        return _map.CellsData.Cast<CellData>().FirstOrDefault(p => p.IsInitialPath).Position;
    }
}