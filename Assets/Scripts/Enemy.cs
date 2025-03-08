using UnityEditor.Embree;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f;

    public void DealDamage(float damage) {
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);
    }
}