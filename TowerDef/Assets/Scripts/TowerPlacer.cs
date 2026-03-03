using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public LayerMask placementLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    void PlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
        {
            // Place tower
            Vector3 placementPosition = new Vector3(
                hit.transform.position.x,
                hit.transform.position.y + hit.transform.localScale.y,
                hit.transform.position.z
            );

            Instantiate(towerPrefab, placementPosition, Quaternion.identity);
            Debug.Log("Tower placed at: " + placementPosition);
        }
    }
}