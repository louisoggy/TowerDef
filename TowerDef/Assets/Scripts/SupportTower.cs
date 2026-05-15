using UnityEngine;

public class SupportTower : MonoBehaviour
{
    public float supportRange = 20f;
    public float fireRateBonus = 2f;
    private LineRenderer lineRenderer;
    private int segments = 60;

    void Start()
    {
        SetupRangeIndicator();
        DrawRangeCircle();
        lineRenderer.enabled = false;
        ApplyBonus();
    }

    public void SetRangeVisible(bool visible)
    {
        if (lineRenderer != null)
            lineRenderer.enabled = visible;
    }

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}