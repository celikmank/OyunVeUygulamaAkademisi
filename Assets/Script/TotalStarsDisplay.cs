using TMPro;
using UnityEngine;

public class TotalStarsDisplay : MonoBehaviour
{
    public int totalLevels = 10;
    public TextMeshProUGUI starText;

    public void UpdateTotalStars()
    {
        int totalStars = 0;

        for (int i = 1; i <= totalLevels; i++)
        {
            totalStars += StarManager.GetStars(i);
        }

        starText.text = totalStars.ToString();
    }

    private void OnEnable()
    {
        UpdateTotalStars();
    }
}
