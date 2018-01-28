using UnityEngine;
using System.Collections;

public class Stage2_GATE1 : MonoBehaviour {
    public GameObject RightDoor;
    public GameObject LeftDoor;
    public GameObject check1;
    public GameObject check2;
    public GameObject boundary;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (check1 == null && check2 == null && boundary!=null) {
            Destroy(boundary);
            
        }
        if (boundary == null) {
            if (RightDoor.transform.position.x < 35)
            {
                RightDoor.transform.position += new Vector3(35 * Time.deltaTime / 2, 0, 0);
                LeftDoor.transform.position -= new Vector3(35 * Time.deltaTime / 2, 0, 0);
            }
        }
	}
}
