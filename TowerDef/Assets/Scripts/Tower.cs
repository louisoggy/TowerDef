using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;
    private Transform target;

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
        }
    }

    void FindTarget()
    {
        // find enemies in range
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        float closestDistance = Mathf.Infinity;
        target = null;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = hit.transform;
                }
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