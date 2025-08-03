using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelStartManager : MonoBehaviour
{
    public GameObject infoCanvas;
    public TextMeshProUGUI timerText;
    public GameObject gameplayRoot;

    public float infoDisplayTime = 3f;
    public float totalLevelTime = 35f;

    private float gameTimer = 0f;
    private bool gameStarted = false;

    private void Start()
    {
        if (infoCanvas == null || gameplayRoot == null || timerText == null)
        {
            Debug.LogError("LevelStartManager: Bir veya daha fazla referans atanmamýþ!");
            return;
        }
        infoCanvas.SetActive(true);
        gameplayRoot.SetActive(false);
        timerText.text = "";
        BonusVisibilityController.UpdateAllBonusVisibility();

        StartCoroutine(StartGameAfterDelay());
    }

    private System.Collections.IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(infoDisplayTime);
        infoCanvas.SetActive(false);
        gameplayRoot.SetActive(true);
        gameStarted = true;
        gameTimer = 0f;
    }

    private void Update()
    {
        if (gameStarted)
        {
            gameTimer += Time.deltaTime;
            UpdateTimerUI();

            if (gameTimer >= totalLevelTime)
            {
                gameStarted = false;
                // Oyun süresi doldu, buraya oyun bitti mantýðý ekle
                Debug.Log("Süre doldu!");
            }
        }
    }

    public void UpdateTimerUI()
    {
        float remaining = Mathf.Max(0f, totalLevelTime - gameTimer);
        timerText.text = remaining.ToString("F1") + "s";
    }

    public float GetGameTime()
    {
        return gameTimer;
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public void StopTimer()
    {
        gameStarted = false;
    }

    public void AddBonusTime(float bonusSeconds)
    {
        totalLevelTime += bonusSeconds;
    }
}
