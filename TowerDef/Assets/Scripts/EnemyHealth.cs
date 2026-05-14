using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public int goldReward = 10;
    public float healthBarHeight = 2f;

    public GameObject healthBarPrefab;
    private Image healthBarFill;
    private GameObject healthBarInstance;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab,
                transform.position + Vector3.up * healthBarHeight,
                Quaternion.identity);
            // get the fill image (second child canvas)
            healthBarFill = healthBarInstance.GetComponentsInChildren<Image>()[1];
        }
    }

    void Update()
    {
        if (healthBarInstance != null)
        {
            // keep bar above enemy
            healthBarInstance.transform.position = transform.position + Vector3.up * healthBarHeight;
            // face the camera
            healthBarInstance.transform.forward = Camera.main.transform.forward;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        GameManager.Instance.AddGold(goldReward);
        if (healthBarInstance != null)
            Destroy(healthBarInstance);
        Destroy(gameObject);

        // check if all enemies are dead and all waves are done
        if (EnemySpawner.Instance.allWavesComplete &&
            GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
        {
            GameManager.Instance.WinGame();
        }
    }
}