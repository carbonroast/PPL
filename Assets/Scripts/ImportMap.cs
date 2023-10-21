using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ImportMap : MonoBehaviour
{
    public Sprite sprite;
    Dictionary<Color, List<Vector3Int>> blockPreset = new Dictionary<Color, List<Vector3Int>>();
    Texture2D texture;
    // Start is called before the first frame update
    void Awake()
    {
        texture = sprite.texture as Texture2D;
        ReadSprite(texture);

    }
    void Start()
    {
        
    }

    void ReadSprite(Texture2D texture)
    {
        int spriteWidth = sprite.texture.width;
        int spriteHeight = sprite.texture.height;
        for(int y=0; y< spriteHeight; y++){
            for(int x=0; x < spriteWidth; x++){
                Color pixelColor = texture.GetPixel(x,y);
                if(blockPreset.ContainsKey(pixelColor)){
                    blockPreset[pixelColor].Add((Vector3Int)new Vector2Int(x,y));
                }
                else{
                    blockPreset.Add(pixelColor,new List<Vector3Int>{(Vector3Int)new Vector2Int(x,y)});
                }
            }
        }
        // foreach(Color key in blockPreset.Keys){
        //     Debug.Log($"KEY: {key}");
        //     foreach(Vector3Int cell in blockPreset[key]){
        //         Debug.Log(cell);
        //     }
        // }
    }

    public Dictionary<Color, List<Vector3Int>> GetMap(){
        return blockPreset;
    }
}
