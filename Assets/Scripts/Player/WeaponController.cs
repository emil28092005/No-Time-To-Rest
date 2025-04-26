using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] int currentWeapon = 0;

    void Start() {
        weapons = GetComponentsInChildren<Weapon>();
        for (int i = 0; i < weapons.Length; ++i) weapons[i].SetWeaponState(i == currentWeapon);
    }

    void Update() {
        if (weapons.Length == 0) return;
        ChangeActiveWeapon((currentWeapon + (int)Input.mouseScrollDelta.y + weapons.Length) % weapons.Length);
        CheckButtonsForChangeWeapon();
        if (Input.GetMouseButton(0) && weapons[currentWeapon].isActive) weapons[currentWeapon].Shoot(Input.GetMouseButtonDown(0), Input.GetMouseButton(0));
    }

    void CheckButtonsForChangeWeapon() {
        KeyCode[] keys = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5};
        foreach (var (key, i) in keys.Select((val, ind) => (val, ind))) {
            if (Input.GetKeyDown(key)) ChangeActiveWeapon(i);
        }
    }

    void ChangeActiveWeapon(int ind) {
        if (ind < 0 || ind >= weapons.Length || ind == currentWeapon) return;
        weapons[currentWeapon].SetWeaponState(false);
        weapons[ind].SetWeaponState(true);
        currentWeapon = ind;
    }
}
