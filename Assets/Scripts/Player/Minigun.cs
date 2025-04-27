using UnityEngine;

public class Minigun : Weapon
{
    public GameObject projectilePrefab;
    public Transform spawnPosition;
    public float fireRate = 10f;

    [SerializeField] float currentDelay = 0;

    public override void Shoot(bool keyDown, bool keyHold) {
        if (keyHold && (fireRate == 0 || currentDelay >= 1 / fireRate) && GameController.i.ConsumeBullet(bulletType)) {
            Instantiate(projectilePrefab, spawnPosition.position, spawnPosition.rotation);
            currentDelay = 0;
        }
    }

    void Update() {
        if (fireRate != 0) currentDelay = Mathf.Clamp(currentDelay += Time.deltaTime, 0, 1 / fireRate);
    }
}
