using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo info;
    RaycastHit hit;
    Ray ray;
    GameObject objInFront;
    // Start is called before the first frame update
    void Start()
    {  
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ray = new Ray();
        ray.origin = transform.position + Vector3.up;
        ray.direction = transform.forward * 10;
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
            objInFront = hit.collider.gameObject;
            print("Name: " + objInFront.name);
        }

        if (objInFront.tag == "Player")
        {
            animator.SetBool("canSeePlayer", true);
            gameObject.GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("FPSController").transform.position);
        }
        else
        {
            animator.SetBool("canSeePlayer", false);
        }

        if (info.IsName("Idle"))
        {
            //GetComponent<NavMeshAgent>().Stop();
        }
        if (info.IsName("Follow"))
        {

            //GetComponent<NavMeshAgent>().Resume();
        }
    }
}
