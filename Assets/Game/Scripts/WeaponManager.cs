using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray rayFromPlayer;
        rayFromPlayer = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * 10, Color.red);
    }
}
