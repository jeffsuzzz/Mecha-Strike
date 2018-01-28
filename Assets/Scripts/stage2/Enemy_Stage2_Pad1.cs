using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Stage2_Pad1 : MonoBehaviour {

    public GameObject healthBar;
    public GameObject CBT;
    public GameObject explode;

    public float max_health;
    public float cur_health;

    private bool active;
    private bool exploded;

    // Use this for initialization
    void Start () {
        max_health = 500;
        cur_health = max_health;
        active = false;
        exploded = false;
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
