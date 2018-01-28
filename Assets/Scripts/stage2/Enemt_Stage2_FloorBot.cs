using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemt_Stage2_FloorBot : MonoBehaviour {

    public GameObject target;
    public GameObject aimPos;
    public GameObject partner;
    public GameObject shootPos;
    public GameObject Bullet;
    public GameObject healthBar;
    public GameObject CBT;
    public GameObject explode;
    public GameObject GATE;

    public float max_health;
    public float cur_health;

    private bool active;
    private bool exploded;
    
    private float fireRate;
    private float nextFire;

    private float Bullet_forward_force;

    // Use this for initialization
    void Start () {
        max_health = 500;
        cur_health = max_health;
        active = false;

        fireRate = 1.0f;
        nextFire = 0;
        Bullet_forward_force = 50;
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            shoot();
        }
        else {
            if (target.transform.position.z >0 
                || cur_health < max_health
                || partner == null)
            active = true;
        }
        transform.LookAt(new Vector3(aimPos.transform.position.x, transform.position.y, aimPos.transform.position.z));
    }

    void shoot()
    {
        if (Time.time > nextFire)
        {
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire = Time.time + fireRate;
            //Bullet.transform.Rotate(0, 90, 0);
            Vector3 cent_dir = aimPos.transform.position - shootPos.transform.position;
            GameObject[] temp_bullet = new GameObject[9];
            Rigidbody[] temp_rigid = new Rigidbody[9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp_bullet[i * 3 + j] = Instantiate(
                        Bullet,
                        shootPos.transform.position,
                        Bullet.transform.rotation) as GameObject;
                    temp_rigid[i * 3 + j] = temp_bullet[i * 3 + j].GetComponent<Rigidbody>();
                    temp_rigid[i * 3 + j].AddForce((
                            Quaternion.AngleAxis(3 * j - 3, transform.up) *
                            Quaternion.AngleAxis(3 * i - 3, transform.right) *
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
