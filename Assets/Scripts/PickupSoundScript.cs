using UnityEngine;
using System.Collections;

public class PickupSoundScript : MonoBehaviour {
    public AudioClip pickupSound;
    public ParticleSystem particle;

    private AudioSource audio;
    private ParticleSystem p;
    private MeshRenderer mesh;
    private bool flag;    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        p = GetComponent<ParticleSystem>();
        mesh = GetComponent<MeshRenderer>();
        flag = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!flag)
        {
            audio.PlayOneShot(pickupSound, 1.0F);
            p.Play();
        }
        flag = true;
        //mesh.enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(WaitAndPrint(5.0F));   
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
