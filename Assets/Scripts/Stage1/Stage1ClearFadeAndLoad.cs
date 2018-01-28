using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stage1ClearFadeAndLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayFadeOut()
    {
        GetComponent<Animation>().Play("Stage1ClearFadeOut");
    }

    public void LoadMap1() {
        SceneManager.LoadScene("Stage1Clear", LoadSceneMode.Single);
    }

    public void LoadStage2()
    {
        SceneManager.LoadScene("Stage2", LoadSceneMode.Single);
    }

    public void LoadMap2()
    {
        SceneManager.LoadScene("Stage2Clear", LoadSceneMode.Single);
    }

    public void LoadStage3()
    {
        SceneManager.LoadScene("Stage3", LoadSceneMode.Single);
    }
}
