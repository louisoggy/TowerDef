using UnityEngine;

public class TowerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}