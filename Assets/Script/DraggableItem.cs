using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DraggableItem : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startAnchoredPosition;
    private Transform startParent;
    private Canvas canvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startAnchoredPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;

        if (canvas != null)
            transform.SetParent(canvas.transform);

        DragAudioManager.Instance?.PlayDragStart();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (transform.parent != startParent)
        {
            DragAudioManager.Instance?.PlayDropSuccess(); // Baþarýlý býrakma
        }
        else
        {
            rectTransform.anchoredPosition = startAnchoredPosition;
            transform.SetParent(startParent);
            DragAudioManager.Instance?.PlayDropFail(); // Hatalý býrakma
        }
    }

    public void ResetToOriginalPosition()
    {
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startAnchoredPosition;
    }

    private float GetCanvasScaleFactor()
    {
        return canvas != null ? canvas.scaleFactor : 1f;
    }

}
