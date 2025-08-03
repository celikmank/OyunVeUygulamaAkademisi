using UnityEngine;

public class StarManager : MonoBehaviour
{
    private const string StarsKey = "LevelStars_";
    private const string UnlockedKey = "LevelUnlocked_";

    public static void SetStars(int levelIndex, int stars)
    {
        int currentStars = GetStars(levelIndex);
        if (stars > currentStars)
        {
            PlayerPrefs.SetInt(StarsKey + levelIndex, stars);
        }
        UnlockLevel(levelIndex + 1);
    }

    public static int GetStars(int levelIndex)
    {
        return PlayerPrefs.GetInt(StarsKey + levelIndex, 0);
    }

    public static void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(UnlockedKey + levelIndex, 1);
    }

    public static bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt(UnlockedKey + levelIndex, levelIndex == 1 ? 1 : 0) == 1;
    }
}
