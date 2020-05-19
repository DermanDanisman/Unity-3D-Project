using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            other.gameObject.transform.position = GameObject.Find("Start").transform.position + Vector3.up;
        }
    }
}
