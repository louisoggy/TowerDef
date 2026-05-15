using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs; // 0 = Mage, 1 = Support
    public int[] towerCosts;
    public LayerMask placementLayer;
    private int selectedTower = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedTower = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedTower = 1;

        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTower = 0;
            GameManager.Instance.UpdateSelectedTower("Mage Tower");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = 1;
            GameManager.Instance.UpdateSelectedTower("Support Tower");
        }
        if (Input.GetMouseButtonDown(0))
            PlaceTower();
    }

    void PlaceTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
        {
            int cost = towerCosts[selectedTower];
            if (!GameManager.Instance.SpendGold(cost))
            {
                Debug.Log("Not enough gold!");
                return;
            }

            Vector3 placementPosition = new Vector3(
                hit.transform.position.x,
                hit.transform.position.y + hit.transform.localScale.y,
                hit.transform.position.z
            );
            Instantiate(towerPrefabs[selectedTower], placementPosition, Quaternion.identity);
            Debug.Log("Placed tower " + selectedTower + " at: " + placementPosition);
        }
    }
}