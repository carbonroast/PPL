using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum Block {
    purple,
    blue,
    red,
    green,
    cyan,

}

[System.Serializable]
public struct BlockData
{
    public Block block;
    public Tile tile;
}