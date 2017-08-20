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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
