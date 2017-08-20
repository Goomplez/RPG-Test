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
        // Reads value from map and returns V2 field with values
        // refreshes on value change
        map.mapSize = EditorGUILayout.Vector2Field("Map Size:", map.mapSize);
        // Enable texture2D field (allowing sprite textures)
        map.texture2D = (Texture2D)EditorGUILayout.ObjectField("Texture 2d:", map.texture2D, typeof(Texture2D), false);

        // Bad texture? Handled.
        if (map.texture2D == null)
        {
            EditorGUILayout.HelpBox("You have not selected a texture 2D yet.", MessageType.Warning);
        } else
        {
            EditorGUILayout.LabelField("Tile Size:", map.tileSize.x + "x" + map.tileSize.y);
        }

        EditorGUILayout.EndVertical();
    }

    void OnEnable()
    {
        // Link component values to TileMap
        map = target as TileMap;
        // Auto select view tool
        Tools.current = Tool.View;
    }
}
