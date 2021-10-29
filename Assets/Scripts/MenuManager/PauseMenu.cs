using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu :Menu<PauseMenu>
{
    [SerializeField] int mainMenuBuildIndex = 0;
    public void Resume()
    {
        Time.timeScale = 1;
        base.BackButton();
    }
    public void Restart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        base.BackButton();
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(mainMenuBuildIndex);

        MainMenu.Open();
    }
    public override void BackButton()
    {
        Application.Quit();
    }
}
