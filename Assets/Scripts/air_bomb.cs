using UnityEngine;
using System.Collections;

public class air_bomb : MonoBehaviour {
    public GameObject Bullet;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y >= 200) {
            GameObject[] temp_bullet = new GameObject[49];
            Rigidbody[] temp_rigid = new Rigidbody[49];
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    temp_bullet[i * 7 + j] = Instantiate(
                        Bullet,
                        transform.position - new Vector3(0,2,0),
                        Bullet.transform.rotation) as GameObject;
                    
                    temp_rigid[i * 7 + j] = temp_bullet[i * 7 + j].GetComponent<Rigidbody>();
                    temp_rigid[i * 7 + j].transform.localScale = (new Vector3(5,5,5));
                    temp_rigid[i * 7 + j].AddForce(new Vector3((i-3)*0.3f,-1,(j-3)*0.3f) * 100 * 50);
                    Destroy(temp_bullet[i * 7 + j], 5.0f);
                }
            }

            Destroy(this.gameObject);
        }
	}
}
