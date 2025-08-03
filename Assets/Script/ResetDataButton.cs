using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDataButton : MonoBehaviour
{
    [Header("Ka� seviye varsa buraya gir")]
    public int totalLevelCount = 10;

    public void ResetPlayerPrefsData()
    {
        // Seviye y�ld�zlar� ve a��lma durumlar� s�f�rlan�r
        for (int i = 1; i <= totalLevelCount; i++)
        {
            PlayerPrefs.DeleteKey("LevelStars_" + i);
            PlayerPrefs.DeleteKey("LevelUnlocked_" + i);
        }

        // Oyuncu istatistikleri
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("PlayerLives");

        // Bonuslar
        PlayerPrefs.DeleteKey("highlightbonus");
        PlayerPrefs.DeleteKey("timebonus");
        PlayerPrefs.DeleteKey("giftbonus");

        // �rnek ek veri silme (ekleyebilirsin):
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.DeleteKey("MusicMuted"); // Ayar gibi

        PlayerPrefs.Save();

        Debug.Log("T�m veriler ba�ar�yla s�f�rland�.");

        // Aktif sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
