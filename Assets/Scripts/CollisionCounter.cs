using UnityEngine;
using System.Collections;

public class CollisionCounter : MonoBehaviour {

    public int counter;

	// Use this for initialization
	void Start () {
        counter = 0;
	}

    void OnTriggerEnter(Collider other)
    {
        counter++;
    }
    void OnTriggerExit(Collider other)
    {
        counter--;
    }

}
