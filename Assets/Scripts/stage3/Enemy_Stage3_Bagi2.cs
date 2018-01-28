﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Stage3_Bagi2 : MonoBehaviour {

    public GameObject target;
    public GameObject aimPos;
    public GameObject shootPos;
    public GameObject Bullet;
    public GameObject healthBar;
    public GameObject CBT;
    public GameObject explode;
    public GameObject ExplodePos;

    public float max_health;
    public float cur_health;

    private bool active;
    private bool exploded;
    private float moveSpeed;
    private float AttackRate;
    private float fireRate;
    private float nextAttack;
    private float nextFire;
    private int shoot_once;
    private float Bullet_forward_force;
    private Vector3 shootDir;

    // Use this for initialization
    void Start () {
        active = false;
        moveSpeed = 25;
        AttackRate = 1.0f;
        fireRate = 0.1f;
        nextAttack = 0;
        nextFire = 0;
        shoot_once = 0;
        max_health = 200;
        cur_health = max_health;
        Bullet_forward_force = 50.0f;
        exploded = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            float dir = target.transform.position.x - this.transform.position.x;
            if ( Mathf.Abs(dir) > 5) {
                if (dir > 0)
                {
                    this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                else {
                    this.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                if (this.transform.position.x > -50)
                    this.transform.position =  new Vector3(-50, this.transform.position.y, this.transform.position.z);
                if (this.transform.position.x < -205)
                    this.transform.position = new Vector3(-205, this.transform.position.y, this.transform.position.z);

            }
            shootDir = (aimPos.transform.position - shootPos.transform.position).normalized;
            shoot();
        }
        else if(exploded == false){
            //float dis = Vector3.Distance(target.transform.position,this.transform.position);
            if (target.transform.position.z > -250) active = true;
        }
        if (target.transform.position.z > 3000) active = false;
    }

    void shoot()
    {
        if (Time.time > nextAttack) {
            nextAttack = Time.time + AttackRate;
            shoot_once = 0;
        }
        if (Time.time > nextFire && shoot_once < 3)
        {
            shoot_once++;
            //audio.PlayOneShot(PistalSE, 1.0F);
            nextFire = Time.time + fireRate;
            GameObject temp_bullet;
            //Bullet.transform.Rotate(0, 90, 0);
            temp_bullet = Instantiate(
                Bullet,
                shootPos.transform.position,
                Bullet.transform.rotation) as GameObject;
            Rigidbody temp_rigid;
            temp_rigid = temp_bullet.GetComponent<Rigidbody>();
            temp_rigid.AddForce(shootDir * Bullet_forward_force * 100);
            Destroy(temp_bullet, 5.0f);
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
            if (exploded == false) {
                GameObject temp_explode;
                temp_explode = Instantiate(explode, ExplodePos.transform.position, transform.rotation) as GameObject;
                temp_explode.transform.localScale = new Vector3(50, 50, 50);
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