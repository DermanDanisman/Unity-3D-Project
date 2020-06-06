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

    [SerializeField] float rangeOfDistance;
    // Start is called before the first frame update
    void Start()
    {  
        animator = GetComponent<Animator>();
        objInFront = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        AttackPlayer();
    }

    void FollowPlayer()
    {
        //ray = new Ray();
        ray.origin = transform.position + Vector3.up * 1.5f;
        ray.direction = transform.forward * 10;
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
            objInFront = hit.collider.gameObject;
        }

        if (objInFront.gameObject.tag == "Player")
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            animator.SetBool("canSeePlayer", true);
            gameObject.GetComponent<NavMeshAgent>().SetDestination(objInFront.gameObject.transform.position);

            Vector3 npcDirection = objInFront.gameObject.transform.position - transform.position;
            npcDirection.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(npcDirection), Time.time * 2);
        }
        else if (objInFront.gameObject.tag != "Player")
        {
            animator.SetBool("canSeePlayer", false);
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }

       
    }

    void AttackPlayer()
    {
        rangeOfDistance = Vector3.Distance(GameObject.Find("FPSController").transform.position, transform.position);
        if (rangeOfDistance < 2f)
        {
            animator.SetBool("withinRange", true);
        }
        else
        {
            animator.SetBool("withinRange", false);
        }
    }
}
