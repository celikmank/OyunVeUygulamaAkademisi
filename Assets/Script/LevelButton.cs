using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; // Bu butonun temsil ettiði seviye
    public string sceneName; // Bu butona týklanýnca yüklenecek sahne
    public Button button;
    public GameObject lockIcon; // Kilit görseli
    public GameObject[] starIcons; // 3 yýldýz objesi
    public TextMeshProUGUI levelText;

    void Start()
    {
        bool isUnlocked = StarManager.IsLevelUnlocked(levelIndex);
        button.interactable = isUnlocked;
        lockIcon.SetActive(!isUnlocked);

        levelText.text = levelIndex.ToString();

        int starCount = StarManager.GetStars(levelIndex);
        UpdateStars(starCount);

        button.onClick.AddListener(OnLevelButtonClick);
    }

    void UpdateStars(int count)
    {
        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(i < count);
        }
    }

    void OnLevelButtonClick()
    {
        if (StarManager.IsLevelUnlocked(levelIndex))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
