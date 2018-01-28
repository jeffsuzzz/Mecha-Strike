using UnityEngine;
using System.Collections;


public class sphere_bullet_script : MonoBehaviour
{
    public ParticleSystem particle;
    
    // Use this for initialization
    void Start () {
        particle = GetComponent<ParticleSystem>();
    }

    /*void OnCollisionEnter(Collision col) {
        //particle.Play();
            Destroy(this.gameObject);
    }*/

    void OnTriggerEnter(Collider other)
    {
        //particle.Play();
        if(other.name != "sphere_bullet(Clone)")
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
