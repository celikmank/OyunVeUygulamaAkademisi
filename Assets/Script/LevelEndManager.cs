using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndManager : MonoBehaviour
{

    public GameObject levelCompletePanel;
    public GameObject levelFailedPanel;
    public GameObject[] stars;
    public float threeStarTime = 15f;
    public float twoStarTime = 25f;

    public string nextSceneName;
    public int currentLevelIndex = 1;

    public TMP_Text timeText;
    public TMP_Text coinsText;
    public TMP_Text xpText;

    public AudioClip levelCompleteSound;
    public AudioClip levelFailedSound;
    private AudioSource audioSource;

    private int xpEarned;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        levelCompletePanel.SetActive(false);
        levelFailedPanel.SetActive(false);

        foreach (var star in stars)
        {
            star.SetActive(false);
        }
    }

    public void ShowLevelComplete(float elapsedTime)
    {
        Time.timeScale = 0f;
        levelCompletePanel.SetActive(true);
        levelFailedPanel.SetActive(false);

        if (audioSource != null && levelCompleteSound != null)
            audioSource.PlayOneShot(levelCompleteSound);

        int starCount = CalculateStarRating(elapsedTime);
        ShowStars(starCount);

        StarManager.SetStars(currentLevelIndex, starCount);

        int reward = starCount * 20;
        if (GameStatsManager.Instance != null)
        {
            GameStatsManager.Instance.AddCoins(reward);
            xpEarned = reward * 5;
            GameStatsManager.Instance.AddXP(xpEarned);
        }

        UpdateLevelCompleteUI(elapsedTime, reward, xpEarned);
    }

    private int CalculateStarRating(float elapsedTime)
    {
        float timeBonusMultiplier = 1f;

        if (MarketManager.Instance != null && MarketManager.Instance.GetBonusCount("timebonus") > 0)
        {
            timeBonusMultiplier = 1.5f;
            MarketManager.Instance.UseBonus("timebonus");
        }

        float adjustedThreeStarTime = threeStarTime * timeBonusMultiplier;
        float adjustedTwoStarTime = twoStarTime * timeBonusMultiplier;

        if (elapsedTime <= adjustedThreeStarTime)
            return 3;
        else if (elapsedTime <= adjustedTwoStarTime)
            return 2;
        else
            return 1;
    }

    private void ShowStars(int starCount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < starCount);
        }
    }

    private void UpdateLevelCompleteUI(float elapsedTime, int coins, int xp)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes}:{seconds:00}";
        coinsText.text = $" {coins}";
        xpText.text = $"{xp}";
    }

    public void ShowLevelFailed()
    {
        Time.timeScale = 0f;
        levelCompletePanel.SetActive(false);
        levelFailedPanel.SetActive(true);

        if (audioSource != null && levelFailedSound != null)
            audioSource.PlayOneShot(levelFailedSound);
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    // Yeni eklenen: Bölümü tekrar baþlatýr
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
