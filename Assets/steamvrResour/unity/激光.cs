using UnityEngine;

public class 激光 : MonoBehaviour
{
    public float lineDuration = 1f; // 射线显示的时间
    public float laserDistance = 100f; // 激光的最大长度
    private LineRenderer lineRenderer;

    void Start()
    {
        // 创建 LineRenderer 组件
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.005f; // 设置线段宽度
        lineRenderer.endWidth = 0.001f; // 设置线段宽度
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 设置材质
        lineRenderer.startColor = Color.red; // 线段起始颜色
        lineRenderer.endColor = Color.red; // 线段结束颜色
    }

    void Update()
    {
        // 更新 LineRenderer 的位置
        DrawRay();
    }

    void DrawRay()
    {
        // 设置 LineRenderer 的点数量
        lineRenderer.positionCount = 2;

        // 设置起始点为物体本身的位置
        Vector3 startPoint = transform.position;

        // 计算终点为物体朝前方向的一个点
        Vector3 endPoint = startPoint + transform.forward * laserDistance;

        // 设置起始点和终点
        lineRenderer.SetPosition(0, startPoint); // 物体本身的位置
        lineRenderer.SetPosition(1, endPoint); // 射线终点

        // 可选：自动消失效果
        //StartCoroutine(FadeLine());
    }

    System.Collections.IEnumerator FadeLine()
    {
        // 等待一段时间后移除线条
        yield return new WaitForSeconds(lineDuration);
        lineRenderer.positionCount = 0; // 清除 LineRenderer
    }
}
