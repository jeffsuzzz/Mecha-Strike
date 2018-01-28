using UnityEngine;
using System.Collections;

public class PlayerClearAnimator : MonoBehaviour {

    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        animator.SetBool("go", false);
        Invoke("Go", 2.5f);
        Destroy(gameObject, 6);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Go()
    {
        animator.SetBool("go", true);
    }
}
