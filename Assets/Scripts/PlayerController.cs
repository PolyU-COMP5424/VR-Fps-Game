using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;  // 输入控制方向
    public float speed = 1;                // 移动速度
    public float gravity = 9.81f;          // 重力大小
    public float fallSpeed = 10f;          // 下落速度
    private CharacterController characterController;

    private Vector3 velocity;             // 角色的速度（包括重力）

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 获取输入轴
        Vector3 inputDirection = new Vector3(input.axis.x, 0, input.axis.y);

        // 获取摄像头方向（水平平面上，保持y轴方向不变）
        Vector3 forward = Player.instance.hmdTransform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Player.instance.hmdTransform.right;
        right.y = 0;
        right.Normalize();

        // 根据输入轴控制前后和左右的移动方向
        Vector3 moveDirection = forward * inputDirection.y + right * inputDirection.x;

        // 移动角色
        characterController.Move(speed * Time.deltaTime * moveDirection);

        // 处理重力和掉落
        if (characterController.isGrounded)
        {
            // 如果在地面上，重置下落速度
            velocity.y = -gravity * Time.deltaTime;
        }
        else
        {
            // 如果不在地面上，角色受重力影响下落
            velocity.y -= gravity * Time.deltaTime;
        }

        // 应用重力
        characterController.Move(velocity * Time.deltaTime);

        // 射线检测地面高度，确保角色在高低差处自动掉落到地面
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            // 如果检测到地面，调整角色位置
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
