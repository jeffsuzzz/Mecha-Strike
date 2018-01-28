using UnityEngine;
using System.Collections;

public class drop_bomb : MonoBehaviour {
    public GameObject Bullet;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= 5) {
            GameObject[] temp_bullet = new GameObject[75];
            Rigidbody[] temp_rigid = new Rigidbody[75];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 3; k++) {
                        temp_bullet[i * 5 + j+k * 25] = Instantiate(
                        Bullet,
                        transform.position,
                        Bullet.transform.rotation) as GameObject;

                        temp_rigid[i * 5 + j + k * 25] = temp_bullet[i * 5 + j * 25].GetComponent<Rigidbody>();
                        temp_rigid[i * 5 + j + k * 25].transform.localScale = (new Vector3(5, 5, 5));
                        temp_rigid[i * 5 + j + k * 25].AddForce(new Vector3((i - 2) * 0.3f, k*0.3f, (j - 2) * 0.3f) * 100 * 50);
                        Destroy(temp_bullet[i * 5 + j + k*25], 5.0f);
                    }
                    
                }
            }

            Destroy(this.gameObject);
        }
	}
}
