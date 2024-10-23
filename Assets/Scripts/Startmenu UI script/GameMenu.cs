using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdCanvasController : MonoBehaviour
{
    public GameObject[] images; // 存放四张图片的数组
    public Vector3 targetPosition = new Vector3(0, 0, 0); // 所有图片的目标位置
    public Text displayText1; // 用于显示第一个文本输入框的内容
    public Text displayText2; // 用于显示第二个文本输入框的内容
    public Text displayParameter; // 用于显示从第一个 Canvas 传来的参数

    // 初始化方法，接收从第二个 Canvas 传递过来的数据
    public void Initialize(int parameter, string input1, string input2)
    {
        GameManager.Instance.agentID = parameter;
        switch (GameManager.Instance.agentID)
        {
            case 0:
                displayParameter.text = "所选特工：Jett";
                break;
            case 1:
                displayParameter.text = "所选特工：Iso";
                break;
            case 2:
                displayParameter.text = "所选特工：Chamber";
                break;
            case 3:
                displayParameter.text = "所选特工：Sage";
                break;
        }
        displayText1.text = "昵称: " + input1;
        displayText2.text = "房间号: " + input2;

        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        // 激活指定索引的图片，并设置到目标位置
        images[GameManager.Instance.agentID].SetActive(true);
        images[GameManager.Instance.agentID].transform.localPosition = targetPosition;
    }
}

