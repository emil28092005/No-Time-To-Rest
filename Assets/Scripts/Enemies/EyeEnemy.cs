using UnityEngine;

public class EyeEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPosition;
    public float attackRange = 10f;
    public float fireRate = 1f;
    public float offsetDistance = 1f;

    [SerializeField] float currentDelay = 0;

    [SerializeField] Player target;

    void Start() {
        target = FindFirstObjectByType<Player>();
    }

    void Update() {
        if (fireRate != 0) currentDelay = Mathf.Clamp(currentDelay += Time.deltaTime, 0, 1 / fireRate);
        Vector3 offset = (target.transform.position - projectileSpawnPosition.position).normalized * offsetDistance;
        bool doShoot = false;
        if (Physics.Raycast(projectileSpawnPosition.position + offset, offset, out RaycastHit hit, attackRange)) {
            if (hit.transform.CompareTag("Player")) doShoot = true;
        }
        if (doShoot && (fireRate == 0 || currentDelay >= 1 / fireRate)) {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position + offset, transform.rotation);
            projectile.transform.LookAt(target.transform);
            currentDelay = 0;
        }
    }
}
