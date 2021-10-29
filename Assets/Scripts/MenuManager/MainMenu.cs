using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu :Menu<MainMenu>
{
    public void PlayButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadNextLevel();
        }
        GameMenu.Open();
    }

    public void SettingButton()
    {
        SettingsMenu.Open();
    }

    public void CreditButton()
    {
        AboutMenu.Open();
    }
    public override void BackButton()
    {
        Application.Quit();
    }
}