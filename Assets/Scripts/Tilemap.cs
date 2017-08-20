using System.Collections;
using UnityEngine;

public class TileMap : MonoBehaviour {

    /**
     * mapSize
     *
     * Stores the mapSize values provided to the TileMap component
     * 
     * Default map size is (20,10) (unity units)
     */
    public Vector2 mapSize = new Vector2(20, 10);
    public Texture2D texture2D;
    public Vector2 tileSize = new Vector2();
    public Vector2 gridSize = new Vector2();

    // Store all sprites attached to a Texture2D
    public Object[] spriteReferences;

    // 
    public int pixelsToUnits = 100;

    public int tileId = 0;


    public Sprite currentTileBrush
    {
        get { return spriteReferences[tileId] as Sprite; }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmosSelected()
    {
        var pos = transform.position;

        if(texture2D != null)
        {
            // Draw grid for tiling
            Gizmos.color = Color.gray;
            var row = 0;
            var maxColumns = mapSize.x;
            var total = mapSize.x * mapSize.y;
            var tile = new Vector3(tileSize.x / pixelsToUnits, tileSize.y / pixelsToUnits);
            var offset = new Vector2(tile.x / 2, tile.y / 2);

            for(var i = 0; i < total; i++)
            {
                var column = i % maxColumns;

                var newX = (column * tile.x) + offset.x + pos.x;
                var newY = -(row * tile.y) - offset.y + pos.y;

                Gizmos.DrawWireCube(new Vector2(newX, newY), tile);

                if(column == maxColumns - 1)
                {
                    row++;
                }
            }

            // Give gizmo white border
            Gizmos.color = Color.white;
            // Gizmos are drawn from center, so we need the center point
            var centerX = pos.x + (gridSize.x / 2);
            var centerY = pos.y - (gridSize.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridSize);
        }
    }
}
