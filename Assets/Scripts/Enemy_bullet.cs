using UnityEngine;
using System.Collections;

public class Enemy_bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        //particle.Play();
        Destroy(this.gameObject);
    }
}
