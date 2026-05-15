using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;
    private Transform target;
    private Animator animator;

    public enum TargetingMode { Nearest, Strongest, First }
    public TargetingMode targetingMode = TargetingMode.Nearest;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        FindTarget();

        if (target == null) return;

        // rotate tower to enemy
        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        // fire at enemy
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(10f);
            if (animator != null)
                animator.SetBool("IsAttacking", true);

            fireCooldown = 1f / fireRate;
        }
    }

    void FindTarget()
    {
        // find all enemies in range
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        float bestValue = Mathf.Infinity;
        target = null;

        if (animator != null)
            animator.SetBool("IsAttacking", false);

        // evaluate each enemy based on targeting mode
        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            float value = 0f;

            if (targetingMode == TargetingMode.Nearest)
            {
                value = Vector3.Distance(transform.position, hit.transform.position);
            }
            else if (targetingMode == TargetingMode.Strongest)
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                value = eh != null ? -eh.currentHealth : 0f;
            }
            else if (targetingMode == TargetingMode.First)
            {
                EnemyMovement em = hit.GetComponent<EnemyMovement>();
                value = em != null ? -em.currentWaypoint : 0f;
            }

            if (value < bestValue)
            {
                bestValue = value;
                target = hit.transform;
            }
        }
    }

    // visualise range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}