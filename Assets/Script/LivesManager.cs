using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public int maxLives = 3;
    public float refillDuration = 180f; // 3 dakika = 180 saniye

    private int currentLives;
    private DateTime lastCheckedTime;

    [SerializeField] private TextMeshProUGUI livesText;

    public static LivesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLives();
        UpdateLivesUI();
        InvokeRepeating(nameof(CheckLifeRegeneration), 1f, 5f);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FindLivesTextDelayed());
    }

    private System.Collections.IEnumerator FindLivesTextDelayed()
    {
        yield return new WaitForSeconds(0.1f); // sahne tam yüklensin

        if (livesText == null)
        {
            GameObject foundText = GameObject.Find("LivesText");
            if (foundText != null)
                livesText = foundText.GetComponent<TextMeshProUGUI>();
        }

        UpdateLivesUI();
    }

    private void LoadLives()
    {
        currentLives = PlayerPrefs.GetInt("PlayerLives", maxLives);
        string savedTime = PlayerPrefs.GetString("LastLifeTime", "");

        if (!DateTime.TryParse(savedTime, out lastCheckedTime))
        {
            lastCheckedTime = DateTime.Now;
        }
    }

    private void SaveLives()
    {
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        PlayerPrefs.SetString("LastLifeTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    public bool UseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            SaveLives();
            UpdateLivesUI();
            return true;
        }
        return false;
    }

    private void CheckLifeRegeneration()
    {
        if (currentLives >= maxLives) return;

        TimeSpan timePassed = DateTime.Now - lastCheckedTime;
        int livesToAdd = (int)(timePassed.TotalSeconds / refillDuration);

        if (livesToAdd > 0)
        {
            currentLives = Mathf.Min(currentLives + livesToAdd, maxLives);
            lastCheckedTime = DateTime.Now.AddSeconds(-(timePassed.TotalSeconds % refillDuration));
            SaveLives();
            UpdateLivesUI();
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = currentLives.ToString();
    }

    public void AddLives(int amount)
    {
        currentLives = Mathf.Min(currentLives + amount, maxLives);
        SaveLives();
        UpdateLivesUI();
    }

    public bool BuyLife(int cost, int amount = 1)
    {
        if (GameStatsManager.Instance != null && GameStatsManager.Instance.GetCoins() >= cost)
        {
            GameStatsManager.Instance.SpendCoins(cost);
            AddLives(amount);
            CoinDisplay.Instance?.UpdateCoinsUI();
            return true;
        }

        Debug.Log("Yeterli coin yok!");
        return false;
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}
