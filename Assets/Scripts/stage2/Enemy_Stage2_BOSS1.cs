using UnityEngine;
using UnityEngine.UI;
//using System.MATH;
using System.Collections;

public class Enemy_Stage2_BOSS1 : MonoBehaviour {
    
    public GameObject explode;

    public GameObject target;
    public GameObject Bullet;

    public GameObject[] ShootPos = new GameObject[8];

    public float max_health;
    public float cur_health;
    private float Bullet_forward_force;

    private bool up;
    private bool active;
    private bool exploded;

    // Use this for initialization
    void Start () {
        active = false;
        max_health = 2500;
        cur_health = max_health;
        exploded = false;

        Bullet_forward_force = 40;
        up = true;
    }
	
	// Update is called once per frame
	void Update () {
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
                GameObject temp_explode;
                temp_explode = Instantiate(explode, transform.position, transform.rotation) as GameObject;
                temp_explode.transform.localScale = new Vector3(70, 70, 70);
                exploded = true;
            }
            //target.SendMessage("KillRobot");
            Destroy(gameObject, 1.0F);
        }
    }

    public void shoot() {
        for (int i = 0; i < 8; i++) {
            if (ShootPos[i].transform.position.z < 1015)
            {
                Vector3 shoot_dir = ShootPos[i].transform.position - transform.position;
                GameObject temp_bullet;
                Rigidbody temp_rigid;
                temp_bullet = Instantiate(Bullet, ShootPos[i].transform.position, Bullet.transform.rotation) as GameObject;
                temp_rigid = temp_bullet.GetComponent<Rigidbody>();
                temp_bullet.transform.localScale = new Vector3(5, 5, 5);
                temp_rigid.AddForce(shoot_dir.normalized * Bullet_forward_force * 100);
                Destroy(temp_bullet, 5.0f);
            }
        }
    }
    public void rotate(float Rotate_Speed)
    {
        transform.Rotate(0, Rotate_Speed * Time.deltaTime, 0);
    }
    public void move_S1(float move_Speed)
    {
        if (transform.position.y >= 120)
        {
            transform.position = new Vector3(transform.position.x, 120, transform.position.z);
            up = false;
        }
        if (transform.position.y <= 40)
        {
            transform.position = new Vector3(transform.position.x, 40, transform.position.z);
            up = true;
        }
        if (up) transform.position += new Vector3(0, move_Speed * Time.deltaTime, 0);
        else transform.position -= new Vector3(0, move_Speed * Time.deltaTime, 0);
    }

    public void move_toPos(float move_Speed, float y)
    {
        if (transform.position.y == y) return;
        if (transform.position.y < y)
        {
            transform.position += new Vector3(0, move_Speed * Time.deltaTime, 0);
            if (transform.position.y > y)
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        else {
            transform.position -= new Vector3(0, move_Speed * Time.deltaTime, 0);
            if (transform.position.y < y)
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
        } 
    }

    public void chase() {
        float move_Speed = Mathf.Abs(target.transform.position.y - transform.position.y) *2;
        transform.LookAt(
            new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        if (transform.position.y < target.transform.position.y)
            transform.position += new Vector3(0, move_Speed * Time.deltaTime, 0);
        else transform.position -= new Vector3(0, move_Speed * Time.deltaTime, 0);
    }
    public void straight()
    {
        transform.LookAt(
            new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        //transform.position =
        //    new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
    }
}
