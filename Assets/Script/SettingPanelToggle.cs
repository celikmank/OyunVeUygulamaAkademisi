using UnityEngine;

public class SettingPanelToggle : MonoBehaviour
{
    public GameObject settingsPanel;

    public void TogglePanel()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
    }
}
