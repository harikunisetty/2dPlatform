using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Basic : MonoBehaviour
{
    public static UI_Basic Instance;

    [SerializeField] Text killCountTxt;
    [SerializeField] Text coinsCountTxt;
    [SerializeField] Image pHealthFill;

    private void Awake()
    {
        if (Instance != null)
            DestroyImmediate(gameObject);
        else
            Instance = this;

        killCountTxt.text = "Kill: 00";
        coinsCountTxt.text = "CoinsCount: 00";
    }

    public void KillCountUI()
    {
        killCountTxt.text = "KillCount: " + GameManager.Instance.KillCount.ToString();
    }

    public void coinsCountUI()
    {
        coinsCountTxt.text = "CoinsCount: " + GameManager.Instance.Coins.ToString();
    }

    public void PlayerHealthUI(float value)
    {
        pHealthFill.fillAmount = value * 0.01f;
    }
}
