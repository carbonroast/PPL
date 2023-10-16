using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Tilemaps;

public class BlockController : MonoBehaviour
{
    public Board board {get; private set;}
    public Tilemap boardmap {get; private set;}

    // Start is called before the first frame update

    public void Initialize(Board board, Tilemap map) 
    {
        this.board = board;
        this.boardmap = map;
    }
    void Start()
    {
        //gridPosition = tilemap.WorldToCell(transform.position);
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckBlockState(Vector3Int cell)
    {
        List<Vector3Int> dropCells = new List<Vector3Int>();

        if(boardmap.GetTile(cell) == null){ //Cell empty, make above cells fall
            List<Vector3Int> aboveCells = GetTilesinColumn(cell);
            for(int i=0; i< aboveCells.Count;  i++){
                dropCells.Add(Fall(aboveCells[i]));
            }
        }
        else { //Check if cell needs to fall and do logic
            dropCells.Add(Fall(cell));
        }

        // Check to see if any matches were made
        foreach(Vector3Int dropCell in dropCells){
            List<Vector3Int> matchList = FindMatchingSurroundingTiles(dropCell);
            if(matchList.Any()){
                LineCompletion(matchList);
                foreach(Vector3Int completedDropCell in matchList){
                    CheckBlockState(completedDropCell);
                }

            }
        }
    }


    public List<Vector3Int> FindMatchingSurroundingTiles(Vector3Int cell)
    {
        Vector3Int[] directionsEW = new Vector3Int[]
        {
            Vector3Int.right,
            Vector3Int.left,
        };

        Vector3Int[] directionsNS = new Vector3Int[]
        {
            Vector3Int.up,
            Vector3Int.down,
        };

        List<Vector3Int> matchList = new List<Vector3Int>();
        matchList.AddRange(FindTilesOnAxis(cell, directionsEW));
        matchList.AddRange(FindTilesOnAxis(cell, directionsNS));
        matchList = matchList.Distinct().ToList();

        return matchList;
    }

    public List<Vector3Int> FindTilesOnAxis(Vector3Int cell, Vector3Int[] directions)
    {
        List<Vector3Int> tileList = new List<Vector3Int>{cell};
        Sprite sprite = boardmap.GetSprite(cell);
        foreach(Vector3Int direction in directions){
            Vector3Int neighborTilePosition = cell + direction;
            Sprite neighborsprite = boardmap.GetSprite(neighborTilePosition);
            if(sprite == neighborsprite){
                tileList.AddRange(TraverseTiles(cell, direction, sprite, tileList));
            }
        }
        tileList = tileList.Distinct().ToList();
        if(tileList.Count >= 3){
            return tileList;
        }
        return new List<Vector3Int>();
    }

    //Recurse through tiles and add all tiles with the same sprite
    public List<Vector3Int> TraverseTiles(Vector3Int cell, Vector3Int direction, Sprite sprite, List<Vector3Int> tileList)
    {
        tileList.Add(cell);
        Vector3Int neighborTilePosition = cell + direction;
        Sprite neighborsprite = boardmap.GetSprite(neighborTilePosition);
        if (!boardmap.HasTile(neighborTilePosition) || sprite != neighborsprite){
            return tileList;
        }
        else {
            return TraverseTiles(neighborTilePosition, direction, sprite, tileList);
        }
    }

    public Vector3Int Fall(Vector3Int cell)
    {
        Vector3Int tileBelow = cell + Vector3Int.down;
        if(!this.board.bounds.Contains((Vector2Int)tileBelow) || this.boardmap.HasTile(tileBelow)){
            return cell;
        }   
        else{
                Tile tile = (Tile)boardmap.GetTile(cell);
                this.board.ClearTile(boardmap, cell);
                this.board.SetTile(boardmap, tileBelow, tile);
                return Fall(tileBelow);
        }

    }

    public List<Vector3Int> GetTilesinColumn(Vector3Int cell)
    {
        List<Vector3Int> cellsinColumn = new List<Vector3Int>();

        Vector3Int cellAbove = cell + Vector3Int.up;
        while(boardmap.HasTile(cellAbove)){
            cellsinColumn.Add(cellAbove);
            cellAbove = cellAbove + Vector3Int.up;
        }
        return cellsinColumn;
    }


    public void LineCompletion(List<Vector3Int> cells)
    {
        foreach(Vector3Int cell in cells){
            this.boardmap.SetTile(cell, null);
        }
    }
}
