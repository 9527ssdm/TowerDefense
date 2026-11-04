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
    public int playerMoney = 50;

    private bool isAccelerated = false; // 是否加速
    // 游戏是否已结束
    public bool isGameOver = false;

    public bool isPause=false;
    public GameObject pauseMenuUI;
    // 拖入 UI Text，显示结果
    public Text resultText;

    public AudioSource sfxSource;

    

    private void Awake()
    {
        // 如果已经有一个实例存在，并且不是当前对象，则销毁当前对象
        Instance = this;
        sfxSource = GetComponent<AudioSource>();   // 自动拿身上的 AudioSource
    }

    private void Update()
    {

        if (!isGameOver && !isPause)
        {
            if (isAccelerated)
            {
                Time.timeScale = 2f; // 加速游戏
            }
            else
            {
                Time.timeScale = 1f; // 正常速度
            }
        }

        // Esc 退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        //按R键重新开始游戏
        if (Input.GetKeyDown(KeyCode.R)&&isGameOver)
        {
            RestartGame();
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

        if(Input.GetKeyDown(KeyCode.P)&&!isGameOver)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0f; // 暂停游戏
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(true); // 显示暂停菜单
            }
        }
        else
        {
            Time.timeScale = 1f; // 恢复游戏
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false); // 隐藏暂停菜单
            }
        }
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1f;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
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

   public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // 编辑器里停止运行，方便测试
#else
    Application.Quit();                                 // 打包后真正退出
#endif
    }
}
