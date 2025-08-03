using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class DropZone : MonoBehaviour, IDropHandler
{
    [Header("Tag Kontrolü")]
    public List<string> acceptedTags;

    [Header("Yöneticiler")]
    public LevelProgressManager levelProgressManager;
    public LevelStartManager levelStartManager;
    public LevelEndManager levelEndManager;

    [Header("Bonuslar")]
    public HighlightBonus highlightBonus;

    [Header("Ayarlar")]
    public float maxAllowedTime = 6f;

    [Header("Sesler")]
    public AudioClip dropSuccessSound;
    [Range(0f, 1f)] public float successVolume = 0.6f;

    public AudioClip dropFailSound;
    [Range(0f, 1f)] public float failVolume = 0.4f;

    private int correctDropCount = 0;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject == null) return;

        string droppedTag = droppedObject.tag;

        if (acceptedTags.Contains(droppedTag))
        {
            if (droppedTag == "TimeBonus")
            {
                HandleBonusDrop(droppedObject);
            }
            else if (droppedTag == "HighlightBonus")
            {
                HandleHighlightBonusDrop(droppedObject);
            }
            else
            {
                HandleCorrectDrop(droppedObject);
            }
        }
        else
        {
            PlaySound(dropFailSound, failVolume);
            ResetDraggedItemPosition(droppedObject);
        }
    }

    private void HandleBonusDrop(GameObject bonusObject)
    {
        PlaySound(dropSuccessSound, successVolume);

        if (levelStartManager != null)
        {
            levelStartManager.AddBonusTime(10f);
            Debug.Log("Bonus zamaný eklendi.");
        }
        else
        {
            Debug.LogWarning("LevelStartManager referansý atanmadý!");
        }

        Destroy(bonusObject);
    }

    private void HandleHighlightBonusDrop(GameObject bonusObject)
    {
        PlaySound(dropSuccessSound, successVolume);

        if (highlightBonus != null)
        {
            highlightBonus.ActivateHighlightBonus();
            Debug.Log("Highlight bonus çalýþtýrýldý.");
        }
        else
        {
            Debug.LogWarning("HighlightBonus referansý atanmadý!");
        }

        Destroy(bonusObject);
    }

    private void HandleCorrectDrop(GameObject droppedObject)
    {
        PlaySound(dropSuccessSound, successVolume);

        correctDropCount++;
        if (levelProgressManager != null)
        {
            levelProgressManager.RegisterCorrectDrop();
        }

        Destroy(droppedObject);
    }

    private void ResetDraggedItemPosition(GameObject droppedObject)
    {
        DraggableItem dragItem = droppedObject.GetComponent<DraggableItem>();
        if (dragItem != null)
        {
            dragItem.ResetToOriginalPosition();
        }
        else
        {
            Debug.LogWarning("DropZone: DraggableItem script bulunamadý.");
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
