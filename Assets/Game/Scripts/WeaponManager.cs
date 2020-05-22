using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject shootingEffect;
    int ammo;
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray rayFromPlayer = GetComponentInChildren<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (ammo > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ammo--;
                print(ammo + " bullet left!");

                RaycastHit hit;
                if (Physics.Raycast(rayFromPlayer, out hit, 100))
                {
                    print("You are looking at the " + hit.transform.gameObject.name);
                    GameObject effect = Instantiate(shootingEffect, hit.point, Quaternion.identity);
                    Destroy(effect, 5);
                }
                if (hit.transform.name == "Target")
                {
                    hit.transform.GetComponent<HealthManager>().GotHit(10);
                }
                else return;
        }
        if (ammo <= 0)
        {
            print("You need to reload!");
        }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "AmmoBox")
        {
            ammo = 10;
            if (ammo <= 10)
                ammo = 10;
            Destroy(other.gameObject);
            print("Ammo: " + ammo);
        }
    }

}
