using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    private int killCount;
    public int KillCount { get => killCount; }
    public int Coins { get => coins; }
    private int coins;

    public static GameManager Instance;


    void Awake()
    {
        if (Instance != null)
            DestroyImmediate(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(Instance);
    }

    public void UpdateKillCount()
    {
        killCount++;

        UI_Basic.Instance.KillCountUI();
    }

    public void UpdateCoins()
    {
        coins++;

        UI_Basic.Instance.coinsCountUI();
    }

    public void LoadNextScene(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
