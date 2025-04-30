using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 50f;
    public float explosionRadius = 5f;
    public float timeToLive = 3f;

    float currentTime = 0;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
    }

    void Update() {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToLive) Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders) {
            if (collider.gameObject.CompareTag("Enemy")) {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.DealDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
