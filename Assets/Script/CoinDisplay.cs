using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Inspector üzerinden atanacak
    public static CoinDisplay Instance { get; private set; }

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

        Debug.Log("CoinDisplay Awake");
    }

    private void OnEnable()
    {
        if (GameStatsManager.Instance != null)
            GameStatsManager.Instance.OnStatsChanged += UpdateCoinsUI;

        UpdateCoinsUI();
    }

    private void OnDisable()
    {
        if (GameStatsManager.Instance != null)
            GameStatsManager.Instance.OnStatsChanged -= UpdateCoinsUI;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI()
    {
        if (coinText != null && GameStatsManager.Instance != null)
        {
            int coins = GameStatsManager.Instance.GetCoins();
            coinText.text = coins.ToString();
        }
    }
}
