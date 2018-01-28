using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stage3_State : MonoBehaviour {

    public GameObject player;
    public GameObject BOSS;
    public GameObject BOSS_Canvas;
    public GameObject BOSS_boundary;
    public GameObject BIG_explode;

    public AudioClip StageBGM;
    public AudioClip BossBGM;
    private AudioSource audio;

    private int GameState;
    private Vector3 BOSS_POS;

	// Use this for initialization
	void Start () {

        GameState = 0;
        BOSS_Canvas.active = false;
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(BOSS != null)BOSS_POS = BOSS.transform.position;
        if (GameState == 0) {
            audio.clip = StageBGM;
            audio.Play();
            audio.volume = 1.0f;
            GameState = 1;
        }
        if (GameState == 1) {
            if (player.transform.position.z > 3000)
            {
                BOSS_boundary.transform.position = new Vector3(-110, 0, 3000);
                GameState = 2;
            }
        }
        
        if (GameState == 2) {
            audio.volume -= 0.5f * Time.deltaTime;
            if (audio.volume <= 0) {
                GameState = 3;
            }
        }
        if (GameState == 3) {
            BOSS_Canvas.active = true;
            audio.clip = BossBGM;
            audio.Play();
            audio.volume = 1.0f;
            GameState = 4;
        }
        if (GameState == 4) {
        }
	}
}
