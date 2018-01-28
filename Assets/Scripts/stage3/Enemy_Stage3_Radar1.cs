using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Stage3_Radar1 : MonoBehaviour {

    public GameObject healthBar;
    public GameObject CBT;
    public GameObject explode;
    public GameObject ExplodePos;

    public float max_health;
    public float cur_health;
    public bool exploded;

    // Use this for initialization
    void Start () {
        max_health = 1000;
        cur_health = max_health;
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
            if (exploded == false)
            {
                for (int i = -1; i < 1; i++) {
                    for (int j = -1; j < 1; j++) {
                        for (int k = -1; k < 1; k++) {
                            GameObject temp_explode;
                            Vector3 exPos = new Vector3(i*20,j*20,k*20);
                            temp_explode = Instantiate(explode, ExplodePos.transform.position + exPos, transform.rotation) as GameObject;
                            temp_explode.transform.localScale = new Vector3(100, 100, 100);
                            exploded = true;
                        }
                    }
                }
                
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
