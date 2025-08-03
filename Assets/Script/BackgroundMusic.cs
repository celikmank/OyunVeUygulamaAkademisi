using UnityEngine;
using UnityEngine.UI;
public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    private AudioSource audioSource;

    [Header("Icon")]
    public Sprite soundOnIcon;     // Ses a��k simgesi
    public Sprite soundOffIcon;    // Ses kapal� simgesi
    public Image iconImage;        // Buton i�indeki Image referans�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateIcon();
    }

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.Play();

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (iconImage == null) return;

        iconImage.sprite = audioSource.isPlaying ? soundOnIcon : soundOffIcon;
    }
}
