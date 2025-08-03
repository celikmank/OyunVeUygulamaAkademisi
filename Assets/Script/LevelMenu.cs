using UnityEngine;

public class LevelMenu : MonoBehaviour
{
  public string gameSceneName = "Level1Game"; // Oyun sahnesinin adý


    public void StartGame()
    {
        // Oyun sahnesine geçiþ yap
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
}
