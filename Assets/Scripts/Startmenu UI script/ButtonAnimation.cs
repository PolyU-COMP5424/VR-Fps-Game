using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonAnimation : MonoBehaviour
{
    public float initialSpeed = 0f; // 初始速度
    public float acceleration = 700f; // 加速度
    public float tempPosition = -800; // 初始位置
    private Vector3 targetPosition; // 目标位置

    void Start()
    {
        // 设置目标位置为按钮的初始位置（场景内）
        targetPosition = transform.localPosition;
        transform.localPosition = new Vector3(tempPosition, transform.localPosition.y, transform.localPosition.z); // 场景外的位置

        StartCoroutine(MoveButton());
    }

    private IEnumerator MoveButton()
    {
        float currentSpeed = initialSpeed; // 当前速度

        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            // 增加速度
            currentSpeed += acceleration * Time.deltaTime;

            // 移动按钮
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, currentSpeed * Time.deltaTime);
            yield return null; // 等待下一帧
        }

        // 确保按钮最终位置准确
        transform.localPosition = targetPosition;
    }
}
