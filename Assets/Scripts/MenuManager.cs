using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;
    public GameObject SettingsPanel;
    public GameObject ShopPanel;

    public void PlayGame()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void OpenShop()
    {
        MainMenuPanel.SetActive(false);
        ShopPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        LevelSelectPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ShopPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
