using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private int currentBonusCount = 3;
    [SerializeField] private float bonusSecondsToAdd = 10f;

    public LevelStartManager levelStartManager;  // Inspector�dan atanacak

    public bool UseBonus()
    {
        if (currentBonusCount > 0)
        {
            currentBonusCount--;

            if (levelStartManager != null)
            {
                levelStartManager.AddBonusTime(bonusSecondsToAdd);
                Debug.Log($"{bonusSecondsToAdd} saniye bonus s�re eklendi.");
            }
            else
            {
                Debug.LogWarning("LevelStartManager referans� atanmad�!");
            }

            return true;
        }
        else
        {
            Debug.Log("Bonus kalmad�!");
            return false;
        }
    }

    public int GetBonusCount()
    {
        return currentBonusCount;
    }

    public void AddBonus(int amount)
    {
        currentBonusCount += amount;
        Debug.Log($"Bonus eklendi! Yeni bonus say�s�: {currentBonusCount}");
    }
}
