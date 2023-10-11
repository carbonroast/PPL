using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour 
{
    public Tilemap tilemap {get; private set;}
    public BlockData[] blocks;

    private void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();


    }
    private void Start()
    {
        SpawnBlock();
    }

    void SpawnBlock() 
    {
        int rand = Random.Range(0, this.blocks.Length);
        BlockData block = this.blocks[rand];
        this.tilemap.SetTile(Vector3Int.zero,block.tile);
        this.tilemap.SetTile(Vector3Int.right,block.tile);
    }

    public bool ValidMove(Vector3Int position)
    {
        return false;
    }
}