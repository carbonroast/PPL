using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections.Generic;
public class Board : MonoBehaviour 
{
    public Tilemap boardmap {get; private set;}
    public Tilemap playermap {get; private set;}
    public BlockData[] blocks;
    public PlayerData playerData;
    public PlayerController playerController {get; private set;}
    public BlockController blockController {get; private set;}
    
    public ImportMap map {get; private set;}
    public Vector2Int boardSize;

    public RectInt bounds
    {
        get
        {
            return new RectInt(new Vector2Int(0,0), this.boardSize);
        }
    }
    private void Awake() {
        this.boardmap = Component.FindObjectsOfType<Tilemap>().ToList().Find(z => z.name == "Boardmap");
        this.playermap = Component.FindObjectsOfType<Tilemap>().ToList().Find(z => z.name == "Playermap");
        this.playerData.Initialize();
        this.blockController = GetComponent<BlockController>();
        this.playerController = GetComponent<PlayerController>();
        this.map = GetComponent<ImportMap>();
    }
    private void Start()
    {
        Debug.Log(bounds);
        this.blockController.Initialize(this, boardmap);
        SpawnPlayer();
        GenerateBoard(this.map.GetMap());
    }

    public void SetTile(Tilemap tileMap, Vector3Int cell, Tile tile)
    {
        tileMap.SetTile(cell, tile);
    }

    public void ClearTile(Tilemap tileMap, Vector3Int cell)
    {
        tileMap.SetTile(cell, null);
    }


    void SpawnPlayer()
    {
        Vector3Int position = new Vector3Int(0,6,0);
        this.playerController.Initialize(this, playermap, position, playerData);
        for (int i=0; i < playerController.cells.Length; i++){
            SetTile(playermap, position + playerController.cells[i], playerController.tile);
        }
    }

    public void SwapBlock(Vector3Int cellLeft, Vector3Int cellRight)
    {
        Tile tileLeft = (Tile)this.boardmap.GetTile(cellLeft);
        Tile tileRight = (Tile)this.boardmap.GetTile(cellRight);
        SetTile(boardmap, cellLeft, tileRight);
        SetTile(boardmap, cellRight, tileLeft);
        blockController.CheckBlockState(cellLeft);
        blockController.CheckBlockState(cellRight);
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

    void GenerateBoard(Dictionary<Color, List<Vector3Int>> mapPreset)
    {
        int color = 0;
        foreach(Color key in mapPreset.Keys){
            if(key != Color.white){
                BlockData blockData = this.blocks[color];
                foreach(Vector3Int cell in mapPreset[key]){
                    SetTile(boardmap, cell, blockData.tile);
                }
            }
            color++;
        }
    }
}