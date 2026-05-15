using UnityEngine;
using UnityEngine.EventSystems;
public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public int[] towerCosts;
    public LayerMask placementLayer;
    private int selectedTower = 0;
    private Tower.TargetingMode selectedTargetingMode = Tower.TargetingMode.Nearest;

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            selectedTargetingMode = (Tower.TargetingMode)(((int)selectedTargetingMode + 1) % 3);
            GameManager.Instance.UpdateTargetingModeUI(selectedTargetingMode.ToString());
        }
    }

    void PlaceTower()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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
            GameObject placed = Instantiate(towerPrefabs[selectedTower], placementPosition, Quaternion.identity);
            Tower tower = placed.GetComponentInChildren<Tower>();
            if (tower != null)
                tower.targetingMode = selectedTargetingMode;
        }
    }
}