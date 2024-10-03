using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 单例实例
    public static GameManager Instance;

    // 定义游戏状态枚举
    public enum GameState
    {
        Menu,      // 菜单
        Playing,   // 游戏进行中
        Paused,    // 暂停
        GameOver   // 游戏结束
    }

    // 当前游戏状态
    public GameState CurrentState { get; private set; }

    // 初始化
    void Awake()
    {
        // 检查是否已有实例存在
        if (Instance == null)
        {
            Debug.Log("GameManager Instance Creating");

            Instance = this;
            // 保持在场景切换时不销毁
            DontDestroyOnLoad(gameObject);
            // 初始化游戏状态为菜单
            ChangeState(GameState.Menu);
        }
        else
        {
            // 如果已有实例，销毁新创建的对象
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 根据当前状态执行不同的逻辑
        switch (CurrentState)
        {
            case GameState.Playing:
                // 游戏进行中的逻辑
                break;
            case GameState.Paused:
                // 暂停中的逻辑
                break;
            case GameState.Menu:
                // 菜单中的逻辑
                break;
            case GameState.GameOver:
                // 游戏结束的逻辑
                break;
        }
    }

    // 按名字载入场景
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 改变游戏状态的方法
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("GameState Changed: " + newState);

        switch (newState)
        {
            case GameState.Menu:
                // 进入菜单状态时的逻辑
                Time.timeScale = 1f; // 确保游戏时间流逝正常
                break;
            case GameState.Playing:
                // 进入游戏进行中状态时的逻辑
                Time.timeScale = 1f; // 确保游戏时间流逝正常
                break;
            case GameState.Paused:
                // 进入暂停状态时的逻辑
                Time.timeScale = 0f; // 暂停游戏时间
                break;
            case GameState.GameOver:
                // 进入游戏结束状态时的逻辑
                Time.timeScale = 0f; // 暂停游戏时间
                break;
        }
    }

    // 退出游戏的方法
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        #if UNITY_EDITOR
        // 在编辑器中停止播放
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 在构建的游戏中退出
        Application.Quit();
        #endif
    }
}