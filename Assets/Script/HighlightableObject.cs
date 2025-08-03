using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HighlightableObject : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private Color originalColor; // Eðer renk deðiþtirme de yapacaksan, ama þu an kullanmýyoruz.

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning($"{name}: CanvasGroup component bulunamadý.");
        }
    }

    public void Highlight(Color color, float duration)
    {
        if (canvasGroup == null)
        {
            Debug.LogWarning($"{name}: CanvasGroup bulunamadýðý için Highlight yapýlamýyor.");
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
            float alpha = (Mathf.Sin(elapsed * Mathf.PI * 4f) + 1f) / 2f; // 0 ile 1 arasýnda
            canvasGroup.alpha = alpha;

            Debug.Log($"{name} alpha: {alpha}"); // Alpha deðerini konsola yazdýr

            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f; // Bitince tam görünür yap
        Debug.Log($"{name} highlight tamamlandý, alpha resetlendi.");
    }
}
