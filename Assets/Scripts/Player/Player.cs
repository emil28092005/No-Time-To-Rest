using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHp = 100;

    [SerializeField] float hp;

    void Start() {
        hp = maxHp;
    }

    public void DealDamage(float damage) {
        hp -= damage;
        if (hp  <= 0) {
            FindAnyObjectByType<LevelController>().OnPlayerDie();
        }
    }
}
