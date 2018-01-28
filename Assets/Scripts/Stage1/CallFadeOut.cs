using UnityEngine;
using System.Collections;

public class CallFadeOut : MonoBehaviour {

    public GameObject Fade;
    public float timetocall = 11.0f; 

	// Use this for initialization
	void Start () {
        Invoke("FadeOut", timetocall);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FadeOut()
    {
        Fade.SendMessage("PlayFadeOut");
    }
}
