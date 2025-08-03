using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance;

    private int coins;
    private int xp;  // Yeni: XP için deðiþken

    public event Action OnStatsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadStats()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        xp = PlayerPrefs.GetInt("XP", 0);  // Yeni: XP yükleme
    }

    private void SaveStats()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("XP", xp);     // Yeni: XP kaydetme
        PlayerPrefs.Save();
        OnStatsChanged?.Invoke();
    }

    public int GetCoins() => coins;
    public int GetXP() => xp;  // Yeni: XP getter

    public void SpendCoins(int amount)
    {
        coins = Mathf.Max(0, coins - amount);
        SaveStats();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        SaveStats();
    }

    public void AddXP(int amount)  // Yeni: XP ekleme
    {
        xp += amount;
        SaveStats();
    }
}
