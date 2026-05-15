using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs;   // array of tower prefabs assigned in the Inspector
    public int[] towerCosts;            // gold cost for each tower
    public LayerMask placementLayer;    // layer that defines valid placement zones

    private int selectedTower = 0;      // currently selected tower type
    private Tower.TargetingMode selectedTargetingMode = Tower.TargetingMode.Nearest;  // default targeting mode
    void Update()
    {
        // press 1 to select Mage Tower
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTower = 0;
            GameManager.Instance.UpdateSelectedTower("Mage Tower");
        }

        // press 2 to select Support Tower
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = 1;
            GameManager.Instance.UpdateSelectedTower("Support Tower");
        }

        // left click to place the selected tower
        if (Input.GetMouseButtonDown(0))
            PlaceTower();

        // press T to cycle through targeting modes Nearest, Strongest and First
        if (Input.GetKeyDown(KeyCode.T))
        {
            selectedTargetingMode = (Tower.TargetingMode)(((int)selectedTargetingMode + 1) % 3);
            GameManager.Instance.UpdateTargetingModeUI(selectedTargetingMode.ToString());
        }
    }

    void PlaceTower()
    {
        // prevent placement if clicking on UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
        {
            // check player has enough gold before placing
            int cost = towerCosts[selectedTower];
            if (!GameManager.Instance.SpendGold(cost))
            {
                Debug.Log("Not enough gold!");
                return;
            }

            // place tower on top of the placement zone tile
            Vector3 placementPosition = new Vector3(
                hit.transform.position.x,
                hit.transform.position.y + hit.transform.localScale.y,
                hit.transform.position.z
            );

            // place the tower and apply the currently selected targeting mode
            GameObject placed = Instantiate(towerPrefabs[selectedTower], placementPosition, Quaternion.identity);
            Tower tower = placed.GetComponentInChildren<Tower>();
            if (tower != null)
                tower.targetingMode = selectedTargetingMode;
        }
    }
}