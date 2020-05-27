using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    WeaponManager weaponManager;

    [SerializeField] Text weaponNameText;
    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GameObject.Find("FPSController").GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponNameText.text = weaponManager.GetWeaponInfo();
    }
}
