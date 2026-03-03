using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth = 20;
    public int currentWave = 1;

    public TMP_Text healthText;
    public TMP_Text waveText;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        healthText.text = "Lives: " + playerHealth;
        waveText.text = "Wave: " + currentWave;
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Player health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}