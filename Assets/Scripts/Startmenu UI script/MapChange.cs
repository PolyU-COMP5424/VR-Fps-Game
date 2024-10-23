using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapChange : MonoBehaviour
{
    public GameObject[] images; // 存放三个图片的数组
    public Button previousButton; // “上一个”按钮
    public Button nextButton; // “下一个”按钮
    public Text displayText; // 用于显示当前图片对应的文本框
    public string[] imageDescriptions; // 存储每张图片对应的文本内容
    // public int currentIndex = GameManager.Instance.mapID; 当前图片的索引

    void Start()
    {
        // 确保所有图片都隐藏，然后只显示第一个图片
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        images[GameManager.Instance.mapID].SetActive(true);

        // 初始化文本框内容
        if (imageDescriptions.Length > 0 && GameManager.Instance.mapID < imageDescriptions.Length)
        {
            displayText.text = imageDescriptions[GameManager.Instance.mapID];
        }

        // 为按钮添加点击事件
        previousButton.onClick.AddListener(ShowPreviousImage);
        nextButton.onClick.AddListener(ShowNextImage);
    }

    // 显示前一个图片
    void ShowPreviousImage()
    {
        // 隐藏当前图片
        images[GameManager.Instance.mapID].SetActive(false);

        // 计算前一个图片的索引
        GameManager.Instance.mapID--;
        if (GameManager.Instance.mapID < 0)
        {
            GameManager.Instance.mapID = images.Length - 1; // 如果当前是第一个，则循环到最后一个
        }

        // 显示新的当前图片
        images[GameManager.Instance.mapID].SetActive(true);

        // 更新文本框内容
        if (GameManager.Instance.mapID < imageDescriptions.Length)
        {
            displayText.text = imageDescriptions[GameManager.Instance.mapID];
        }
    }

    // 显示下一个图片
    void ShowNextImage()
    {
        // 隐藏当前图片
        images[GameManager.Instance.mapID].SetActive(false);

        // 计算下一个图片的索引
        GameManager.Instance.mapID++;
        if (GameManager.Instance.mapID >= images.Length)
        {
            GameManager.Instance.mapID = 0; // 如果当前是最后一个，则循环到第一个
        }

        // 显示新的当前图片
        images[GameManager.Instance.mapID].SetActive(true);

        // 更新文本框内容
        if (GameManager.Instance.mapID < imageDescriptions.Length)
        {
            displayText.text = imageDescriptions[GameManager.Instance.mapID];
        }
    }
}
