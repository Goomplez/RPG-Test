using System.Collections;
using UnityEngine;
using UnityEditor;
// TODO: Add a way to change highlight color

/**
 * TilePickerWindow
 * 
 * GUI window for selecting tiles for painting from a Texture2D type
 * 
 * Tiles should first be created from a sprite in multiple mode with a tile map (.png).
 * Slice the tiles evenly in the sprite editor and apply. The resulting sprite will
 * then have individual sprites contained within it that will be selectable through this window.
 * 
 * A brush tool (TileBrush) will be used to fill the TileMap grid.
 */
public class TilePickerWindow : EditorWindow {

    // To deal with small window sizes
    public enum Scale
    {
        x1,
        x2,
        x3,
        x4,
        x5
    }
    Scale scale;

    // Window scrolling when spritemap overflows
    public Vector2 scrollPosition = Vector2.zero;

    // Currently selected tile
    Vector2 currentSelection = Vector2.zero;
    
    // Open the window through the 'Window' menu
    // This is the 'start' function of this class
    [MenuItem("Window/Tile Picker")]
	public static void OpenTilePickerWindow()
    {
        var window = EditorWindow.GetWindow(typeof(TilePickerWindow));
        var title = new GUIContent();
        title.text = "Tile Picker";
        window.titleContent = title;
    }

    // Runs on each interaction with the GUI
    // @see https://docs.unity3d.com/ScriptReference/EditorWindow.OnGUI.html
    void OnGUI()
    {
        // Is an active GameObject selected (one on the scene)
        if(Selection.activeGameObject == null)
        {
        //    Debug.Log("GameObject is null", Selection.activeGameObject);
            return;
        }

        // Goal is to draw selected tile map into tile picker window
        // Try to get the TileMap component from the selected object
        var selection = ((GameObject)Selection.activeObject).GetComponent<TileMap>();
        if (selection != null)
        {
            // Reference the texture
            var texture2D = selection.texture2D;
            if(texture2D != null)
            {
                // Set up the scale selection box and convert to int
                scale = (Scale)EditorGUILayout.EnumPopup("Zoom", scale);
                var newScale = ((int)scale) + 1;
                var newTextureSize = new Vector2(texture2D.width, texture2D.height) * newScale;
                // Use an offset to avoid TileMap texture overlapping scale menu 
                var offset = new Vector2(10, 25);
                
                var viewPort = new Rect(0, 0, position.width-5, position.height-5); // Window dimensions minus scroll bars
                var contentSize = new Rect(0, 0, newTextureSize.x + offset.x, newTextureSize.y + offset.y); // 
                scrollPosition = GUI.BeginScrollView(viewPort, scrollPosition, contentSize);
                GUI.DrawTexture(new Rect(offset.x, offset.y, newTextureSize.x, newTextureSize.y), texture2D);

                // Calculate size of select box and where it will be rendered

                var tile = selection.tileSize * newScale;
                var grid = new Vector2(newTextureSize.x / tile.x, newTextureSize.y / tile.y);

                var selectionPos = new Vector2( tile.x * currentSelection.x + offset.x,
                                                tile.y * currentSelection.y + offset.y);


                // Selection box and styles
                var boxTexture = new Texture2D(1, 1);
                boxTexture.SetPixel(0, 0, new Color(0, 0.5f, 1f, 0.4f));
                boxTexture.Apply();

                var style = new GUIStyle(GUI.skin.customStyles[0]);
                style.normal.background = boxTexture;

                GUI.Box(new Rect(selectionPos.x, selectionPos.y, tile.x, tile.y), "", style);

                // Capture mouse click to change selection box
                var cEvent = Event.current;

                // Need mouse pos
                Vector2 mousePos = new Vector2(cEvent.mousePosition.x, cEvent.mousePosition.y);
                if(cEvent.type == EventType.mouseDown && cEvent.button == 0) // Check for mouse down and left-click
                {
                    currentSelection.x = Mathf.Floor((mousePos.x + scrollPosition.x) / tile.x);
                    currentSelection.y = Mathf.Floor((mousePos.y + scrollPosition.y) / tile.y);

                    Debug.Log(currentSelection.ToString());

                    if(currentSelection.x > grid.x - 1)
                    {
                        currentSelection.x = grid.x - 1;
                    }

                    if (currentSelection.y > grid.y - 1)
                    {
                        currentSelection.y = grid.y - 1;
                    }

                    selection.tileId = (int)(currentSelection.x + (currentSelection.y * grid.x) + 1);

                    Debug.Log("tile Id: " + selection.tileId + ", Grid Values:" + grid.ToString());

                    Repaint();
                }



                GUI.EndScrollView();

                //        Debug.Log("Start GUI.DrawTexture", selection.texture2D);
            }
        }
    }
}
