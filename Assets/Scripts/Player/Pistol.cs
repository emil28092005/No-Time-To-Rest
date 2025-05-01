using UnityEngine;

public class Pistol : Weapon
{
    public GameObject projectilePrefab;
    public Transform spawnPosition;
    public float fireRate = 1f;

    [SerializeField] float currentDelay = 0;

    public override void Shoot(bool keyDown, bool keyHold) {
        if (keyDown && (fireRate == 0 || currentDelay >= 1 / fireRate)) {
            Instantiate(projectilePrefab, spawnPosition.position, spawnPosition.rotation);
            currentDelay = 0;
        }
    }

    void Start() {
        if (fireRate != 0) currentDelay = 1 / fireRate;
    }

    void Update() {
        if (fireRate != 0) currentDelay = Mathf.Clamp(currentDelay += Time.deltaTime, 0, 1 / fireRate);
    }
}
