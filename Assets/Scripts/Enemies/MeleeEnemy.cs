using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    public float detectionRadius = 5f;
    public float patrollRadius = 3f;
    public float damage = 5f;
    public float attackRate = 1f;
    public float attackRadius = 1f;

    [SerializeField] Player target;
    [SerializeField] MeleeEnemyState currentState = MeleeEnemyState.Patrolling;
    Vector3 patrollingTarget;
    NavMeshAgent navMeshAgent;
    float currentDelay = 0;

    void Start() {
        target = FindFirstObjectByType<Player>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrollingTarget = GetPatrollingTarget();
    }

    void Update() {
        switch (currentState) {
            case MeleeEnemyState.Patrolling:
                if (CanSeeTarget()) currentState = MeleeEnemyState.Pursuing;
                else {
                    if (Vector3.Distance(transform.position - new Vector3(0, navMeshAgent.baseOffset * transform.localScale.y), navMeshAgent.destination) <= 0.1f) patrollingTarget = GetPatrollingTarget();
                    navMeshAgent.destination = patrollingTarget;
                }
                break;
            case MeleeEnemyState.Pursuing:
                if (!CanSeeTarget()) currentState = MeleeEnemyState.Patrolling;
                navMeshAgent.destination = target.transform.position;
                if (Vector3.Distance(transform.position, target.transform.position) <= attackRadius) currentState = MeleeEnemyState.Attacking;
                break;
            case MeleeEnemyState.Attacking:
                navMeshAgent.ResetPath();
                if (Vector3.Distance(transform.position, target.transform.position) >= attackRadius) currentState = CanSeeTarget() ? MeleeEnemyState.Pursuing : MeleeEnemyState.Patrolling;
                else if (currentDelay >= 1 / attackRate) {
                    target.DealDamage(damage);
                    currentDelay = 0;
                }
                break;
        }
        if (attackRate != 0) {
            if (currentState == MeleeEnemyState.Attacking) currentDelay = Mathf.Clamp(currentDelay += Time.deltaTime, 0, 1 / attackRate);
            else currentDelay = 0;
        }
    }

    Vector3 GetPatrollingTarget() {
        NavMeshPath path = new();
        while (true) {
            Vector2 circle = Random.insideUnitCircle * patrollRadius;
            Vector3 maybeDestination = transform.position + new Vector3(circle.x, 0, circle.y);
            NavMesh.CalculatePath(transform.position, maybeDestination, NavMesh.AllAreas, path);
            if (path.status == NavMeshPathStatus.PathComplete) return maybeDestination;
            if (path.status != NavMeshPathStatus.PathInvalid) break;
        }
        Vector3 destination = path.corners[^1];
        destination.y = transform.position.y;
        return destination;
    }

    bool CanSeeTarget() {Vector3 vectorToTarget = (target.transform.position - transform.position).normalized;
        bool canSee = false;
        if (Physics.Raycast(transform.position, vectorToTarget, out RaycastHit hit, detectionRadius)) {
            if (hit.transform.CompareTag("Player")) canSee = true;
        }
        return canSee;
    }
}

enum MeleeEnemyState {
    Patrolling,
    Pursuing,
    Attacking
}
