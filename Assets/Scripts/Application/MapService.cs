using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapService
{
    public CellData[,] GenerateMapData(int xSize, int ySize)
    {
        var _mapData = new CellData[xSize, ySize];
        var middleCell = _mapData.GetLength(1) / 2;
        for (int i = 0; i < _mapData.GetLength(0); i++)
        {
            for (int j = 0; j < _mapData.GetLength(1); j++)
            {
                CellData cell = new CellData();

                if (middleCell != j)
                {
                    cell.CellType = CellType.Ground;
                }
                else
                {
                    cell.CellType = CellType.Street;
                    cell.IsInitialPath = i == 0;
                    cell.IsFinalyPath = i == _mapData.GetLength(0) - 1;
                }

                cell.Name = $"cell-{i}-{j}";
                cell.Position = new Vector3(i, 0, j);
                _mapData[i, j] = cell;
            }
        }
        return _mapData;
    }
}