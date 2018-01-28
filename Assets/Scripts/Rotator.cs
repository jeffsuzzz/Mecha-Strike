using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public float rotateSpeed = 360f;
	
	void Update () {
        //rotateSpeed = 720f;
        transform.Rotate (new Vector3(0, rotateSpeed*Time.deltaTime,0), Space.World);
	}
}
