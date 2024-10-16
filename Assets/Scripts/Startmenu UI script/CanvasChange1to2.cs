using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public Canvas currentCanvas;
    public Canvas newCanvas;
    public Button[] buttons; // 四个按钮
    public Button switchButton; // 切换 Canvas 的按钮
    private int selectedParameter; // 存储选择的参数

    void Start()
    {
        newCanvas.gameObject.SetActive(false); // 初始时隐藏新 Canvas

        // 为四个按钮添加点击事件
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 记录当前按钮的索引
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
        }

        // 为切换 Canvas 的按钮添加点击事件
        switchButton.onClick.AddListener(SwitchCanvas);
    }

    void OnButtonPressed(int index)
    {
        selectedParameter = index; // 记录当前按下按钮的索引作为参数
        Debug.Log("Selected parameter: " + selectedParameter);
        // 在这里可以实现图片的平移效果...
    }

    void SwitchCanvas()
    {
        currentCanvas.gameObject.SetActive(false); // 隐藏当前 Canvas
        newCanvas.gameObject.SetActive(true); // 显示新 Canvas

        // 将参数传递给新的 Canvas
        NewMenu newMenu = newCanvas.GetComponent<NewMenu>();
        if (newMenu != null)
        {
            newMenu.ReceiveParameter(selectedParameter);
        }
    }
}
