using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerHealth = 20;
    public int currentWave = 1;
    public TMP_Text healthText;
    public TMP_Text waveText;
    public int gold = 50;
    public TMP_Text goldText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
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
        Debug.Log("Player health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}