using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BonusVisibilityController : MonoBehaviour
{
    public string bonusKey;

    private static List<BonusVisibilityController> allBonusControllers = new List<BonusVisibilityController>();

    private void OnEnable()
    {
        allBonusControllers.Add(this);
    }

    private void OnDisable()
    {
        allBonusControllers.Remove(this);
    }

    private IEnumerator Start()
    {
        while (MarketManager.Instance == null)
            yield return null;

        UpdateVisibility();
    }

    public void UpdateVisibility()
    {
        bool isActive = MarketManager.Instance.GetBonusCount(bonusKey) > 0;
        gameObject.SetActive(isActive);
    }

    public static void UpdateAllBonusVisibility()
    {
        var copy = new List<BonusVisibilityController>(allBonusControllers);

        foreach (var controller in copy)
        {
            if (controller != null)
                controller.UpdateVisibility();
        }
    }

}
