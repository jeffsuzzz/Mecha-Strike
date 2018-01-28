using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public float enemyDetectDistance;   
    public float walkingSpeed;
    public float max_health;
    public float cur_health;
    public GameObject healthBar;
    public GameObject bulletEmitter;
    public GameObject bullet;
    public float bulletforce;
	public int attack_type;
    public GameObject CBT;
    public GameObject explode;

    public GameObject target;
    //private Transform target;
    private UnityEngine.AI.NavMeshAgent navMA;
    private Vector3 bullet_rotate;
    private bool bullet_rotate_flag;
    private float detect_distance;
    private float targetDistance;

    // Use this for initialization
    void Start () {

        navMA = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMA.speed = walkingSpeed;
        navMA.stoppingDistance = 15;
        detect_distance = 100;
        targetDistance = 300;
        //target = GameObject.FindGameObjectWithTag("Player").transform;

        cur_health = max_health;
		
		switch(attack_type)
		{
            case 0:
                break;
			case 1:
				InvokeRepeating("WaitAndShoot", 2, 3.0f);
				break;
			case 2:
				InvokeRepeating("tri_shoot", 2, 3.0f);
				break;
			case 3:
				InvokeRepeating("explode_shoot", 2, 3.0f);
				break;
            case 4:
                bullet_rotate = new Vector3(0, 10, 0);
                bullet_rotate_flag = true;
                InvokeRepeating("round_shoot", 2, 0.4f);
                break;
		}
      
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = target.transform.position;
        targetDistance = Vector3.Distance(targetPos, transform.position);

        navMA.SetDestination(targetPos);
        if (cur_health != 0)
        {
            if (targetDistance < detect_distance) {
                navMA.Resume();
            }
            else
            {
                navMA.Stop();
            }
                
            if (targetDistance < 10)
            {
                navMA.updateRotation = false;
                Vector3 backwardpos = transform.position - targetPos;
                backwardpos.Normalize();
                navMA.Move(backwardpos);
            }
            else
                navMA.updateRotation = true;
        }
        else navMA.Stop();
    }
	
	void OnTriggerEnter(Collider other)
	{
		if(other.name == "sphere_bullet(Clone)"){
            set_healthBar(10);
        }

	}

    void OnColliderEnter(Collider other)
    {
        //if (other.name == "sphere_bullet(Clone)")
        //{
            set_healthBar(10);
        //}

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
            GameObject temp_explode;
            temp_explode = Instantiate(explode, transform.position, transform.rotation) as GameObject;
            temp_explode.transform.Translate(0, 5, 0);

            target.SendMessage("KillRobot");

            GameObject childHB = transform.GetChild(0).gameObject;
            childHB.SetActive(false);
            GameObject childObj = transform.GetChild(2).gameObject;
            childObj.SetActive(false);
            Destroy(gameObject, 1.0F);
        }
    }

    void initialCBT(string text_damage)
    {
        GameObject temp_CBT = Instantiate(CBT)as GameObject;
        temp_CBT.transform.SetParent(transform.Find("EnemyCanvas"));
        temp_CBT.GetComponent<RectTransform>().transform.localPosition = CBT.transform.localPosition;
        temp_CBT.GetComponent<RectTransform>().transform.localRotation = CBT.transform.localRotation;
        temp_CBT.GetComponent<RectTransform>().transform.localScale = CBT.transform.localScale;

        temp_CBT.GetComponent<Text>().text = "-" + text_damage;
        temp_CBT.GetComponent<Animator>().SetTrigger("Hit");

        Destroy(temp_CBT, 1.0F);
    }

	void WaitAndShoot()
    {   
		if(cur_health > 0 && targetDistance < detect_distance)
		{
			GameObject temp_bullet;
			temp_bullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
			Rigidbody temp_bulllet_rigid;
			temp_bulllet_rigid = temp_bullet.GetComponent<Rigidbody>();
			Vector3 bullet_dir;
			bullet_dir = target.transform.position - bulletEmitter.transform.position;
            bullet_dir.Normalize();

            temp_bulllet_rigid.AddForce(bullet_dir * (bulletforce + walkingSpeed)* 15);

			Destroy(temp_bullet, 5.0f);
		}	
    }

    void tri_shoot(){
		if(cur_health > 0 && targetDistance < detect_distance)
		{
			Vector3 bullet_dir1, bullet_dir2, bullet_dir3;
			bullet_dir1 = target.transform.position - transform.position;
            bullet_dir1.Normalize();
            bullet_dir2 = bullet_dir1 + bulletEmitter.transform.right;
			bullet_dir3 = bullet_dir1 - bulletEmitter.transform.right;          
            bullet_dir2.Normalize();
            bullet_dir2.Normalize();

            GameObject temp_bullet1, temp_bullet2, temp_bullet3;
			temp_bullet1 = Instantiate(bullet, bulletEmitter.transform.position + bullet_dir1, bulletEmitter.transform.rotation) as GameObject;
			temp_bullet2 = Instantiate(bullet, bulletEmitter.transform.position + bullet_dir2, bulletEmitter.transform.rotation) as GameObject;
			temp_bullet3 = Instantiate(bullet, bulletEmitter.transform.position + bullet_dir3, bulletEmitter.transform.rotation) as GameObject;
			
			Rigidbody temp_bulllet_rigid1, temp_bulllet_rigid2, temp_bulllet_rigid3;
			temp_bulllet_rigid1 = temp_bullet1.GetComponent<Rigidbody>();
			temp_bulllet_rigid2 = temp_bullet2.GetComponent<Rigidbody>();
			temp_bulllet_rigid3 = temp_bullet3.GetComponent<Rigidbody>();
						
            
			temp_bulllet_rigid1.AddForce(bullet_dir1 * (bulletforce + walkingSpeed) *15);
			temp_bulllet_rigid2.AddForce(bullet_dir2 * (bulletforce + walkingSpeed) *15);
			temp_bulllet_rigid3.AddForce(bullet_dir3 * (bulletforce + walkingSpeed) *15);

			Destroy(temp_bullet1, 5.0f);
			Destroy(temp_bullet2, 5.0f);
			Destroy(temp_bullet3, 5.0f);
		}	
	}
	
	void explode_shoot(){
		if(cur_health > 0 && targetDistance < detect_distance)
		{
			GameObject[] temp_bullet = new GameObject[27];
			Rigidbody[] temp_bulllet_rigid = new Rigidbody[27];
			Vector3[] bullet_dir = new Vector3[27];
			
			for(int x=-1; x<2; x++)
			{
				for(int y=-1; y<2; y++){
					for(int z=-1; z<2; z++){
						if(x==0 && y==0&& z==0)
						{
							continue;
						}
						bullet_dir[(x+1)*9+(y+1)*3+z+1] = new Vector3(x, y, z);
                        bullet_dir[(x + 1) * 9 + (y + 1) * 3 + z + 1].Normalize();

                        temp_bullet[(x+1)*9+(y+1)*3+z+1] = Instantiate(bullet, bulletEmitter.transform.position + bullet_dir[(x+1)*9+(y+1)*3+z+1], bulletEmitter.transform.rotation) as GameObject;
						temp_bulllet_rigid[(x+1)*9+(y+1)*3+z+1] = temp_bullet[(x+1)*9+(y+1)*3+z+1].GetComponent<Rigidbody>();
				
						temp_bulllet_rigid[(x+1)*9+(y+1)*3+z+1].AddForce(bullet_dir[(x+1)*9+(y+1)*3+z+1] * (bulletforce + walkingSpeed) * 10);

						Destroy(temp_bullet[(x+1)*9+(y+1)*3+z+1], 5.0f);
					}
				}	
			}
						
		}
	}

    void round_shoot()
    {
        if (cur_health > 0 && targetDistance < detect_distance)
        {
            GameObject temp_bullet;
            temp_bullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
            Rigidbody temp_bulllet_rigid;
            temp_bulllet_rigid = temp_bullet.GetComponent<Rigidbody>();
            Vector3 bullet_dir;
            bullet_dir = target.transform.position - transform.position;
            bullet_dir.Normalize();
            bullet_dir = Quaternion.Euler(bullet_rotate) * bullet_dir;

            if (bullet_rotate.y > 80)
                bullet_rotate_flag = false;
            else if (bullet_rotate.y < -80)
                bullet_rotate_flag = true;
            if (bullet_rotate_flag) {
                bullet_rotate.y += 10;
            }
            else bullet_rotate.y -= 10;

            temp_bulllet_rigid.AddForce(bullet_dir * (bulletforce + walkingSpeed)*15);
            
            Destroy(temp_bullet, 5.0f);
        }
    }

}
