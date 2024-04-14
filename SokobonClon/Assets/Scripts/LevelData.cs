using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileData
{
    public int x;
    public int y;
    public bool wall;
    public string content;
}

[System.Serializable]
public class LevelData
{
    public int level;
    public List<TileData> tiles;
}
