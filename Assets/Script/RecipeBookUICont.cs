using UnityEngine;
using UnityEngine.UI;

public class RecipeBookUICont : MonoBehaviour
{
    public GameObject recipebookPanel;
    public Button openRecipebookButton;
    public Button closeRecipebookButton;

    private void Start()
    {
        recipebookPanel.SetActive(false);

        if (openRecipebookButton != null)
            openRecipebookButton.onClick.AddListener(OpenRecipe);

        if (closeRecipebookButton != null)
            closeRecipebookButton.onClick.AddListener(CloseRecipe);
    }

    public void OpenRecipe()
    {
        recipebookPanel.SetActive(true);
    }

    public void CloseRecipe()
    {
        recipebookPanel.SetActive(false);
    }
}
