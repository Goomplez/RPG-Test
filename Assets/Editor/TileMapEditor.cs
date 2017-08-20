using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor {

    // Reference to the TileMap instance we have selected
    public TileMap map;
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        // Used to update grid on change
        var oldSize = map.mapSize;
    
        // Reads value from map and returns V2 field with values
        // refreshes on value change
        map.mapSize = EditorGUILayout.Vector2Field("Map Size:", map.mapSize);

        if(map.mapSize != oldSize)
        {
            UpdateCalculations();
        }

        // Enable texture2D field (allowing sprite textures)
        map.texture2D = (Texture2D)EditorGUILayout.ObjectField("Texture 2d:", map.texture2D, typeof(Texture2D), false);

        // Bad texture? Handled.
        if (map.texture2D == null)
        {
            EditorGUILayout.HelpBox("You have not selected a texture 2D yet.", MessageType.Warning);
        } else
        {
            EditorGUILayout.LabelField("Tile Size:", map.tileSize.x + "x" + map.tileSize.y);
            EditorGUILayout.LabelField("Grid Size in Units:", map.gridSize.x + "x" + map.gridSize.y);
            EditorGUILayout.LabelField("Pixels to Units:", map.pixelsToUnits.ToString());

        }

        EditorGUILayout.EndVertical();
    }

    void OnEnable()
    {
        // Link component values to TileMap
        map = target as TileMap;

        // Auto select view tool
        Tools.current = Tool.View;

        // Read all sprites attached to a Texture2D when we select a TileMap
        // Test for existence of texture2D in map
        if(map.texture2D != null)
        {
            UpdateCalculations();   
        }
    }

    void UpdateCalculations()
    {
        // Get the asset path of the reference Texture2D
        var path = AssetDatabase.GetAssetPath(map.texture2D);
        // Texture should be split by grid, load all sprites in the texture into memory
        map.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

        // In order to determine tile size we need to reference the first sprite in the references
        // NOTE: the first index in the sprite references is the texture itself, so we select the second index which is the first sliced sprite
        var sprite = (Sprite)map.spriteReferences[1];
        // Grab width and height
        var width = sprite.textureRect.width;
        var height = sprite.textureRect.height;

        // Tell the map the tile size
        map.tileSize = new Vector2(width, height);

        // Get pixels to units
        map.pixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);

        // Determine grid total size
        // Created sprite should have 
        map.gridSize = new Vector2((width / map.pixelsToUnits) * map.mapSize.x, (height / map.pixelsToUnits) * map.mapSize.y);
    }
}
