using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] int currentWeapon = 0;

    void Start() {
        weapons = GetComponentsInChildren<Weapon>();
        for (int i = 0; i < weapons.Length; ++i) {
            if (i != currentWeapon) weapons[i].SetWeaponState(false);
        }
    }

    void Update() {
        if (Input.GetMouseButton(0) && weapons[currentWeapon].isActive) weapons[currentWeapon].Shoot(Input.GetMouseButtonDown(0), Input.GetMouseButton(0));
    }
}
