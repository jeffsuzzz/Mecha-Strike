using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour {

    public GameObject GameStart;
    public GameObject StageSelect;
    public GameObject Exit;
    public GameObject HTP;
    public GameObject HTP_Text;
    public GameObject Credits;
    public GameObject Credits_Text;
    public GameObject Back;
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;

    public AudioClip MenuBGM;
    private AudioSource audio;

    private int load;
    private float currentTime;
    
	// Use this for initialization
	void Start ()
    {
        audio = GetComponent<AudioSource>();
        GameStart.active = true;
        StageSelect.active = true;
        Exit.active = true;
        HTP.active = true;
        HTP_Text.active = false;
        Credits.active = true;
        Credits_Text.active = false;
        Back.active = false;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
        load = -1;
}
    public void functions(string function)
    {
        if (function == "GameStart") f_GameStart();
        if (function == "StageSelect") f_StageSelect();
        if (function == "Exit") f_Exit();
        if (function == "HTP") f_HTP();
        if (function == "Credits") f_Credits();
        if (function == "Back") f_Back();
        if (function == "STAGE1") f_Stage1();
        if (function == "STAGE2") f_Stage2();
        if (function == "STAGE3") f_Stage3();
    }
    public void f_GameStart()
    {
        GameStart.active = false;
        StageSelect.active = false;
        Exit.active = false;
        HTP.active = false;
        Credits.active = false;
        GetComponent<Animation>().Play("FadeOut");
        currentTime = Time.time;
        load = 0;
    }
    public void f_StageSelect() {
        GameStart.active = false;
        StageSelect.active = false;
        Exit.active = false;
        HTP.active = false;
        Credits.active = false;
        Back.active = true;
        Stage1.active = true;
        Stage2.active = true;
        Stage3.active = true;
    }
    public void f_Exit() {
        Application.Quit();
    }
    public void f_HTP(){
        GameStart.active = false;
        StageSelect.active = false;
        Exit.active = false;
        HTP.active = false;
        HTP_Text.active = true;
        Credits.active = false;
        Back.active = true;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
    }

    public void f_Credits() {
        GameStart.active = false;
        StageSelect.active = false;
        Exit.active = false;
        HTP.active = false;
        Credits.active = false;

        Credits_Text.active = true;
        Back.active = true;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
    }
    public void f_Back() {
        GameStart.active = true;
        StageSelect.active = true;
        Exit.active = true;
        HTP.active = true;
        HTP_Text.active = false;
        Credits.active = true;
        Credits_Text.active = false;
        Back.active = false;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
    }
    public void f_Stage1()
    {
        Back.active = false;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
        GetComponent<Animation>().Play("FadeOut");
        currentTime = Time.time;
        load = 1;

    }
    public void f_Stage2()
    {
        Back.active = false;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
        GetComponent<Animation>().Play("FadeOut");
        currentTime = Time.time;
        load = 2;
    }
    public void f_Stage3()
    {
        Back.active = false;
        Stage1.active = false;
        Stage2.active = false;
        Stage3.active = false;
        GetComponent<Animation>().Play("FadeOut");
        currentTime = Time.time;
        load = 3;
    }
    // Update is called once per frame
    void Update () {
        if (load != -1) {
            audio.volume -= 1f * Time.deltaTime;
        }
        if (load == 0 && Time.time > currentTime + 1.0f)
        {
            Application.LoadLevel("GameStart");
            load = -1;
        }
        if (load == 1 && Time.time > currentTime + 1.0f)
        {
            Application.LoadLevel("stage1");
            load = -1;
        }
        if (load == 2 && Time.time > currentTime + 1.0f)
        {
            Application.LoadLevel("stage2");
            load = -1;
        }
        if (load == 3 && Time.time > currentTime + 1.0f)
        {
            Application.LoadLevel("stage3");
            load = -1;
        }
	}
}
