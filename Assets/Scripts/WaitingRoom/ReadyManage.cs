using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyManage : MonoBehaviour
{
    // 准备按钮数组
    public RectTransform[] readyButtons; // 四个准备按钮的 RectTransform
    // 开始游戏按钮
    public Button startButton;
    // 添加玩家按钮
    public Button addPlayerButton;

    // 按钮初始高度（不可见的高处）
    public float initialHeight = 500f;
    // 重力加速度
    public float gravity = -9.8f;
    // 下落之间的延迟
    public float delayBetweenFalls = 0.5f;

    // 当前已添加的玩家数
    private int currentPlayerCount = 0;
    // 每个准备按钮的准备状态
    private bool[] isPlayerReady;

    // 记录每个按钮的目标位置
    private Vector2[] targetPositions;

    void Start()
    {
        // 初始化
        isPlayerReady = new bool[readyButtons.Length];
        targetPositions = new Vector2[readyButtons.Length];

        // 将所有准备按钮移动到初始高度，并禁用其交互
        for (int i = 0; i < readyButtons.Length; i++)
        {
            RectTransform btn = readyButtons[i];
            // 记录按钮的目标位置（即在场景中设置的位置）
            targetPositions[i] = btn.anchoredPosition;
            // 将按钮移到高处
            btn.anchoredPosition = new Vector2(btn.anchoredPosition.x, btn.anchoredPosition.y + initialHeight);
            btn.GetComponent<Button>().interactable = false;
            btn.gameObject.SetActive(false);
        }

        // 禁用开始按钮
        startButton.interactable = false;

        // 添加玩家按钮点击事件
        addPlayerButton.onClick.AddListener(OnAddPlayerButtonClicked);
    }

    void OnAddPlayerButtonClicked()
    {
        if (currentPlayerCount < readyButtons.Length)
        {
            // 激活下一个准备按钮
            RectTransform readyButton = readyButtons[currentPlayerCount];
            readyButton.gameObject.SetActive(true);
            StartCoroutine(Fall(readyButton, currentPlayerCount));
            currentPlayerCount++;

            // 如果已经达到最大玩家数，禁用添加玩家按钮
            if (currentPlayerCount == readyButtons.Length)
            {
                addPlayerButton.interactable = false;
            }
        }
    }

    IEnumerator Fall(RectTransform button, int playerIndex)
    {
        // 等待一定的延迟
        yield return new WaitForSeconds(playerIndex * delayBetweenFalls);

        float elapsedTime = 0f;
        Vector2 startPosition = button.anchoredPosition;
        Vector2 targetPosition = targetPositions[playerIndex];

        // 在下落过程中禁用按钮的交互
        Button btnComponent = button.GetComponent<Button>();
        btnComponent.interactable = false;

        // 下落动画
        while (button.anchoredPosition.y > targetPosition.y)
        {
            elapsedTime += Time.deltaTime;
            float displacement = 0.5f * gravity * elapsedTime * elapsedTime;
            button.anchoredPosition = new Vector2(startPosition.x, startPosition.y + displacement);

            if (button.anchoredPosition.y <= targetPosition.y)
            {
                button.anchoredPosition = targetPosition;
                break;
            }
            yield return null;
        }

        // 下落完成后，启用按钮交互
        btnComponent.interactable = true;

        // 添加按钮点击事件
        btnComponent.onClick.AddListener(() => OnReadyButtonClicked(playerIndex));
    }

    void OnReadyButtonClicked(int playerIndex)
    {
        isPlayerReady[playerIndex] = !isPlayerReady[playerIndex];

        // 只需修改按钮的可用性，无需修改文本
        Button btnComponent = readyButtons[playerIndex].GetComponent<Button>();
        // 这里可以根据需要调整按钮的外观，例如改变颜色
        if (isPlayerReady[playerIndex])
        {
            btnComponent.image.color = Color.green;
        }
        else
        {
            btnComponent.image.color = Color.white;
        }

        // 检查所有已下落的按钮是否都已准备
        CheckAllPlayersReady();
    }

    void CheckAllPlayersReady()
    {
        bool allReady = true;
        for (int i = 0; i < currentPlayerCount; i++)
        {
            if (!isPlayerReady[i])
            {
                allReady = false;
                break;
            }
        }

        // 根据所有玩家是否已准备，设置开始按钮的可交互性
        startButton.interactable = allReady;
    }
}
