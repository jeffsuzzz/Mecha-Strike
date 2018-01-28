using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

    EnemyAI parentScript;
    public GameObject target;

    private Animator animator;
    private Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        targetPos = target.transform.position;
        parentScript  = transform.parent.GetComponent<EnemyAI>();

        float targetDistance = Vector3.Distance(targetPos, transform.position);
        animator.SetFloat("Distance", targetDistance);
        animator.SetFloat("Life", parentScript.cur_health);
    }

    // Update is called once per frame
    void Update()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;    
        targetPos = target.transform.position;
        float targetDistance = Vector3.Distance(targetPos, transform.position);

        animator.SetFloat("Distance", targetDistance);
        animator.SetFloat("Life", parentScript.cur_health);
    }
  
}
