using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Stage3_turret5 : MonoBehaviour {

    public GameObject turret_cent;
    public GameObject turret_side;

    public GameObject target;
    public GameObject aimPos;
    public GameObject shootPos_cent;
    public GameObject shootPos_right;
    public GameObject shootPos_left;
    public GameObject Bullet;
    public GameObject healthBar;
    public GameObject CBT;
    public GameObject explode;
    public GameObject ExplodePos;
    public GameObject GATE2;

    public float max_health;
    public float cur_health;

    private bool active;
    private bool exploded;

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
        max_health = 400;
        cur_health = max_health;
        Bullet_forward_force = 100;

        AttackRate_cent = 1.0f;
        fireRate_cent = 0.1f;
        nextAttack_cent = 0;
        nextFire_cent = 0;

        AttackRate_side = 2.5f;
        fireRate_side = 0.1f;
        nextAttack_side = 0;
        nextFire_side = 0;
}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            transform.Rotate(new Vector3(0,-90,0));
            turret_cent.transform.LookAt(target.transform.position);
            turret_cent.transform.Rotate(new Vector3(-90, 0, 0));
            turret_side.transform.LookAt(target.transform.position);
            turret_side.transform.Rotate(new Vector3(90, 0, 0));
            shoot();
        }
        else if(!exploded){
            if(target.transform.position.z>1350) active = true;
            //if (GATE2 == null) active = false;
        }
        if (target.transform.position.z > 3000) active = false;
    }

    void shoot()
    {
        if (Time.time > nextAttack_side)
        {
            nextAttack_side = Time.time + AttackRate_side;
            shoot_once_side = 0;
        }
        if (Time.time > nextFire_side && shoot_once_side < 10)
        {
            shoot_once_side++;
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire_side = Time.time + fireRate_side;
            GameObject temp_bullet;
            //Bullet.transform.Rotate(0, 90, 0);
            Vector3 side_dir = aimPos.transform.position - turret_side.transform.position;
            temp_bullet = Instantiate(
                Bullet,
                shootPos_right.transform.position,
                Bullet.transform.rotation) as GameObject;
            Rigidbody temp_rigid;
            temp_rigid = temp_bullet.GetComponent<Rigidbody>();
            temp_rigid.AddForce(side_dir.normalized * Bullet_forward_force * 100);
            GameObject temp_bullet1;
            Rigidbody temp_rigid1;
            temp_bullet1 = Instantiate(
                Bullet,
                shootPos_left.transform.position,
                Bullet.transform.rotation) as GameObject;
            temp_rigid1 = temp_bullet1.GetComponent<Rigidbody>();
            temp_rigid1.AddForce(side_dir.normalized * Bullet_forward_force * 100);
            Destroy(temp_bullet1, 5.0f);
        }

        if (Time.time > nextAttack_cent)
        {
            nextAttack_cent = Time.time + AttackRate_cent;
            shoot_once_cent = 0;
        }
        if (Time.time > nextFire_cent && shoot_once_cent < 1)
        {
            shoot_once_cent++;
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire_cent = Time.time + fireRate_cent;
            //Bullet.transform.Rotate(0, 90, 0);
            Vector3 cent_dir = aimPos.transform.position - shootPos_cent.transform.position;
            GameObject[] temp_bullet = new GameObject[9];
            Rigidbody[] temp_rigid = new Rigidbody[9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp_bullet[i * 3 + j] = Instantiate(
                        Bullet,
                        shootPos_cent.transform.position,
                        Bullet.transform.rotation) as GameObject;
                    temp_rigid[i * 3 + j] = temp_bullet[i * 3 + j].GetComponent<Rigidbody>();
                    temp_rigid[i * 3 + j].AddForce((
                            Quaternion.AngleAxis(2 * j - 2, transform.up) *
                            Quaternion.AngleAxis(2 * i - 2, turret_cent.transform.right) *
                            cent_dir.normalized
                            ) * Bullet_forward_force * 100);
                    Destroy(temp_bullet[i * 3 + j], 5.0f);
                }
            }
            
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
                temp_explode = Instantiate(explode, ExplodePos.transform.position, transform.rotation) as GameObject;
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
