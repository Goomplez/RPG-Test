using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
