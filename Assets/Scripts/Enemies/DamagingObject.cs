using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    public float hitRate = 1f;
    public float damage = 5f;

    [SerializeField] float currentDelay = 0;

    void Start() {
        if (hitRate != 0) currentDelay = 1 / hitRate;
    }

    void Update() {
        if (hitRate != 0) currentDelay = Mathf.Clamp(currentDelay += Time.deltaTime, 0, 1 / hitRate);
    }

    public void ReverseOnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.controller.CompareTag("Player") && currentDelay == 1 / hitRate) {
            Player player = hit.controller.GetComponent<Player>();
            player.DealDamage(damage);
            currentDelay = 0;
        }
    }
}
