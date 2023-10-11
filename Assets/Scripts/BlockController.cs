using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Tilemaps;

public class BlockController : MonoBehaviour
{
    Board board;
    Vector3Int gridPosition;
    // Start is called before the first frame update
    void Start()
    {
        //gridPosition = tilemap.WorldToCell(transform.position);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindSurroundingTiles()
    {
        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up,
            Vector3Int.down
        };
        
        foreach(Vector3Int direction in directions)
        {
            Vector3Int neighborTilePosition = gridPosition + direction;

            // if (tilemap.HasTile(neighborTilePosition))
            // {
            //     Debug.Log($"Found tile at {neighborTilePosition}");
            // }
            // else
            // {
            //     Debug.Log($"No Tile at {neighborTilePosition}");
            // }
        }
    }

    void Fall()
    {
        Vector3Int tileBelow = gridPosition + Vector3Int.down;
        // if(tilemap.HasTile(tileBelow))
        // {
        //     Debug.Log("Reach bottom");
        //     return;
        // }
        // else
        // {
        //     gridPosition = tileBelow;
        //     Fall();
        // }
    }
}
