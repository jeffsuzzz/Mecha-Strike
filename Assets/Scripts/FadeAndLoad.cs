using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeAndLoad : MonoBehaviour {

    public GameObject BOSS;
    public GameObject ME;
    public GameObject OpenText;
    private bool destroyed;
    private float Textdis;

    // Use this for initialization
    void Start ()
    {
        destroyed = false;
        Textdis = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (OpenText.active && Time.time > Textdis + 2.0f) OpenText.active = false;
        if (BOSS == null && destroyed == false) BossDead();

    }

    public void button_functions(string function)
    {
        if (function == "Resume") f_Resume();
        if (function == "MainMenu") f_MainMenu();
        if (function == "Exit") f_Exit();
    }

    public void BossDead()
    {
        GetComponent<Animation>().Play("Stage1FadeOut");
    }

    public void LoadMap() {
        SceneManager.LoadScene("Stage1Clear", LoadSceneMode.Single);
    }

    public void f_Resume()
    {
        Screen.lockCursor = true;
        Time.timeScale = 1;
        ME.active = false;
    }

    public void f_MainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }

    public void f_Exit()
    {
        Application.Quit();
    }
}
