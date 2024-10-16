using UnityEngine;
using UnityEngine.UI;

public class UICanvasSwitcher : MonoBehaviour
{
    public Canvas currentCanvas; // 当前显示的 Canvas
    public Canvas newCanvas; // 要切换到的新的 Canvas
    public Button switchButton; // 切换的按钮
    public Button StartGameButtion;//开始游戏按钮

    void Start()
    {
        // 为按钮添加点击事件
        switchButton.onClick.AddListener(SwitchCanvas);
        // 确保新 Canvas 初始时是隐藏的
        newCanvas.gameObject.SetActive(false);
        StartGameButtion.onClick.AddListener(StartGame);
    }

    void SwitchCanvas()
    {
        // 隐藏当前的 Canvas
        currentCanvas.gameObject.SetActive(false);
        // 显示新的 Canvas
        newCanvas.gameObject.SetActive(true);
    }
    void StartGame()//加载地图
    {
        SceneController.Instance.TransitionToDestination();
    }
}
