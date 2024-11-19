using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform mainCamera;   // 主摄像头
    public Vector3 offset = new Vector3(0.1f, 0.1f, 2f);  // 血量条相对于摄像头的偏移位置
    public Canvas healthBarCanvas;  // 血量条所在的 Canvas

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;  // 自动获取主摄像头
        }
    }

    void Update()
    {
        // 将血量条的位置设置为摄像头位置 + 偏移量
        Vector3 desiredPosition = mainCamera.position + mainCamera.forward * offset.z + mainCamera.up * offset.y + mainCamera.right * offset.x;
        transform.position = desiredPosition;

        // 让血量条始终面朝摄像头
        transform.LookAt(mainCamera.position);
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);

        // 如果Canvas是World Space模式，可以设置为始终面向摄像头
        if (healthBarCanvas.renderMode == RenderMode.WorldSpace)
        {
            // 确保Canvas始终面向摄像头
            healthBarCanvas.transform.rotation = Quaternion.LookRotation(healthBarCanvas.transform.position - mainCamera.position);
        }
    }
}
