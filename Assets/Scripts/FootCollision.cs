using UnityEngine;
using System.Collections;

public class FootCollision : MonoBehaviour
{
    public bool OnGround;

    // Use this for initialization
    void Start()
    {
        OnGround = true;
    }

    // Update is called once per frame
    /*void Update()
    {

    }*/
    void OnCollisionEnter(Collision col)
    {
        OnGround = true;
    }
    void OnCollisionExit(Collision other)
    {
        OnGround = false;
    }
}
