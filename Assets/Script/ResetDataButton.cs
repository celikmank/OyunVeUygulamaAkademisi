using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDataButton : MonoBehaviour
{
    [Header("Kaç seviye varsa buraya gir")]
    public int totalLevelCount = 10;

    public void ResetPlayerPrefsData()
    {
        // Seviye yýldýzlarý ve açýlma durumlarý sýfýrlanýr
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

        // Örnek ek veri silme (ekleyebilirsin):
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.DeleteKey("MusicMuted"); // Ayar gibi

        PlayerPrefs.Save();

        Debug.Log("Tüm veriler baþarýyla sýfýrlandý.");

        // Aktif sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
