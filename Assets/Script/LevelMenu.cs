using UnityEngine;

public class LevelMenu : MonoBehaviour
{
  public string gameSceneName = "Level1Game"; // Oyun sahnesinin ad�


    public void StartGame()
    {
        // Oyun sahnesine ge�i� yap
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
}
