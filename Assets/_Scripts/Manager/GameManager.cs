using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 单例模式
    public static GameManager Instance;
    // 玩家血量
    public int playerHealth = 100;
    // 玩家金钱
    public int playerMoney = 100;

    private bool isAccelerated = false; // 是否加速
    // 游戏是否已结束
    public bool isGameOver = false;
    // 拖入 UI Text，显示结果
    public Text resultText;           

    private void Awake()
    {
        // 如果已经有一个实例存在，并且不是当前对象，则销毁当前对象
        Instance = this;
    }

    private void Update()
    {
        if (isAccelerated)
        {
            Time.timeScale = 4f; // 加速游戏
        }
        else
        {
            Time.timeScale = 1f; // 正常速度
        }
            //按R键重新开始游戏
        if (Input.GetKeyDown(KeyCode.R)&&isGameOver)
        {
            Time.timeScale = 1f; // 恢复时间流逝
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新加载当前场景
        }

        if(Input.GetKeyDown(KeyCode.S) && !isGameOver)
        {
            if(isAccelerated==false)
            {
                isAccelerated = true;
            }
            else
            {
                isAccelerated = false;
            }
        }
    }

    public void Defeat()
    {
        if(isGameOver)
        {
            return;
        }
        isGameOver = true;

        if(resultText != null)
        {
            resultText.text = "GAME OVER";
            resultText.gameObject.SetActive(true);
        }
        // 冻结游戏
        Time.timeScale = 0f;           
    }

    public void Victory()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;

        if (resultText != null)
        {
            resultText.text = "VICTORY!";
            resultText.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void PlayerTakeDamage(int damage)
    {
        // 玩家受到伤害
        playerHealth -= damage;
        // 确保血量不会低于 0
        playerHealth = Mathf.Max(playerHealth, 0); 
        Debug.Log("玩家血量: " + playerHealth);

        if(playerHealth <= 0)
        {
            Defeat();
            Debug.Log("GameOver");
        }
    }

    public bool SendMoney(int amount)
    {
        // 玩家花费金钱
        if (playerMoney < amount)
        {
            return false;
        }
        // 确保金钱不会低于 0
        playerMoney -= amount;
        return true;
    }

    public void AddMoney(int amount)
    {
        // 玩家获得金钱
        playerMoney += amount;
    }
}
