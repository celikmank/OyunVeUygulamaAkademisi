using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    public int levelIndex;
    public string sceneName;
    public Button levelButton;
    public GameObject lockIcon;
    public GameObject[] starIcons;
    public TextMeshProUGUI levelNumberText;

    private void Start()
    {
        bool isUnlocked = StarManager.IsLevelUnlocked(levelIndex);

        levelButton.interactable = isUnlocked;
        lockIcon.SetActive(!isUnlocked);
        levelNumberText.text = levelIndex.ToString();

        int starCount = isUnlocked ? StarManager.GetStars(levelIndex) : 0;
        UpdateStars(starCount);

        levelButton.onClick.AddListener(OnLevelButtonClick);
    }

    private void UpdateStars(int starCount)
    {
        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(i < starCount);
        }
    }

    public void OnLevelButtonClick()
    {
        if (StarManager.IsLevelUnlocked(levelIndex))
        {
            if (LivesManager.Instance.GetCurrentLives() > 0)
            {
                LivesManager.Instance.UseLife();
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Yeterli can yok!");
                // UI uyarý paneli gösterilebilir.
            }
        }
    }
}
