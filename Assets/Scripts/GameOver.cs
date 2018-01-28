using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    public GameObject TEXT;
    public GameObject BUTTON;
    // Use this for initialization
    void Start () {
        Screen.lockCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Continue()
    {
        TEXT.active = false;
        BUTTON.active = false;
        Application.LoadLevel("MainMenu");
    }
}
