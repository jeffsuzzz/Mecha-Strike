using UnityEngine;
using System.Collections;

public class Stage3_GATE2_control : MonoBehaviour {
    public GameObject boundary;
    public GameObject Radar;
    public GameObject Gate;
    public bool gateOpen;
    // Use this for initialization
    void Start () {
        gateOpen = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (gateOpen == true)
        {
            DestroyObject(boundary);
            if (Gate.transform.position.y > -200) Gate.transform.position -= new Vector3(0, 50 * Time.deltaTime, 0);
        }
        else
        {
            if (RadarDestroyed()) gateOpen = true;
        }
    }
    bool RadarDestroyed()
    {
        if (Radar == null) return true;
        return false;
    }
}
