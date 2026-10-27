using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int PlayerHealth = 100;

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
}
