using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stage2_State : MonoBehaviour {

    public GameObject player;
    public GameObject BOSS_GATE;
    public GameObject BOSS_TRIGGER;

    public AudioClip StageBGM;
    public AudioClip BossBGM;
    private AudioSource audio;

    private int GameState;

	// Use this for initialization
	void Start () {
        GameState = 0;
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameState == 0) {
            audio.clip = StageBGM;
            audio.Play();
            audio.volume = 1.0f;
            GameState = 1;
        }
        if (GameState == 1) {
            if (player.transform.position.z > 900)
                GameState = 2;
        }
        
        if (GameState == 2) {
            audio.volume -= 0.25f * Time.deltaTime;
            if (BOSS_GATE.transform.position.y >= 20)
            {
                BOSS_GATE.transform.position = new Vector3(BOSS_GATE.transform.position.x, 20, BOSS_GATE.transform.position.z);
            }
            else BOSS_GATE.transform.position += new Vector3(0, 180 * Time.deltaTime, 0);
            if (audio.volume <= 0) {
                GameState = 3;
            }
        }
        if (GameState == 3) {
            audio.clip = BossBGM;
            audio.Play();
            audio.volume = 1.0f;
            Destroy(BOSS_TRIGGER);
            GameState = 4;
        }
        if (GameState == 4) {

        }
	}
}
