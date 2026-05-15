using UnityEngine;

public class SupportTower : MonoBehaviour
{
    public float supportRange = 20f;   // radius in which nearby towers receive the bonus
    public float fireRateBonus = 2f;   // multiplier applied to fire rate of towers in range

    private LineRenderer lineRenderer;
    private int segments = 60;         // smoothness of range circle

    void Start()
    {
        SetupRangeIndicator();
        DrawRangeCircle();
        lineRenderer.enabled = false;  // hide range circle
        ApplyBonus();
    }

    // toggle range circle visibility
    public void SetRangeVisible(bool visible)
    {
        if (lineRenderer != null)
            lineRenderer.enabled = visible;
    }

    // create and configure the LineRenderer for the range circle
    void SetupRangeIndicator()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    // draw the range circle using trig to place points around the tower
    void DrawRangeCircle()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * supportRange;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * supportRange;
            lineRenderer.SetPosition(i, new Vector3(
                transform.position.x + x,
                transform.position.y,
                transform.position.z + z));
            angle += 360f / segments;
        }
    }

    // find all towers in range and multiply their fire rate by the bonus
    void ApplyBonus()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, supportRange);
        foreach (Collider hit in hits)
        {
            Tower tower = hit.GetComponent<Tower>();
            if (tower != null)
            {
                tower.fireRate *= fireRateBonus;
                Debug.Log("Support bonus applied to tower: " + hit.gameObject.name);
            }
        }
    }

    // visualise support range in the editor when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}