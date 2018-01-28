using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stage2_FadeAndLoad : MonoBehaviour {

    public GameObject BOSS;
    public GameObject ME;

    public GameObject OpenText;
    
    private bool destroyed;
    private float Textdis;

    // Use this for initialization
    void Start () {
        destroyed = false;
        Textdis = Time.time;
    }
    public void button_functions(string function)
    {
        if (function == "Resume") f_Resume();
        if (function == "MainMenu") f_MainMenu();
        if (function == "Exit") f_Exit();
    }
    // Update is called once per frame
    void Update () {
        if (OpenText.active  && Time.time > Textdis + 2.0f) OpenText.active = false;
        BossDead();
    }

    public void BossDead()
    {
        if (BOSS == null && destroyed == false)
        {
            GetComponent<Animation>().Play("Stage2FadeOut");
            destroyed = true;
        }
    }

    public void LoadMap() {
        SceneManager.LoadScene("Stage2Clear", LoadSceneMode.Single);
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
