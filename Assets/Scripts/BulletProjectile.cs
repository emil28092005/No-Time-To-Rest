using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 20f;
    public float timeToLive = 5f;

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
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
