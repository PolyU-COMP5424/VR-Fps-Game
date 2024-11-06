using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 僵尸逻辑
/// </summary>
public class Zombie : MonoBehaviour
{
    private GameObject m_Target;
    private NavMeshAgent m_Agent;
    private Animator m_Animation;
    public int HP = 100;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int DeadHash = Animator.StringToHash("Dead");

    void Awake()
    {
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        m_Animation = gameObject.GetComponent<Animator>();
        m_Target = GameObject.FindWithTag("Player_");
    }

    /// <summary>
    /// 开始先禁用agent，防止寻路报错
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        m_Agent.enabled = false;
        yield return new WaitForSeconds(1);
        m_Agent.enabled = true;
    }

    void Update()
    {
        if (HP <= 0)
        {
            return;
        }
        
        //僵尸攻击目标的逻辑
        if (m_Agent.enabled)
        {
            m_Agent.SetDestination(m_Target.transform.position);

            //距离人物小于2m，开始攻击
            if (Vector3.Distance(m_Target.transform.position, transform.position) < 2)
            {
                m_Animation.SetBool(AttackHash, true);
            }
            else
            {
                m_Animation.SetBool(AttackHash, false);
            }
        }
    }

    /// <summary>
    /// 被枪射击逻辑
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            if (HP <= 0)
            {
                //死亡后，停止寻路，播放死亡动画，3s后销毁
                m_Agent.Stop();
                m_Agent.enabled = false;
                m_Animation.SetBool(DeadHash, true);
                //2s后自动销毁
                Destroy(gameObject, 3f);
            }
        }
    }
}