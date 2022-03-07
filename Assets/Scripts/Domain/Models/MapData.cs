using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MapData
{
    public MapData(int xSize, int ySize)
    {
        XSize = xSize;
        YSize = ySize;
    }

    public int XSize { get; set; }
    public int YSize { get; set; }
    public CellData[,] CellsData { get; set; }
}