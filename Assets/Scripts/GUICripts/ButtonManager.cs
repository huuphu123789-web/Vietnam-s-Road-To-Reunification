using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScnene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {

        SceneManager.LoadScene("MainMenuScene");
        AudioManager.instance.StopPlaySceneClip();
        AudioManager.instance.StopSettingClip();
        Time.timeScale = 1;
    }
    public void History()
    {
        SceneManager.LoadScene("HistoryScene");
    }
    public void PauseGame()
    {
        menuPanel.SetActive(true);
        PlayerController.instance.isTopAttacking = false;
        Time.timeScale = 0;
    }

    public void ContinousGame()
    {
        menuPanel.SetActive(false);
        PlayController.instance.isShooting = false;
        Time.timeScale = 1;
        PlayController.instance.isShooting = true;

    }

    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
        AudioManager.instance.StopBackgroundMusic();
        AudioManager.instance.PlaySettingClip();

    }

    public void BackToSetting()
    {
        settingPanel.SetActive(false);
    }

    public void ToSetting()
    {
        settingPanel.SetActive(true);
        if (Time.timeScale == 0)
        {
            PlayController.instance.isShooting = true;
        }
    }
}
