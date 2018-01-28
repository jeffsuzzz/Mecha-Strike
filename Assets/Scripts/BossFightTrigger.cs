using UnityEngine;
using System.Collections;

public class BossFightTrigger : MonoBehaviour {

    public GameObject BossFightBounds;
    public GameObject Boss;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        BossFightBounds.SetActive(true);
        Boss.SetActive(true);
    }


}
