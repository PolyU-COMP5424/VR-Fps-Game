using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMenu : MonoBehaviour
{
    public Text displayText; // 显示传递过来的参数的文本框
    public Canvas secondCanvas; // 当前的 Canvas
    public Canvas thirdCanvas; // 第三个 Canvas
    public Button switchButtonCreat; // 切换 Canvas 的按钮
    public Button switchButtonJoin; // 切换 Canvas 的按钮
    public InputField inputField1; // 第一个文本输入框
    public InputField inputField2; // 第二个文本输入框
    public int agent;
    void Start()
    {
        // 为按钮添加点击事件监听
        switchButtonCreat.onClick.AddListener(SwitchToThirdCanvas);
        switchButtonJoin.onClick.AddListener(SwitchToThirdCanvas);
    }

    public void ReceiveParameter(int parameter)
    {
        // 显示传递过来的参数
        string str;
        switch(parameter)
        {
            case 0:
                str = "你选择的特工是Jett";
                agent = 0;
                break;
            case 1:
                str = "你选择的特工是Iso";
                agent = 1;
                break;
            case 2:
                str = "你选择的特工是Chamber";
                agent = 2;
                break;
            case 3:
                str = "你选择的特工是Sage";
                agent = 3;
                break;
            default:
                str = "";
                break;
        }
        displayText.text = str;
    }


    public void SwitchToThirdCanvas()
    {
        // 获取文本输入框的内容
        string input1 = inputField1.text;
        string input2 = inputField2.text;

        // 获取第三个 Canvas 的控制器
        ThirdCanvasController thirdController = thirdCanvas.GetComponent<ThirdCanvasController>();
        if (thirdController != null)
        {
            // 传递参数和输入框的内容
            thirdController.Initialize(agent, input1, input2);
        }

        // 隐藏当前 Canvas，显示第三个 Canvas
        secondCanvas.gameObject.SetActive(false);
        thirdCanvas.gameObject.SetActive(true);
    }
}
