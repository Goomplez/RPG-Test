using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public int columns = 10;
    public int rows = 10;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;

    private Transform boardHolder;

    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        for(int x = 1; x < columns-1; x++)
        {
            for(int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for(int x = -1; x < columns + 1; x++)
        {
            for(int y= -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[0];

                if(x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wallTiles[0];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

	// Use this for initialization
	void Start () {
        BoardSetup();

        InitialiseList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
