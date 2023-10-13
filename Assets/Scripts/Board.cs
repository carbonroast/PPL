using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour 
{
    public Tilemap tilemap {get; private set;}
    public BlockData[] blocks;
    public PlayerData playerData;
    public PlayerController playerController {get; private set;}
    
    public Vector2Int boardSize;

    public RectInt bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }
    private void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.playerData.Initialize();
        this.playerController = GetComponent<PlayerController>();

    }
    private void Start()
    {
        SpawnPlayer();
        //SpawnBlock();
    }

    public void SetTile(Vector3Int cell, Tile tile)
    {
        this.tilemap.SetTile(cell, tile);
    }

    public void ClearTile(Vector3Int cell)
    {
        this.tilemap.SetTile(cell, null);
    }
    void SpawnBlock() 
    {
        int rand = Random.Range(0, this.blocks.Length);
        BlockData blockData = this.blocks[rand];
        this.tilemap.SetTile(Vector3Int.zero,blockData.tile);
        this.tilemap.SetTile(Vector3Int.right,blockData.tile);
    }

    void SpawnPlayer()
    {
        Vector3Int position = new Vector3Int(0,6,0);
        this.playerController.Initialize(this, position, playerData);
        for (int i=0; i < playerController.cells.Length; i++){
            this.tilemap.SetTile(position + playerController.cells[i], playerController.tile);
        }
    }

    public bool ValidMove(PlayerController playercontroller, Vector3Int direction)
    {
        foreach(Vector3Int cell in playerController.cells){
            
            if(!this.bounds.Contains((Vector2Int)cell + (Vector2Int)playerController.position + (Vector2Int)direction))
            {
                return false;
            }
        }
        return true;
    }
}