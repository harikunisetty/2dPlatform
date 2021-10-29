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
    
    [Header("LevelIndex")]
    [SerializeField] int nextLevelIndex;
    [SerializeField] string nextLevelName;
    [SerializeField] LoadLevelIndex levelObjective;
    [SerializeField] Player_MoveMent playerController;
    [SerializeField] GameObject player;
     void Awake()
    {
        if (Instance != null)
            DestroyImmediate(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(Instance);
    }
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            playerController = player.GetComponent<Player_MoveMent>();

        levelObjective = Object.FindObjectOfType<LoadLevelIndex>();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }
        if (levelObjective != null && levelObjective.IsObjectiveCompleted)
        {
            LevelEnded();
        }
    }
    void LevelEnded()
    {
        if (playerController != null)
        {

            playerController.enabled = false;

            player.GetComponent<Rigidbody2D>().MovePosition(Vector3.zero);

            LoadNextLevel();
        }
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {

        nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;

        LoadNextLevel(nextLevelIndex);
    }
    public void LoadNextLevel(int index)
    {
        if (index > 0 && index <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
    }

    public void LoadNextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
