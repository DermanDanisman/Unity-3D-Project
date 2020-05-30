using System.Collections;
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

            if (currentWeapon == weapons[2])
            {
                Vector3 pos = GetComponentInChildren<Camera>().transform.position;
                Instantiate(grenade, pos, Quaternion.identity);
                GetComponent<Rigidbody>().AddForce(transform.forward * 100);
            }
            
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
        if (currentAmmo[weaponIndex] <= 0 && ammoPouch[weaponIndex] > 0)
        {
            canShoot = false;
            StartCoroutine(Reload());
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
