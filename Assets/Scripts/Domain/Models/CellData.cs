using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CellData
{
    public CellType CellType { get; set; }
    public bool IsInitialPath { get; set; }
    public bool IsFinalyPath { get; set; }
    public Vector3 Position { get; set; }
    public string Name { get; set; }
}