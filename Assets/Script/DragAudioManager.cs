using UnityEngine;

public class DragAudioManager : MonoBehaviour
{
    public static DragAudioManager Instance;

    public AudioClip dragStartClip;
    public AudioClip dropSuccessClip;
    public AudioClip dropFailClip;

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton: yaln�zca bir tane DragAudioManager olmas�na izin ver
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�ince yok olmas�n
        }
        else
        {
            Destroy(gameObject); // Ba�ka varsa kendini sil
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D ses
    }

    public void PlayDragStart() => PlayClip(dragStartClip);
    public void PlayDropSuccess() => PlayClip(dropSuccessClip);
    public void PlayDropFail() => PlayClip(dropFailClip);

    private void PlayClip(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
