using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightBonus : MonoBehaviour
{
    public string targetTag = "level2";  // Vurgulanacak objelerin tag'i
    public Color highlightColor = Color.yellow;  // Vurgu rengi
    public float highlightDuration = 1.5f;        // Vurgu süresi

    public void ActivateHighlightBonus()
    {
        if (MarketManager.Instance == null)
        {
            Debug.LogWarning("MarketManager bulunamadý.");
            return;
        }

        if (!MarketManager.Instance.UseBonus("highlightbonus"))
        {
            Debug.Log("Highlight bonus yeterli deðil.");
            return;
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets == null || targets.Length == 0)
        {
            Debug.LogWarning("Highlight için hedef bulunamadý.");
            return;
        }

        List<GameObject> validTargets = new List<GameObject>();
        foreach (var t in targets)
        {
            if (t != null) validTargets.Add(t);
        }

        if (validTargets.Count == 0)
        {
            Debug.LogWarning("Tüm hedefler null, vurgulama yapýlmayacak.");
            return;
        }

        GameObject selected = validTargets[Random.Range(0, validTargets.Count)];
        if (selected == null)
        {
            Debug.LogWarning("Seçilen hedef null.");
            return;
        }

        var highlightable = selected.GetComponent<HighlightableObject>();
        if (highlightable == null)
        {
            Debug.LogWarning("Seçilen objede HighlightableObject component yok.");
            return;
        }

        highlightable.Highlight(highlightColor, highlightDuration);

        Debug.Log("Highlight bonus baþlatýldý.");
    }
}
