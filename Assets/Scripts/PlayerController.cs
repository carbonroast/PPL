using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

[System.Serializable]
public class PlayerController : MonoBehaviour 
{
    public Board board {get; private set;}
    public Tile tile;
    private Vector3Int position;

    public void Initialize(Board board) 
    {
        this.board = board;
        this.position = new Vector3Int(0,6,0);
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
        if(Input.GetKey(KeyCode.D)){
            Move(Vector3Int.right);
        }
        if(Input.GetKey(KeyCode.W)){
            Move(Vector3Int.up);
        }
        if(Input.GetKey(KeyCode.S)){
            Move(Vector3Int.down);
        }
    }

    void Move(Vector3Int direction)
    {
        Vector3Int newPosition = this.position + direction;
    }
}