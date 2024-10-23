using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMovement : MonoBehaviour
{

    // 在 Inspector 中分配需要下落的按钮
    public RectTransform[] buttons; 
    // 初始高度，按钮将从目标位置上方的这个高度开始下落
    public float initialHeight = 500f; 
    // 重力加速度，负值表示向下加速
    public float gravity = -9.8f; 
    // 每个按钮下落之间的延迟时间
    public float delayBetweenButtons = 0.5f; 

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform button = buttons[i];
            // 记录目标位置
            Vector2 targetPosition = button.anchoredPosition;
            // 设置按钮初始位置在目标位置上方 initialHeight 处
            button.anchoredPosition = new Vector2(targetPosition.x, targetPosition.y + initialHeight);
            // 启动下落协程，并根据索引设置延迟，实现顺序下落
            StartCoroutine(Fall(button, targetPosition, i * delayBetweenButtons));
        }
    }

    IEnumerator Fall(RectTransform button, Vector2 targetPosition, float delay)
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Vector2 startPosition = button.anchoredPosition;

        while (button.anchoredPosition.y > targetPosition.y)
        {
            elapsedTime += Time.deltaTime;
            // 根据自由落体公式计算位移
            float displacement = 0.5f * gravity * elapsedTime * elapsedTime;
            button.anchoredPosition = new Vector2(startPosition.x, startPosition.y + displacement);

            // 当按钮到达或超过目标位置时，结束循环
            if (button.anchoredPosition.y <= targetPosition.y)
            {
                button.anchoredPosition = targetPosition;
                break;
            }

            yield return null;
        }
    }
}
