using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

[System.Serializable]
public class PlayerController : MonoBehaviour 
{
    public Board board {get; private set;}
    public PlayerData data {get; private set;}
    public Vector3Int[] cells {get; private set;}
    public Vector3Int position {get; private set;}
    public Tile tile {get; private set;}

    public void Initialize(Board board, Vector3Int position, PlayerData data) 
    {
        Debug.Log($"Player Initailized: {position}");
        this.board = board;
        this.position = position;
        this.data = data;
        this.tile = data.tile;

        if (this.cells == null) {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i=0; i < data.cells.Length; i++){
            this.cells[i] = data.cells[i];
        }
    }
    private void Awake() 
    {
        //this.board = 
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            Move(Vector3Int.left);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            Move(Vector3Int.right);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            Move(Vector3Int.up);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            Move(Vector3Int.down);
        }
    }

    void Move(Vector3Int direction)
    {
        bool valid = this.board.ValidMove(this, direction);
        if(valid){
            foreach(Vector3Int cell in this.cells){
                this.board.ClearTile(cell + position);
            }
            foreach(Vector3Int cell in this.cells){
                Vector3Int newPosition = this.position + direction + cell;
                this.board.SetTile(newPosition, this.tile);
            }
            this.position = position + direction; 
        }

    }
}