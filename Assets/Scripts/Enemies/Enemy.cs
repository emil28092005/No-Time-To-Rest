using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f;
    public GameObject prefab;

    public void DealDamage(float damage) {
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);
    }
}