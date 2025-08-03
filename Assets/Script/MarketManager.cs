using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    // Bonus anahtarlar�
    private readonly string[] allBonuses = { "highlightbonus", "timebonus", "giftbonus" };

    // Bonuslar�n sat�n al�nma durumu (adet)
    private Dictionary<string, int> bonusCounts = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeBonuses();
            LoadBonusCounts();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeBonuses()
    {
        // E�er PlayerPrefs'te yoksa 0 ile ba�lat
        foreach (string bonus in allBonuses)
        {
            if (!PlayerPrefs.HasKey(bonus))
            {
                PlayerPrefs.SetInt(bonus, 0);
            }
        }
        PlayerPrefs.Save();
    }

    private void LoadBonusCounts()
    {
        foreach (string bonus in allBonuses)
        {
            bonusCounts[bonus] = PlayerPrefs.GetInt(bonus, 0);
        }
    }

    private void SaveBonusCount(string bonusKey)
    {
        if (bonusCounts.ContainsKey(bonusKey))
        {
            PlayerPrefs.SetInt(bonusKey, bonusCounts[bonusKey]);
            PlayerPrefs.Save();
        }
    }

    // Bonusun ka� adet hakk� var
    public int GetBonusCount(string bonusKey)
    {
        if (bonusCounts.ContainsKey(bonusKey))
            return bonusCounts[bonusKey];
        return 0;
    }

    // Bonus sat�n al�nd���nda adet art�r
    public bool TryPurchaseBonus(string bonusKey, int cost)
    {
        if (!bonusCounts.ContainsKey(bonusKey))
            return false;

        int coins = GameStatsManager.Instance.GetCoins();
        if (coins >= cost)
        {
            GameStatsManager.Instance.SpendCoins(cost);

            bonusCounts[bonusKey]++;
            SaveBonusCount(bonusKey);

            BonusVisibilityController.UpdateAllBonusVisibility();
            return true;
        }

        return false;
    }

    // Bonus kullan�ld���nda adet azalt
    public bool UseBonus(string bonusKey)
    {
        if (!bonusCounts.ContainsKey(bonusKey) || bonusCounts[bonusKey] <= 0)
            return false;

        bonusCounts[bonusKey]--;
        SaveBonusCount(bonusKey);

        UpdateBonusVisibility(bonusKey);

        return true;
    }

    // Bonusun adedi 0 ise g�r�n�rl�k kapans�n, 1+ ise a��ls�n
    private void UpdateBonusVisibility(string bonusKey)
    {
        BonusVisibilityController[] controllers = Resources.FindObjectsOfTypeAll<BonusVisibilityController>();

        foreach (var controller in controllers)
        {
            if (controller.bonusKey == bonusKey)
            {
                bool shouldShow = GetBonusCount(bonusKey) > 0;
                controller.gameObject.SetActive(shouldShow);
            }
        }
    }

    // Oyunu a�arken g�r�n�rl�kleri g�ncellemek i�in
    private void Start()
    {
        foreach (var bonusKey in allBonuses)
        {
            UpdateBonusVisibility(bonusKey);
        }
    }
}
