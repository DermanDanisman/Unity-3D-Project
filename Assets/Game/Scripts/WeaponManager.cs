using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject shootingEffect;
    int ammo = 10;
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray rayFromPlayer = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (ammo > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RaycastHit hit;
                if (Physics.Raycast(rayFromPlayer, out hit, 100))
                {
                    print("You are looking at the " + hit.transform.gameObject.name);
                    GameObject effect = Instantiate(shootingEffect, hit.point, Quaternion.identity);
                    Destroy(effect, 5);
                }
                ammo--;
                print(ammo + " bullet left!");

                if (hit.transform.name == "Target")
                {
                    hit.transform.GetComponent<HealthManager>().GotHit(10);
                }
        }
        else
        {
            print("You need to reload!");
        }
        }
    }
}
