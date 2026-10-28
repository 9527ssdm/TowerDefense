using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text moneyText;// 显示金钱的文本
    public Text healthText;// 显示生命值的文本
    public Text waveText;// 显示波次的文本

    void Update()
    {
        if(GameManager.Instance!= null)
        {
            moneyText.text = "金币: " + GameManager.Instance.playerMoney;
            healthText.text = "生命: " + GameManager.Instance.playerHealth;

            if (WaveManager.Instance != null)
            {
                waveText.text = $"第 {WaveManager.Instance.CurrentWave} / {WaveManager.Instance.TotalWaves} 波";
            }
        }
    }
}
