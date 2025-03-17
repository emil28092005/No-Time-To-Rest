using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] int currentWeapon = 0;

    void Start() {
        weapons = GetComponentsInChildren<Weapon>();
        if (weapons.Length > currentWeapon) weapons[currentWeapon].SetWeaponState(true);
    }

    void Update() {
        if (Input.GetMouseButton(0) && weapons[currentWeapon].isActive) weapons[currentWeapon].Shoot(Input.GetMouseButtonDown(0), Input.GetMouseButton(0));
    }
}
