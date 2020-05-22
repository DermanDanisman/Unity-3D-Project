using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject shootingEffect;
    
    int ammo;
    float reloadTime;
    bool startTimer;
    bool canShoot = true;
    string currentWeapon;
    int weaponIndex;

    [SerializeField] string[] weapons;
    [SerializeField] int[] currentAmmo;
    [SerializeField] int[] clipSize;
    //[SerializeField] bool[] hasWeapon;

    private void Start() 
    {
        weaponIndex = 0;
        currentWeapon = weapons[weaponIndex];
        print("Current weapon: " + currentWeapon);
    }
    void Update()
    {
        WeaponSwitch();
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
                if (hit.transform.tag == "Target")
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

    void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentWeapon == weapons[0])
            {
                weaponIndex++;
                print(weaponIndex);
                currentWeapon = weapons[weaponIndex];
            }
            else if (currentWeapon == weapons[1])
            {
                weaponIndex++;
                print(weaponIndex);
                currentWeapon = weapons[weaponIndex];
            }
            else if (currentWeapon == weapons[2])
            {
                weaponIndex = 0;
                print(weaponIndex);
                currentWeapon = weapons[weaponIndex];
            }
            print("Current weapon: " + currentWeapon + " | " + weaponIndex);
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
