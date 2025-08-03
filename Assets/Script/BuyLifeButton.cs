using TMPro;
using UnityEngine;

public class BuyLifeButton : MonoBehaviour
{
    public int coinCost = 10;
    public int amountToAdd = 1;
    public TextMeshProUGUI warningText;

    public void OnBuyLifeClick()
    {
        bool result = LivesManager.Instance.BuyLife(coinCost, amountToAdd);

        if (!result && warningText != null)
        {
            warningText.text = "Yeterli coin yok!";
            Invoke(nameof(ClearWarning), 2f); // 2 saniye sonra temizle
        }
    }

    void ClearWarning()
    {
        if (warningText != null)
            warningText.text = "";
    }
}
