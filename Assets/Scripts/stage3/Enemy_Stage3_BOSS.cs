using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Stage3_BOSS : MonoBehaviour {
    
    public Slider BOSS_HP;

    public GameObject target;
    public GameObject aimPos;
    public GameObject Bullet;
    public GameObject air_bomb;
    public GameObject drop_bomb;
    public GameObject explode;
    public GameObject big_explode;
    public GameObject ExplodePos;

    public GameObject trishot_cent;
    public GameObject trishot_right;
    public GameObject trishot_left;

    public GameObject front_cent;
    public GameObject front_right;
    public GameObject front_left;

    public GameObject airbomb_right;
    public GameObject airbomb_left;

    public GameObject dropbomb_right;
    public GameObject dropbomb_left;

    public float max_health;
    public float cur_health;
    private float moveSpeed;

    private bool active;
    private bool forward;
    private bool exploded;
    private int State;
    private int LastState;
    
    private float fireRate;
    private float nextFire;
    
    private float fireRate_tri;
    private float nextFire_tri;
    private int shoot_once_tri;

    private float fireRate_air;
    private float nextFire_air;
    private int shoot_once_air;

    private float explotion_Rate;
    private float next_explotion;
    private int explode_time;

    private bool air_right;

    private float Bullet_forward_force;

    // Use this for initialization
    void Start () {

        active = false;
        forward = true;
        max_health = 10000;
        cur_health = max_health;
        Bullet_forward_force = 100;
        moveSpeed = 75;
        State = 0;

        //AttackRate_tri = 10.0f;
        fireRate_tri = 0.1f;
        //nextAttack_tri = 0;
        nextFire_tri = 0;
        shoot_once_tri = 0;

        fireRate_air = 0.5f;
        nextFire_air = 0;
        shoot_once_air = 0;
        air_right = true;

        explotion_Rate = 0.25f;
        next_explotion = 0;
        explode_time = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (target.transform.position.z > 3000) {
            active = true; }
        if (active && !exploded) {
            BOSS_HP.value = cur_health / max_health;
            switch (State)
            {
                case 0:
                    if (transform.position.x > 150) transform.position -= new Vector3(50 * Time.deltaTime, 0, 0);
                    else State = 1;
                    break;
                case 1:
                    if (Time.time > nextFire_tri && shoot_once_tri < 50)
                    {
                        nextFire_tri = Time.time + fireRate_tri;
                        trishot();
                        //front_shot();
                        shoot_once_tri++;
                    }
                    if (shoot_once_tri == 50) {
                        if (transform.position.x > -85) forward = true;
                        else forward = false;
                        State = 2;
                    }
                    break;

                case 2:
                    if (forward) transform.position -= new Vector3(300 * Time.deltaTime, 0, 0);
                    else transform.position += new Vector3(300 * Time.deltaTime, 0, 0);
                    if (transform.position.x > 150) {
                        transform.position = new Vector3(150, transform.position.y, transform.position.z);
                        shoot_once_air = 0;
                        State = 3;
                    }
                    if (transform.position.x < -320) {
                        transform.position = new Vector3(-320, transform.position.y, transform.position.z);
                        shoot_once_air = 0;
                        State = 3;
                    }
                    break;

                case 3:
                    if (Time.time > nextFire_air && shoot_once_air < 10)
                    {
                        nextFire_air = Time.time + fireRate_air;
                        air_shot();
                        shoot_once_air++;
                    }
                    if (shoot_once_air == 10)
                    {
                        shoot_once_tri =0;
                        State = 1;
                    }
                    break;
                default:
                    break;
            }

            if (State > 0 && State !=2)
            {
                if (transform.position.x >= target.transform.position.x + 10 || transform.position.x >= 150) forward = true;
                else if (transform.position.x <= target.transform.position.x - 100 || transform.position.x <= -320) forward = false;
                if (transform.position.x > 150) transform.position = new Vector3(150, transform.position.y, transform.position.z);
                if (transform.position.x < -320) transform.position = new Vector3(-320, transform.position.y, transform.position.z);

                if (forward) transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                else transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            
        }
        if (exploded == true)
        {
            if (Time.time > next_explotion)
            {
                if (explode_time < 8)
                {
                    next_explotion = Time.time + explotion_Rate;
                    GameObject temp_explode;
                    temp_explode = Instantiate(explode, ExplodePos.transform.position, transform.rotation) as GameObject;
                    temp_explode.transform.localScale = new Vector3(100, 100, 100);
                }
                if (explode_time == 8)
                {
                    GameObject temp_explode;
                    temp_explode = Instantiate(big_explode, ExplodePos.transform.position, transform.rotation) as GameObject;
                    temp_explode.transform.localScale = new Vector3(100, 100, 100);
                    Destroy(gameObject, 1.0F);
                    explode_time++;
                }

                explode_time++;
            }
            
        }
    }

    void trishot() {
        GameObject[] temp_bullet = new GameObject[5];
        Rigidbody[] temp_rigid = new Rigidbody[5];
        Vector3 tri_cent_dir = aimPos.transform.position - trishot_cent.transform.position;
        float miniRotate =5f- Mathf.Abs((5 * Time.time) % 20-10);
        for (int i = 0; i < 5; i++)
        {
            temp_bullet[i] = Instantiate(
                Bullet,
                trishot_cent.transform.position,
                Bullet.transform.rotation) as GameObject;
            temp_rigid[i] = temp_bullet[i].GetComponent<Rigidbody>();
            temp_rigid[i].transform.localScale = new Vector3(5, 5, 5);
            temp_rigid[i].AddForce((
                    // Quaternion.AngleAxis(2 * j - 2, transform.up) *
                    Quaternion.AngleAxis(6 * i - 12 + miniRotate, transform.up) *
                    tri_cent_dir.normalized
                    ) * Bullet_forward_force * 100);
            Destroy(temp_bullet[i], 5.0f);
        }

        if (target.transform.position.z > 3090) {
            temp_bullet = new GameObject[5];
            temp_rigid = new Rigidbody[5];
            Vector3 tri_right_dir = aimPos.transform.position - trishot_right.transform.position;
            for (int i = 0; i < 5; i++)
            {
                temp_bullet[i] = Instantiate(
                    Bullet,
                    trishot_right.transform.position,
                    Bullet.transform.rotation) as GameObject;
                temp_rigid[i] = temp_bullet[i].GetComponent<Rigidbody>();
                temp_rigid[i].transform.localScale = new Vector3(5, 5, 5);
                temp_rigid[i].AddForce((
                        // Quaternion.AngleAxis(2 * j - 2, transform.up) *
                        Quaternion.AngleAxis(6 * i - 12 + miniRotate, transform.up) *
                        tri_right_dir.normalized
                        ) * Bullet_forward_force * 100);
                Destroy(temp_bullet[i], 5.0f);
            }
        }

        if (target.transform.position.z < 3290) {
            temp_bullet = new GameObject[5];
            temp_rigid = new Rigidbody[5];
            Vector3 tri_left_dir = aimPos.transform.position - trishot_left.transform.position;
            for (int i = 0; i < 5; i++)
            {
                temp_bullet[i] = Instantiate(
                    Bullet,
                    trishot_left.transform.position,
                    Bullet.transform.rotation) as GameObject;
                temp_rigid[i] = temp_bullet[i].GetComponent<Rigidbody>();
                temp_rigid[i].transform.localScale = new Vector3(5, 5, 5);
                temp_rigid[i].AddForce((
                        // Quaternion.AngleAxis(2 * j - 2, transform.up) *
                        Quaternion.AngleAxis(6 * i - 12 + miniRotate, transform.up) *
                        tri_left_dir.normalized
                        ) * Bullet_forward_force * 100);
                Destroy(temp_bullet[i], 5.0f);
            }
        }
        
    }

    void air_shot() {
        if (air_right)
        {
            GameObject temp_bomb;
            Rigidbody temp_rigid;
            Vector3 bomb_dir = new Vector3(target.transform.position.x, 200, target.transform.position.z) - airbomb_right.transform.position;
            temp_bomb = Instantiate(air_bomb, airbomb_right.transform.position, air_bomb.transform.rotation) as GameObject;
            temp_rigid = temp_bomb.GetComponent<Rigidbody>();
            temp_rigid.AddForce(bomb_dir.normalized * Bullet_forward_force * 200);
            air_right = false;
        }
        else {
            GameObject temp_bomb;
            Rigidbody temp_rigid;
            Vector3 bomb_dir = new Vector3(target.transform.position.x, 200, target.transform.position.z) - airbomb_left.transform.position;
            temp_bomb = Instantiate(air_bomb, airbomb_left.transform.position, air_bomb.transform.rotation) as GameObject;
            temp_rigid = temp_bomb.GetComponent<Rigidbody>();
            temp_rigid.AddForce(bomb_dir.normalized * Bullet_forward_force * 200);
            air_right = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "sphere_bullet(Clone)")
        {
            set_healthBar(10);
        }
    }

    void set_healthBar(float health_loss)
    {
        cur_health -= health_loss;
        if (cur_health < 0)
        {
            cur_health = 0;
        }
        
        if (cur_health == 0)
        {
            active = false;
            if (exploded == false)
            {
                exploded = true;
            }
            
            //target.SendMessage("KillRobot");
        }
        
    }
}
