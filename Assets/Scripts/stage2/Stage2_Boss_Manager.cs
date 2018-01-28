using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stage2_Boss_Manager : MonoBehaviour {

    public GameObject Canvas;
    public Slider BOSS_HP;

    public GameObject BOSS_TRIGGER;
    public GameObject BOSS1;
    public GameObject BOSS2;
    public GameObject BOSS3;
    public GameObject target;


    private int state;

    public GameObject explode;
    public GameObject big_explode;
    public Vector3[] ExplodePos = new Vector3[10];
    private bool exploded;
    private float next_explotion;
    private float explotion_Rate;
    private int explode_time;

    private float max_health;
    private float cur_health;

    private float firerate_S1;
    private float nextfire_S1;

    private float firerate_S2;
    private float nextfire_S2;

    private float StateTime;

    private Enemy_Stage2_BOSS1 BOSS1_S;
    private Enemy_Stage2_BOSS2 BOSS2_S;
    private Enemy_Stage2_BOSS3 BOSS3_S;

    // Use this for initialization
    void Start () {
        state = 0;
        explotion_Rate = 0.2f;
        BOSS1_S = gameObject.GetComponentInChildren<Enemy_Stage2_BOSS1>();
        BOSS2_S = gameObject.GetComponentInChildren<Enemy_Stage2_BOSS2>();
        BOSS3_S = gameObject.GetComponentInChildren<Enemy_Stage2_BOSS3>();
        max_health = BOSS1_S.max_health + BOSS2_S.max_health + BOSS3_S.max_health;

        cur_health = 0;

        firerate_S1 = 0.2f;
        nextfire_S1 = 0;

        firerate_S2 = 0.1f;
        nextfire_S2 = 0;

        StateTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        cur_health = 0;
        if (BOSS1 != null) cur_health += BOSS1_S.cur_health;
        if (BOSS2 != null) cur_health += BOSS2_S.cur_health;
        if (BOSS3 != null) cur_health += BOSS3_S.cur_health;

        if (cur_health == 0) exploded = true;
        BOSS_HP.value = cur_health / max_health;
        switch (state) {
            case 0:
                Canvas.active = false;
                if (BOSS_TRIGGER == null) {
                    Canvas.active = true;
                    state++;
                } 
                break;
            case 1:
                BOSS_INTRO();
                break;
            case 2:
                FREE_SPIN();
                //TIME = 12.5S
                break;

            case 3:
                GET_TO_POS(40, 40, 40);
                //TIME = 0.5S
                break;

            case 4:
                UP_DOWN();
                //TIME = 13S
                break;

            case 5:
                GET_TO_POS(120, 40, 40);
                //TIME = 0.5S
                break;

            case 6:
                LOCAL_SPIN();
                //TIME = 13S
                break;

            case 7:
                CHASE();
                //TIME = 0.5S
                break;

            case 8:
                STRAIGHT();
                //TIME = 6S
                break;

            case 9:
                if (BOSS1 != null) BOSS1_S.chase();
                if (BOSS2 != null) BOSS2_S.chase();
                if (BOSS3 != null) BOSS3_S.chase();
                if (Time.time > StateTime)
                {
                    StateTime = Time.time + 12f;
                    state = 2;
                }

                break;
            default:
                break;

        }
        explosion();
    }

    void explosion() {
        if (exploded == true)
        {
            if (Time.time > next_explotion)
            {
                if (explode_time < 18)
                {
                    next_explotion = Time.time + explotion_Rate;
                    GameObject temp_explode;
                    temp_explode = Instantiate(explode, ExplodePos[explode_time%9], transform.rotation) as GameObject;
                    temp_explode.transform.localScale = new Vector3(70, 70, 70);
                }

                else if (explode_time == 18)
                {
                    GameObject temp_explode;
                    temp_explode = Instantiate(big_explode, ExplodePos[9], transform.rotation) as GameObject;
                    temp_explode.transform.localScale = new Vector3(100, 100, 100);
                    Destroy(gameObject, 1.0F);
                }
                
                explode_time++;
            }
        }
    }

    void BOSS_INTRO() {
        if (BOSS1.transform.position.y < 75)
        {
            BOSS1.transform.position += new Vector3(0, 25 * Time.deltaTime, 0);
            BOSS2.transform.position += new Vector3(0, 25 * Time.deltaTime, 0);
            BOSS3.transform.position += new Vector3(0, 25 * Time.deltaTime, 0);
        }
        if (BOSS1.transform.position.y >= 75
            || BOSS2.transform.position.y >= 75
            || BOSS3.transform.position.y >= 75)
        {
            BOSS1.transform.position = new Vector3(BOSS1.transform.position.x, 75, BOSS1.transform.position.z);
            BOSS2.transform.position = new Vector3(BOSS2.transform.position.x, 75, BOSS2.transform.position.z);
            BOSS3.transform.position = new Vector3(BOSS3.transform.position.x, 75, BOSS3.transform.position.z);

            StateTime = Time.time + 12f;
            state++;
        }
    }

    void FREE_SPIN() {
        if (BOSS1 != null) {
            if (Time.time > nextfire_S1)
            {
                BOSS1_S.shoot();
            }
            BOSS1_S.rotate(193);
            BOSS1_S.move_S1(80);
        }
        if (BOSS2 != null) {
            if (Time.time > nextfire_S1)
            {
                BOSS2_S.shoot();
            }
            BOSS2_S.rotate(-325);
            BOSS2_S.move_S1(60);
        }
        if (BOSS3 != null) {
            if (Time.time > nextfire_S1)
            {
                BOSS3_S.shoot();
            }
            BOSS3_S.rotate(482);
            BOSS3_S.move_S1(40);
        }

        if (Time.time > nextfire_S1)
        {
            nextfire_S1 = Time.time + firerate_S1;
        }

        if (Time.time > StateTime) {
            StateTime = Time.time + 0.5f;
            state++;
        }
    }

    void GET_TO_POS(float y1,float y2,float y3) {
        if (BOSS1 != null) BOSS1_S.move_toPos(80 * 2, y1);
        if (BOSS2 != null) BOSS2_S.move_toPos(80 * 2, y2);
        if (BOSS3 != null) BOSS3_S.move_toPos(80 * 2, y3);
        
        if (Time.time > StateTime)
        {
            StateTime = Time.time + 12.5f;
            state++;
        }
    }

    void UP_DOWN() {
        if (BOSS1 != null) {
            BOSS1.transform.LookAt(
                new Vector3(target.transform.position.x, BOSS1.transform.position.y, target.transform.position.z));
            
            if (Time.time > nextfire_S2)
            {
                BOSS1_S.shoot();
            }

            BOSS1_S.move_S1(80);
        }
        if (BOSS2 != null) {
            BOSS2.transform.LookAt(
                new Vector3(target.transform.position.x, BOSS2.transform.position.y, target.transform.position.z));
            
            if (Time.time > nextfire_S2)
            {
                BOSS2_S.shoot();
            }
            
            BOSS2_S.move_S1(60);
        }
        if (BOSS3 != null) {
            BOSS3.transform.LookAt(
                new Vector3(target.transform.position.x, BOSS3.transform.position.y, target.transform.position.z));

            if (Time.time > nextfire_S2)
            {
                BOSS3_S.shoot();
            }
            
            BOSS3_S.move_S1(40);
        }

        if (Time.time > nextfire_S2)
        {
            nextfire_S2 = Time.time + firerate_S2;
        }

        if (Time.time > StateTime)
        {
            StateTime = Time.time + 0.5f;
            state++;
        }
    }

    void LOCAL_SPIN() {
        if (BOSS1 != null) {
            if (Time.time > nextfire_S2)
            {
                BOSS1_S.shoot();
            }

            BOSS1_S.move_S1(60);
            BOSS1_S.rotate(60);
        }
        if (BOSS2 != null) {
            if (Time.time > nextfire_S2)
            {
                BOSS2_S.shoot();
            }
            BOSS2_S.rotate(-60);
        }
        if (BOSS3 != null) {
            if (Time.time > nextfire_S2)
            {
                BOSS3_S.shoot();
            }
            BOSS3_S.move_S1(60);
            BOSS3_S.rotate(-60);
        }

        if (Time.time > nextfire_S2)
        {
            nextfire_S2 = Time.time + firerate_S2;
        }

        if (Time.time > StateTime)
        {
            StateTime = Time.time + 1.0f;
            state++;
        }
    }

    void CHASE() {
        if (BOSS1 != null) 
            BOSS1_S.chase();

        if (BOSS2 != null)
            BOSS2_S.chase();

        if (BOSS3 != null)
            BOSS3_S.chase();

        if (Time.time > StateTime)
        {
            StateTime = Time.time + 5.5f;
            state++;
        }
    }

    void STRAIGHT() {
        if (BOSS1 != null) {
            if (Time.time > nextfire_S1)
            {
                BOSS1_S.shoot();
            }
            BOSS1_S.chase();

            BOSS1_S.straight();
        }

        if (BOSS2 != null) {
            if (Time.time > nextfire_S1)
            {
                BOSS2_S.shoot();
            }
            BOSS2_S.chase();
            BOSS2_S.straight();
        }

        if (BOSS3 != null) {
            if (Time.time > nextfire_S1)
            {
                BOSS3_S.shoot();
            }
            BOSS3_S.chase();
            BOSS3_S.straight();
        }

        if (Time.time > nextfire_S1)
        {
            nextfire_S1 = Time.time + firerate_S1;
        }
            if (Time.time > StateTime)
        {
            StateTime = Time.time + 0.5f;
            state ++;
        }
    }
}
