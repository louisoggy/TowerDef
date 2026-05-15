using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton instance accessible from other scripts
    public static GameManager Instance;

    public int playerHealth = 20;
    public int currentWave = 1;

    // UI text
    public TMP_Text healthText;
    public TMP_Text waveText;
    public TMP_Text goldText;
    public TMP_Text fpsText;
    public TMP_Text selectedTowerText;
    public TMP_Text targetingModeText;

    public int gold = 200;

    // panels shown on game over or win
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private bool rangeVisible = false;  // tracks whether range circles are shown

    void Awake()
    {
        // set singleton reference on awake so other scripts can access it
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        UpdateTargetingModeUI("Nearest");  // default targeting mode display
    }

    void Update()
    {
        // calculate and display FPS each frame
        float fps = 1f / Time.deltaTime;
        if (fpsText != null)
            fpsText.text = "FPS: " + Mathf.Round(fps);

        // toggle range circles on all towers with R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            rangeVisible = !rangeVisible;
            Tower[] towers = FindObjectsByType<Tower>(FindObjectsSortMode.None);
            foreach (Tower t in towers)
                t.SetRangeVisible(rangeVisible);
            SupportTower[] supportTowers = FindObjectsByType<SupportTower>(FindObjectsSortMode.None);
            foreach (SupportTower s in supportTowers)
                s.SetRangeVisible(rangeVisible);
        }
    }

    // refresh all HUD text elements
    public void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "Lives: " + playerHealth;
        if (waveText != null)
            waveText.text = "Wave: " + currentWave;
        if (goldText != null)
            goldText.text = "Gold: " + gold;
    }

    // add gold and refresh UI
    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold: " + gold);
        UpdateUI();
    }

    // attempt to spend gold, returns false if insufficient
    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;
        gold -= amount;
        UpdateUI();
        return true;
    }

    // update the selected tower name display
    public void UpdateSelectedTower(string towerName)
    {
        if (selectedTowerText != null)
            selectedTowerText.text = "Selected: " + towerName;
    }

    // reduce player health and trigger game over if health reaches zero
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        UpdateUI();
        if (playerHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;  // freeze game on game over
        }
    }

    // update the targeting mode display text
    public void UpdateTargetingModeUI(string mode)
    {
        if (targetingModeText != null)
            targetingModeText.text = "Targeting: " + mode;
    }

    // show win panel and freeze game
    public void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // resume time and load the next level
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    // restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}