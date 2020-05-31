﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject shootingEffect;
    [SerializeField] AudioClip gunShotSound;
    [SerializeField] GameObject grenade;
    
    int ammo;
    bool startTimer;
    bool canShoot;
    bool canReload;
    string currentWeapon;
    int weaponIndex;
    float timer;

    [SerializeField] string[] weapons;
    [SerializeField] int[] currentAmmo;
    [SerializeField] int[] clipSize;
    [SerializeField] int[] ammoPouch;
    [SerializeField] int[] maxAmmoPouch;
    [SerializeField] float[] reloadTime;
    //[SerializeField] bool[] hasWeapon;

    private void Start() 
    {
        canShoot = true;
        weaponIndex = 0;
        currentWeapon = weapons[weaponIndex];
        print("Current weapon: " + currentWeapon);
    }
    
    void Update()
    {
        WeaponSwitch();
        Shoot();
        ThrowGrenade();
    }

    void Shoot()
    {
        Ray rayFromPlayer = GetComponentInChildren<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Input.GetKeyDown(KeyCode.F) && currentAmmo[weaponIndex] > 0 && canShoot)
        {
            startTimer = true;
            currentAmmo[weaponIndex]--;
            print(currentAmmo[weaponIndex] + " bullet left!");
            
            GetComponentInChildren<AudioSource>().clip = gunShotSound;
            GetComponentInChildren<AudioSource>().Play();

            RaycastHit hit;
            if (Physics.Raycast(rayFromPlayer, out hit, 100) && currentWeapon != weapons[2])
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
        if (currentAmmo[weaponIndex] <= 0 && ammoPouch[weaponIndex] > 0)
        {
            canShoot = false;
            canReload = true;
            StartCoroutine(Reload());
        }
    }

    void ThrowGrenade()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentWeapon == weapons[2])
            {
                GameObject launcher = GameObject.Find("Launcher");
                GameObject g = Instantiate(grenade, launcher.transform.position, Quaternion.identity);
                g.GetComponent<Rigidbody>().AddForce(launcher.transform.forward * 500);
                Destroy(g, 5);
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
            print("Current weapon: " + currentWeapon + " Ammo: " + currentAmmo[weaponIndex]);
        }
    }

    IEnumerator Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("Reloading.....");
            yield return new WaitForSeconds(reloadTime[weaponIndex]);
            currentAmmo[weaponIndex] = clipSize[weaponIndex];
            ammoPouch[weaponIndex] -= currentAmmo[weaponIndex];
            print("Total ammo left: " + ammoPouch[weaponIndex]);
            canShoot = true;
            canReload = false;

        }
    }

    public string GetWeaponInfo()
    {
        return currentWeapon + "  " + currentAmmo[weaponIndex] + " / " + ammoPouch[weaponIndex];
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "AmmoBox")
        {
            ammoPouch[0] += 10;
            if (ammoPouch[0] >= maxAmmoPouch[0])
                ammoPouch[0] = maxAmmoPouch[0];

            ammoPouch[1] += 30;
            if (ammoPouch[1] >= maxAmmoPouch[1])
                ammoPouch[1] = maxAmmoPouch[1];

            ammoPouch[2] += 5;
            if (ammoPouch[2] >= maxAmmoPouch[2])
                ammoPouch[2] = maxAmmoPouch[2];

            Destroy(other.gameObject);
            print("Total Ammo: " + ammoPouch[weaponIndex]);
        }
    }

}
