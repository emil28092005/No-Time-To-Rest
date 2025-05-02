using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    public float speed = 1f;
    public float detectionRadius = 5f;
    public float patrollRadius = 3f;
    public float damage = 5f;
    public float attackRate = 1f;
    public float attackRadius = 1f;

    [SerializeField] Player target;
    [SerializeField] MeleeEnemyState currentState = MeleeEnemyState.Patrolling;
    Vector3 patrollingTarget;
    NavMeshAgent navMeshAgent;

    void Start() {
        target = FindFirstObjectByType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrollingTarget = GetPatrollingTarget();
    }

    void Update() {
        switch (currentState) {
            case MeleeEnemyState.Patrolling:
                Vector3 vectorToTarget = (target.transform.position - transform.position).normalized;
                bool canSee = false;
                if (Physics.Raycast(transform.position, vectorToTarget, out RaycastHit hit, detectionRadius)) {
                    if (hit.transform.CompareTag("Player")) canSee = true;
                }
                if (canSee) currentState = MeleeEnemyState.Pursuing;
                else {
                    if (Vector3.Distance(transform.position, patrollingTarget) <= 0.1f) patrollingTarget = GetPatrollingTarget();
                    navMeshAgent.destination = patrollingTarget;
                }
                break;
        }
    }

    Vector3 GetPatrollingTarget() {
        return Vector3.zero;
    }
}

enum MeleeEnemyState {
    Patrolling,
    Pursuing,
    Attacking
}
