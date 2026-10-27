using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 单例模式
    public static GameManager Instance;
    // 玩家血量
    public int PlayerHealth = 100;
    // 玩家金钱
    public int playerMoney = 100;

    

    private void Awake()
    {
        // 如果已经有一个实例存在，并且不是当前对象，则销毁当前对象
        Instance = this;
    }

    public void PlayerTakeDamage(int damage)
    {
        // 玩家受到伤害
        PlayerHealth -= damage;
        // 确保血量不会低于 0
        PlayerHealth = Mathf.Max(PlayerHealth, 0); 
        Debug.Log("玩家血量: " + PlayerHealth);

        if(PlayerHealth <= 0)
        {
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
