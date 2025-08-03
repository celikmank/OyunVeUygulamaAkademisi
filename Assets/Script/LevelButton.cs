using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; // Bu butonun temsil etti�i seviye
    public string sceneName; // Bu butona t�klan�nca y�klenecek sahne
    public Button button;
    public GameObject lockIcon; // Kilit g�rseli
    public GameObject[] starIcons; // 3 y�ld�z objesi
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
