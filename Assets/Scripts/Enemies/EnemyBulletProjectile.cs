using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();
            player.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
