using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ����ʵ��
    public static GameManager Instance;

    // ������Ϸ״̬ö��
    public enum GameState
    {
        Menu,      // �˵�
        Playing,   // ��Ϸ������
        Paused,    // ��ͣ
        GameOver   // ��Ϸ����
    }

    public int mapID;
    public int agentID;

    // ��ǰ��Ϸ״̬
    public GameState CurrentState { get; private set; }

    // ��ʼ��
    void Awake()
    {
        // ����Ƿ�����ʵ������
        if (Instance == null)
        {
            Debug.Log("GameManager Instance Creating");

            Instance = this;
            // �����ڳ����л�ʱ������
            DontDestroyOnLoad(gameObject);
            // ��ʼ����Ϸ״̬Ϊ�˵�
            ChangeState(GameState.Menu);
        }
        else
        {
            // �������ʵ���������´����Ķ���
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
        // ���ݵ�ǰ״ִ̬�в�ͬ���߼�
        switch (CurrentState)
        {
            case GameState.Playing:
                // ��Ϸ�����е��߼�
                break;
            case GameState.Paused:
                // ��ͣ�е��߼�
                break;
            case GameState.Menu:
                // �˵��е��߼�
                break;
            case GameState.GameOver:
                // ��Ϸ�������߼�
                break;
        }
    }

    // ���������볡��
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // �ı���Ϸ״̬�ķ���
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("GameState Changed: " + newState);

        switch (newState)
        {
            case GameState.Menu:
                // ����˵�״̬ʱ���߼�
                Time.timeScale = 1f; // ȷ����Ϸʱ����������
                break;
            case GameState.Playing:
                // ������Ϸ������״̬ʱ���߼�
                Time.timeScale = 1f; // ȷ����Ϸʱ����������
                break;
            case GameState.Paused:
                // ������ͣ״̬ʱ���߼�
                Time.timeScale = 0f; // ��ͣ��Ϸʱ��
                break;
            case GameState.GameOver:
                // ������Ϸ����״̬ʱ���߼�
                Time.timeScale = 0f; // ��ͣ��Ϸʱ��
                break;
        }
    }

    // �˳���Ϸ�ķ���
    public void QuitGame()
    {
        Debug.Log("Quit Game");
#if UNITY_EDITOR
        // �ڱ༭����ֹͣ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �ڹ�������Ϸ���˳�
        Application.Quit();
#endif
    }
}