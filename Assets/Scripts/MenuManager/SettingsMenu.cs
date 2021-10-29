using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu :Menu<SettingsMenu>
{
    [Header("Volume")]

    [SerializeField] Slider volumeSlider;

    [Header("Vibrations")]

    [SerializeField] bool vibrations;

    [SerializeField] Text vibrationsText;

    [Header("Data")]

    [SerializeField] DataManager dataManager;

    protected override void Awake()
    {
        base.Awake();

        dataManager = Object.FindObjectOfType<DataManager>();

        LoadData();
        Debug.Log(Application.persistentDataPath);

    }
    public void volumeController(float Volume)
    {
        //PlayerPrefs.SetFloat("Volume", VolumeValue);

        if (dataManager == null)
            return;

        dataManager.Volume = Volume;
        Debug.Log("volume" + Volume);
    }
    public void Vibrations()
    {
        if (dataManager == null)
            return;

        vibrations = !vibrations;
        dataManager.Vibrations = vibrations;


        if (vibrations)
            vibrationsText.text = "ON";
        else
            vibrationsText.text = "OFF";

    }
    public override void BackButton()
    {
        base.BackButton();

        if (dataManager != null)
            dataManager.Save();
    }
    private void LoadData()
    {
        if (dataManager == null || volumeSlider == null || vibrationsText == null)
            return;

        dataManager.Load();
        volumeSlider.value = dataManager.Volume;
        if (dataManager.Vibrations)
            vibrationsText.text = "ON";
        else
            vibrationsText.text = "OFF";
        /*vibrationsText.text = dataManager.Vibrations.ToString();*/
    }
}
