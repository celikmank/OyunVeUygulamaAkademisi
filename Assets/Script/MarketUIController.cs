using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketUIController : MonoBehaviour
{
    public GameObject marketPanel;
    public Button openMarketButton;
    public Button closeMarketButton;

    [Header("Ses Ayarları")]
    public AudioClip marketOpenSound;
    [Range(0f, 1f)] public float soundVolume = 0.7f;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        marketPanel.SetActive(false); // Başlangıçta kapalı

        openMarketButton.onClick.AddListener(() =>
        {
            PlaySound(marketOpenSound, soundVolume);
            marketPanel.SetActive(true);
        });

        closeMarketButton.onClick.AddListener(() =>
        {
            marketPanel.SetActive(false);
        });
    }
    void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
