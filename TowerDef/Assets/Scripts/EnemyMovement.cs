using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypoint = 0;

    void Update()
    {
        if (currentWaypoint >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        Transform target = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position,
            target.position, speed * Time.deltaTime);

        // Rotate to face movement direction
        Vector3 direction = target.position - transform.position;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
            currentWaypoint++;
    }

    void ReachEnd()
    {
        GameManager.Instance.TakeDamage(1);
        Debug.Log("Enemy reached the end!");
        Destroy(gameObject);
    }
}