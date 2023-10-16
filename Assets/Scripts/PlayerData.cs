using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct PlayerData
{
    public Tile tile;
    public Vector3Int[] cells {get; private set;}

    public void Initialize()
    {
        cells =  new Vector3Int[] {new Vector3Int(0,0,0), new Vector3Int(1,0,0)};
    }
}