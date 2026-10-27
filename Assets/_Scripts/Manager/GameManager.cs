using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int PlayerHealth = 100;
    public int playerMoney = 100;

    

    private void Awake()
    {
        Instance= this;
    }

    public void PlayerTakeDamage(int damage)
    {
        PlayerHealth -= damage;
        PlayerHealth= Mathf.Max(PlayerHealth, 0); // 确保血量不会低于 0
        Debug.Log("玩家血量: " + PlayerHealth);

        if(PlayerHealth <= 0)
        {
            Debug.Log("GameOve");
        }
    }

    public bool SendMoney(int amount)
    {
        if (playerMoney < amount)
        {
            return false;
        }
        playerMoney -= amount;
        return true;
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
    }
}
