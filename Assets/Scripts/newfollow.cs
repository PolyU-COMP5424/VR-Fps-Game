using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public NavMeshAgent nav;       // 敌人的NavMeshAgent，用于路径规划
    public Transform target;       // 玩家目标
    public float attackRadius = 2.0f;  // 攻击范围
    public Animator anim;          // 动画控制器
    public float timeBtwAttack = 1.5f; // 攻击间隔
    public int attackDamage = 10;  // 攻击伤害
    private bool previouslyAttack = false; // 是否已攻击过
    public LayerMask playerLayer;  // 玩家图层，用于检测是否在攻击范围内

    private void Start()
    {
        // 如果目标未赋值，自动查找带有"Player"标签的物体
        if (target == null)
        {
            target = GameObject.FindWithTag("Player")?.transform;  // 用“Player”标签找到胶囊体或未来的玩家
        }

        // 输出错误信息，如果没有找到目标
        if (target == null)
        {
            Debug.LogError("Target not assigned and no object with 'Player' tag found.");
        }

        // 获取 NavMeshAgent 和 Animator 组件
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null && nav != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            // 如果玩家在攻击范围外，继续追踪玩家
            if (distanceToPlayer > attackRadius)
            {
                nav.SetDestination(target.position);
                anim.SetBool("attack", false);  // 停止攻击动画
            }
            // 玩家在攻击范围内，停止移动并攻击
            else if (distanceToPlayer <= attackRadius)
            {
                nav.SetDestination(transform.position); // 停止移动
                anim.SetBool("attack", true);  // 播放攻击动画

                // 进行攻击
                if (!previouslyAttack)
                {
                    AttackPlayer();
                    previouslyAttack = true;
                    Invoke(nameof(ResetAttack), timeBtwAttack);  // 攻击冷却
                }
            }
        }
    }

    // 执行攻击逻辑
    void AttackPlayer()
    {
        // 检查玩家是否在攻击范围内
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);

        foreach (var hitPlayer in hitPlayers)
        {
            // 简单的玩家伤害处理逻辑
            EnemyDamage playerHealth = hitPlayer.GetComponent<EnemyDamage>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);  // 造成伤害
                Debug.Log("Enemy attacked the player, causing damage.");
            }
        }
    }

    // 重置攻击状态
    void ResetAttack()
    {
        previouslyAttack = false;
    }

    // 在场景中可视化攻击范围（调试用）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);  // 可视化攻击范围
    }
}
