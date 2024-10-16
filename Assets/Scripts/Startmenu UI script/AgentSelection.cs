using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcherWithAcceleration : MonoBehaviour
{
    public GameObject[] images; // 四张图片
    public Button[] buttons; // 四个按钮
    public Text displayText; // 用于显示的文本框
    public string[] texts; // 每个按钮对应的文本内容
    public Vector3 offScreenPosition = new Vector3(1000, 0, 0); // 场景外的初始位置
    public Vector3 onScreenPosition = new Vector3(0, 0, 0); // 场景内的目标位置
    public float moveDuration = 1.5f; // 移动的时间
    private int currentImageIndex = -1; // 当前显示的图片索引
    private bool isMoving = false; // 标记是否在移动

    void Start()
    {
        // 初始化图片到场景外
        foreach (GameObject image in images)
        {
            image.transform.localPosition = offScreenPosition;
        }

        // 初始化文本框内容为空
        displayText.text = "";

        // 为每个按钮添加点击事件
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 需要捕获当前的索引
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    void OnButtonClick(int index)
    {
        if (isMoving || index == currentImageIndex) return; // 如果正在移动或点击的是当前图片，则不做任何处理

        // 如果有其他图片正在显示，先让其回到场景外
        if (currentImageIndex != -1)
        {
            StartCoroutine(MoveImageOut(images[currentImageIndex]));
        }

        // 更新文本框内容
        displayText.text = texts[index];

        // 显示新图片
        currentImageIndex = index;
        StartCoroutine(MoveImageIn(images[currentImageIndex]));
    }

    private IEnumerator MoveImageIn(GameObject image)
    {
        isMoving = true; // 标记正在移动
        float elapsedTime = 0f;
        Vector3 startPos = offScreenPosition;
        Vector3 endPos = onScreenPosition;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            // 模拟加速效果，使用 t² 来表示非线性加速
            float smoothStep = Mathf.SmoothStep(0f, 1f, t);
            image.transform.localPosition = Vector3.Lerp(startPos, endPos, smoothStep);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保图片到达目标位置
        image.transform.localPosition = endPos;
        isMoving = false; // 移动完成
    }

    private IEnumerator MoveImageOut(GameObject image)
    {
        isMoving = true; // 标记正在移动
        float elapsedTime = 0f;
        Vector3 startPos = onScreenPosition;
        Vector3 endPos = offScreenPosition;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            // 模拟加速效果，使用 t² 来表示非线性加速
            float smoothStep = Mathf.SmoothStep(0f, 1f, t);
            image.transform.localPosition = Vector3.Lerp(startPos, endPos, smoothStep);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保图片到达目标位置
        image.transform.localPosition = endPos;
        isMoving = false; // 移动完成
    }
}
