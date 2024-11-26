using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
public SteamVR_Action_Vector2 input; // 左Joystick的输入
public float speed = 1; // 移动速度
public float gravity = 9.81f; // 重力大小
private CharacterController characterController;

private Vector3 velocity; // 角色的速度（包括重力）

// Start is called before the first frame update
void Start()
{
characterController = GetComponent<CharacterController>();
}

// Update is called once per frame
void Update()
{
// 获取左Joystick的输入轴
Vector3 inputDirection = new Vector3(input.axis.x, 0, input.axis.y);

// 调试输入值，确保摇杆数据正确
Debug.Log("Left Joystick Input: X = " + input.axis.x + ", Y = " + input.axis.y);

// 如果有输入（避免因浮点数误差导致角色小范围抖动）
if (inputDirection.magnitude > 0.1f)
{
// 获取玩家模型的前方和右方方向
Vector3 forward = transform.forward; // 玩家模型的前方
forward.y = 0; // 忽略垂直分量
forward.Normalize();

Vector3 right = transform.right; // 玩家模型的右方
right.y = 0; // 忽略垂直分量
right.Normalize();

// 计算移动方向
Vector3 moveDirection = forward * inputDirection.z + right * inputDirection.x;

// 调试移动方向
Debug.Log("Move Direction: " + moveDirection);

// 移动角色
characterController.Move(speed * Time.deltaTime * moveDirection);
}

// 处理重力和掉落
if (characterController.isGrounded)
{
velocity.y = -gravity * Time.deltaTime; // 重置下落速度
}
else
{
velocity.y -= gravity * Time.deltaTime; // 受重力影响下落
}

// 应用重力
characterController.Move(velocity * Time.deltaTime);

// 射线检测，确保角色不会穿越地面
RaycastHit hit;
if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
{
transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
}
}
}