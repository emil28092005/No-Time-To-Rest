using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isActive { get; private set; } = true;

    public abstract void Shoot(bool keyDown, bool keyHold);

    public void SetWeaponState(bool active) {
        isActive = active;
        GetComponent<MeshRenderer>().enabled = active;
    }
}