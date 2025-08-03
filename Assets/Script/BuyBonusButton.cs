using TMPro;
using UnityEngine;

public class BuyBonusButton : MonoBehaviour
{
    public string bonusKey; // Örn: "highlightbonus"
    public int cost = 20;
    public TextMeshProUGUI warningText;

    public void OnClickBuy()
    {
        if (MarketManager.Instance == null)
        {
            Debug.LogError("MarketManager.Instance null!");
            return;
        }

        if (warningText == null)
        {
            Debug.LogError("warningText reference is missing!");
            return;
        }

        if (string.IsNullOrEmpty(bonusKey))
        {
            Debug.LogError("bonusKey is null or empty!");
            return;
        }

        // Satýn alma iþlemi
        if (MarketManager.Instance.TryPurchaseBonus(bonusKey, cost))
        {
            warningText.text = "Item Purchased!";

            // Bonuslarýn görünürlüðünü güncelle
            BonusVisibilityController.UpdateAllBonusVisibility();
        }
        else
        {
            warningText.text = "Not Enough Coin!";
        }
        if (MarketManager.Instance.TryPurchaseBonus(bonusKey, cost))
        {
            warningText.text = "Item Purchased!";

            // Bonus görünürlüðünü güncelle
            BonusVisibilityController.UpdateAllBonusVisibility();
        }

        Invoke(nameof(ClearWarning), 2f);

    }

    private void ClearWarning()
    {
        warningText.text = "";
    }
}
