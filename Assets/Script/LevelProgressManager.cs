using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
   public int totalRequiredDrops = 2;
    private int correctDrops = 0;

    public LevelEndManager levelEndManager; // LevelEndManager referansý
    public float maxAllowedTime = 6f; // max time in seconds to complete level
    private float elapsedTime = 0f;
    private bool levelFinished = false;
    private void Update()
    {
        if (levelFinished) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > maxAllowedTime)
        {
            levelFinished = true;
            levelEndManager.ShowLevelFailed(); // Time is up, player failed
        }
    }

    public void RegisterCorrectDrop()
    {
        if (levelFinished) return;

        correctDrops++;

        if (correctDrops >= totalRequiredDrops)
        {
            levelFinished = true;

            if (elapsedTime <= maxAllowedTime)
            {
                levelEndManager.ShowLevelComplete(elapsedTime);
            }
            else
            {
                levelEndManager.ShowLevelFailed();
            }
        }
    }
}
