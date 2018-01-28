using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Stage3_heli1 : MonoBehaviour {

    public GameObject target;
    public GameObject aimPos;
    public GameObject Bullet;
    public GameObject healthBar;
    public GameObject CBT;
    public GameObject explode;
    public GameObject shootPos1;
    public GameObject shootPos2;
    public GameObject shootPos3;
    public GameObject shootPos4;
    public GameObject GATE1;

    public float max_health;
    public float cur_health;
    public float keepdis;

    private bool active;
    private bool exploded;
    private float movespeed;
    private bool moveright;

    private float AttackRate_cent;
    private float fireRate_cent;
    private float nextAttack_cent;
    private float nextFire_cent;
    private int shoot_once_cent;

    private float AttackRate_side;
    private float fireRate_side;
    private float nextAttack_side;
    private float nextFire_side;
    private int shoot_once_side;

    private float Bullet_forward_force;

    // Use this for initialization
    void Start () {
        active = false;
        max_health = 300;
        cur_health = max_health;
        Bullet_forward_force = 100;
        movespeed = 50.0f;
        moveright = true;

        AttackRate_cent =0.5f;
        fireRate_cent = 0.1f;
        nextAttack_cent = 0;
        nextFire_cent = 0;
        shoot_once_cent = 0;

        AttackRate_side = 1.0f;
        fireRate_side = 0.1f;
        nextAttack_side = 0;
        nextFire_side = 0;
        shoot_once_side = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (transform.position.y < 220) transform.position += new Vector3(0, movespeed * Time.deltaTime, 0);
            if (transform.position.y > 220) transform.position = new Vector3(transform.position.x, 220, transform.position.z);

            float dis = target.transform.position.z + keepdis - this.transform.position.z;
            if (Mathf.Abs(dis) > 5)
            {
                if (dis > 0)
                {
                    this.transform.position += new Vector3(0, 0, movespeed * Time.deltaTime);
                }
                else
                {
                    this.transform.position -= new Vector3(0, 0, movespeed * Time.deltaTime);
                }
            }

                if (moveright == true) transform.position -= new Vector3(movespeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3(movespeed * Time.deltaTime, 0, 0);

            if (transform.position.x > -60) moveright = true;
            if (transform.position.x < -140) moveright = false;
         
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            transform.Rotate(new Vector3(0, 90, 20));
            
            shoot();
        }
        else if (!exploded)
        {
            if (GATE1 == null) 
            active = true;
        }
    }

    void shoot()
    {
        if (Time.time > nextAttack_side)
        {
            nextAttack_side = Time.time + AttackRate_side;
            shoot_once_side = 0;
        }
        if (Time.time > nextFire_side && shoot_once_side < 5)
        {
            shoot_once_side++;
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire_side = Time.time + fireRate_side;
            //Bullet.transform.Rotate(0, 90, 0);
            Vector3 side_dir = aimPos.transform.position - transform.position;

            if (shoot_once_side == 4)
            {
                GameObject[] temp_bullet = new GameObject[9];
                Rigidbody[] temp_rigid = new Rigidbody[9];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        temp_bullet[i * 3 + j] = Instantiate(
                            Bullet,
                            shootPos1.transform.position,
                            Bullet.transform.rotation) as GameObject;
                        temp_rigid[i * 3 + j] = temp_bullet[i * 3 + j].GetComponent<Rigidbody>();
                        temp_rigid[i * 3 + j].AddForce((
                                Quaternion.AngleAxis(5 * i - 5, Vector3.right) *
                                Quaternion.AngleAxis(5 * j - 5, Vector3.up) *
                                side_dir.normalized
                                ) * Bullet_forward_force * 100);
                        Destroy(temp_bullet[i * 3 + j], 5.0f);
                    }
                }
                GameObject[] temp_bullet1 = new GameObject[9];
                Rigidbody[] temp_rigid1 = new Rigidbody[9];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        temp_bullet1[i * 3 + j] = Instantiate(
                            Bullet,
                            shootPos4.transform.position,
                            Bullet.transform.rotation) as GameObject;
                        temp_rigid1[i * 3 + j] = temp_bullet1[i * 3 + j].GetComponent<Rigidbody>();
                        temp_rigid1[i * 3 + j].AddForce((
                                Quaternion.AngleAxis(5 * i - 5, Vector3.right) *
                                Quaternion.AngleAxis(5 * j - 5, Vector3.up) *
                                side_dir.normalized
                                ) * Bullet_forward_force * 100);
                        Destroy(temp_bullet1[i * 3 + j], 5.0f);
                    }
                }
            }
            else {
                GameObject temp_bullet;
                temp_bullet = Instantiate(
                    Bullet,
                    shootPos1.transform.position,
                    Bullet.transform.rotation) as GameObject;
                Rigidbody temp_rigid;
                temp_rigid = temp_bullet.GetComponent<Rigidbody>();
                temp_rigid.AddForce(side_dir.normalized * Bullet_forward_force * 100);
                GameObject temp_bullet1;
                Rigidbody temp_rigid1;
                temp_bullet1 = Instantiate(
                    Bullet,
                    shootPos4.transform.position,
                    Bullet.transform.rotation) as GameObject;
                temp_rigid1 = temp_bullet1.GetComponent<Rigidbody>();
                temp_rigid1.AddForce(side_dir.normalized * Bullet_forward_force * 100);
                Destroy(temp_bullet1, 5.0f);
            }
            
        }

        if (Time.time > nextAttack_cent)
        {
            nextAttack_cent = Time.time + AttackRate_cent;
            shoot_once_cent = 0;
        }
        if (Time.time > nextFire_cent && shoot_once_cent < 3)
        {
            shoot_once_cent++;
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire_cent = Time.time + fireRate_cent;
            //Bullet.transform.Rotate(0, 90, 0);
            Vector3 cent_dir2 = aimPos.transform.position - shootPos2.transform.position;
            GameObject temp_bullet;
            //Bullet.transform.Rotate(0, 90, 0);
            temp_bullet = Instantiate(
                Bullet,
                shootPos2.transform.position,
                Bullet.transform.rotation) as GameObject;

            Rigidbody temp_rigid;
            temp_rigid = temp_bullet.GetComponent<Rigidbody>();
            temp_rigid.AddForce(cent_dir2.normalized * Bullet_forward_force * 100);

            Vector3 cent_dir3 = aimPos.transform.position - shootPos3.transform.position;
            GameObject temp_bullet1;
            Rigidbody temp_rigid1;
            temp_bullet1 = Instantiate(
                Bullet,
                shootPos3.transform.position,
                Bullet.transform.rotation) as GameObject;
            temp_rigid1 = temp_bullet1.GetComponent<Rigidbody>();
            temp_rigid1.AddForce(cent_dir3.normalized * Bullet_forward_force * 100);
            Destroy(temp_bullet1, 5.0f);

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
        if (cur_health != 0)
        {
            initialCBT(health_loss.ToString());
        }

        cur_health -= health_loss;
        if (cur_health < 0)
        {
            cur_health = 0;
        }
        healthBar.transform.localScale = new Vector3(cur_health / max_health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

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

    void initialCBT(string text_damage)
    {
        GameObject temp_CBT = Instantiate(CBT) as GameObject;
        temp_CBT.transform.SetParent(transform.Find("EnemyCanvas"));
        temp_CBT.GetComponent<RectTransform>().transform.localPosition = CBT.transform.localPosition;
        temp_CBT.GetComponent<RectTransform>().transform.localRotation = CBT.transform.localRotation;
        temp_CBT.GetComponent<RectTransform>().transform.localScale = CBT.transform.localScale;

        temp_CBT.GetComponent<Text>().text = "-" + text_damage;
        temp_CBT.GetComponent<Animator>().SetTrigger("Hit");

        Destroy(temp_CBT, 1.0F);
    }
}
