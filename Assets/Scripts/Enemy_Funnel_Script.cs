using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Funnel_Script : MonoBehaviour {

    public GameObject target;
    public GameObject Bullet;
    public GameObject healthBar;
    public GameObject CBT;

    public float SenceDistance;
    public float KeepDistance;
    private float Speed;
    public float max_health;
    public float cur_health;

    private bool active;
    private bool chasing;
    private bool rotating;
    private int rotatedir;
    private float fireRate;
    private float nextFire;
    private float Bullet_forward_force;

    // Use this for initialization
    void Start () {
        Speed = 100.0f;
        active = true;
        chasing = false;
        rotating = false;
        rotatedir = (int)Random.value * 10 % 2;
        cur_health = max_health;
        fireRate = 1.0f;
        Bullet_forward_force = 25.0f;
    }
	
	// Update is called once per frame
	void Update () {

        if (active) {

            Vector3 targetPos = target.transform.position + new Vector3(0, 10f, 0);
            this.transform.forward = (targetPos - this.transform.position).normalized;

            if (Vector3.Distance(targetPos, this.transform.position) < SenceDistance)
                chasing = true;

            if (chasing == true && rotating == false)
            {
                if (Vector3.Distance(targetPos, this.transform.position) > KeepDistance)
                {
                    this.transform.position += this.transform.forward * Speed * Time.deltaTime;
                }
                else
                {
                    rotating = true;
                }
            }

            if (rotating)
            {
                this.transform.position = targetPos -
                    (Quaternion.AngleAxis(180 * Time.deltaTime, Vector3.up) *
                    this.transform.forward).normalized * KeepDistance;
                shoot();
            }

        }
        
    }

    void shoot() {
        if (Time.time > nextFire)
        {
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire = Time.time + fireRate;
            GameObject temp_bullet;
            Bullet.transform.Rotate(0, 90, 0);
            temp_bullet = Instantiate(
                Bullet, 
                this.transform.position + this.transform.forward*5, 
                Bullet.transform.rotation) as GameObject;
            Rigidbody temp_rigid;
            temp_rigid = temp_bullet.GetComponent<Rigidbody>();
            temp_rigid.AddForce(this.transform.forward * Bullet_forward_force * 100);
            Destroy(temp_bullet, 2.0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "sphere_bullet(Clone)")
        {
            set_healthBar(10);
        }

    }

    /*void OnColliderEnter(Collider other)
    {
        if (other.name == "sphere_bullet(Clone)")
        {
                set_healthBar(10);
        }
    }*/

    void set_healthBar(float health_loss)
    {
        if (cur_health != 0)
        {
            initialCBT(health_loss.ToString());
        }

        cur_health -= health_loss;
        if (cur_health <= 0)
        {
            cur_health = 0;
            active = false;
            Destroy(this.gameObject, 1.0f);
        }
        
        healthBar.transform.localScale = new Vector3(cur_health / max_health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

    }

    void initialCBT(string text_damage)
    {
        GameObject temp_CBT = Instantiate(CBT) as GameObject;
        temp_CBT.transform.SetParent(transform.Find("EnemyCanvas"));
        temp_CBT.GetComponent<RectTransform>().transform.localPosition = CBT.transform.localPosition;
        temp_CBT.GetComponent<RectTransform>().transform.localRotation = CBT.transform.localRotation;
        temp_CBT.GetComponent<RectTransform>().transform.localScale = CBT.transform.localScale;

        temp_CBT.GetComponent<Text>().text = "-" + text_damage;

        Destroy(temp_CBT, 1.0F);
    }
}
