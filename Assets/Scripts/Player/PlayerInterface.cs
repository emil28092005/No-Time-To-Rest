using System;
using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public TMP_Text textTime;
    public TMP_Text textHP;
    public TMP_Text textBullets;

    Player player;
    WeaponController weaponController;
    LevelController levelController;

    void Start() {
        player = GetComponentInParent<Player>();
        weaponController = GetComponentInParent<WeaponController>();
        levelController = FindFirstObjectByType<LevelController>();
    }

    void Update() {
        if (levelController) textTime.text = TimeSpan.FromSeconds(levelController.GetTimeSpent()).ToString(@"mm\:ss");
        textHP.text = $"{(int)player.GetHp()}/{player.maxHp}";
        Weapon currentWeapon = weaponController.GetCurrentWeapon();
        textBullets.text = currentWeapon && currentWeapon.bulletType != BulletType.None ? GameController.i.GetBulletCount(currentWeapon.bulletType).ToString() : "-";
    }
}
