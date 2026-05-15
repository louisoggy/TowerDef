using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerHealth = 20;
    public int currentWave = 1;
    public int gold = 50;
    public TMP_Text healthText;
    public TMP_Text waveText;
    public TMP_Text fpsText;
    public TMP_Text goldText;
    public TMP_Text selectedTowerText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        float fps = 1f / Time.deltaTime;
        if (fpsText != null)
            fpsText.text = "FPS: " + Mathf.Round(fps);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold: " + gold);
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;
        gold -= amount;
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "Lives: " + playerHealth;
        if (waveText != null)
            waveText.text = "Wave: " + currentWave;
        if (goldText != null)
            goldText.text = "Gold: " + gold;
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        UpdateUI();
        if (playerHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void UpdateSelectedTower(string towerName)
    {
        if (selectedTowerText != null)
            selectedTowerText.text = "Selected: " + towerName;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }
}