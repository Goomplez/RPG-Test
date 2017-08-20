using System.Collections;
using UnityEngine;
using UnityEditor;

/**
 * NewTileMapMenu
 * 
 * The menu command to add a new TileMap game object to the scene
 */
public class NewTileMapMenu {

    /**
     *  CreateTileMap
     * 
     *  Adds a new TileMap GameObject to the scene
     */

    [MenuItem("GameObject/Tile Map")] // This just assigns the menu item to the GameObject menu and can be called anything you want
    public static void CreateTileMap()
    {
        // Get our GameObject from the TileMap.cs script
        GameObject go = new GameObject("Tile Map");
        // Add the script as a component of the game object
        go.AddComponent<TileMap>();
    }
}
