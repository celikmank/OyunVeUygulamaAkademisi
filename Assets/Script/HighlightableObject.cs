using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HighlightableObject : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private Color originalColor; // E�er renk de�i�tirme de yapacaksan, ama �u an kullanm�yoruz.

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning($"{name}: CanvasGroup component bulunamad�.");
        }
    }

    public void Highlight(Color color, float duration)
    {
        if (canvasGroup == null)
        {
            Debug.LogWarning($"{name}: CanvasGroup bulunamad��� i�in Highlight yap�lam�yor.");
            return;
        }

        StopAllCoroutines();
        StartCoroutine(HighlightRoutine(duration));
    }

    private IEnumerator HighlightRoutine(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float alpha = (Mathf.Sin(elapsed * Mathf.PI * 4f) + 1f) / 2f; // 0 ile 1 aras�nda
            canvasGroup.alpha = alpha;

            Debug.Log($"{name} alpha: {alpha}"); // Alpha de�erini konsola yazd�r

            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f; // Bitince tam g�r�n�r yap
        Debug.Log($"{name} highlight tamamland�, alpha resetlendi.");
    }
}
