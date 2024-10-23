using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 敌人AI : MonoBehaviour
{
    public float detectionRange = 10f; // 检测玩家的范围
    public float wanderRange = 5f; // 随机移动范围
    public float attackRange = 2f; // 攻击范围
    public float chaseRange = 10f; // 追击范围
    public LayerMask playerLayer; // 玩家图层
    public Animator animator; // 动画组件
    public float moveSpeed = 2f; // 移动速度
    public 玩家 PLayer;

    private Transform player;
    private Vector3 targetPosition;
    private bool isWandering = true;

    void Start()
    {
        StartCoroutine(Wander()); // 开始随机移动的协程
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            ChasePlayer();
        }
        else if (isWandering)
        {
            MoveToTargetPosition();
        }
    }

    void DetectPlayer()
    {
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);

        if (playersInRange.Length > 0)
        {
            // 找到第一个玩家
            player = playersInRange[0].transform;
            isWandering = false; // 停止随机移动
        }
    }

    void ChasePlayer()
    {
        // 计算与玩家的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // 播放攻击动画
            animator.SetBool("attack", true);
            isWandering = false; // 停止移动
        }
        else
        {
            animator.SetBool("attack", false);
            MoveTowards(player.position); // 追击玩家

            // 如果与玩家的距离超过追击范围，停止追击并开始随机移动
            if (distanceToPlayer > chaseRange)
            {
                player = null; // 重置玩家引用
                isWandering = true; // 开始随机移动
                StartCoroutine(Wander());
            }
        }
    }

    void MoveTowards(Vector3 target)
    {
        // 计算移动方向
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 旋转朝向目标
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }

    IEnumerator Wander()
    {
        while (isWandering) // 只有在没有玩家的情况下才随机移动
        {
            targetPosition = Random.insideUnitSphere * wanderRange + transform.position;
            targetPosition.y = transform.position.y; // 保持y轴不变

            // 等待到达目的地
            while (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                MoveTowards(targetPosition);
                yield return null; // 等待一帧
            }

            yield return new WaitForSeconds(2f); // 等待一段时间后再随机移动
        }
    }

    void MoveToTargetPosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            MoveTowards(targetPosition);
        }
    }
    public void attack()
    {
        PLayer.underacctick(10);
    }
}
