using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject target;
    [SerializeField] int launchVelocity = 1000;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            transform.LookAt(target.transform.position);
            proj.GetComponent<Rigidbody>().AddForce(transform.forward * launchVelocity);
            Destroy(proj, 3f);
            timer = 0;
        }

    }
}
