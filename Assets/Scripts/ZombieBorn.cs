using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 僵尸出生点控制
/// </summary>
public class ZombieBorn : MonoBehaviour
{
    [SerializeField] private GameObject m_Zombie;
    [SerializeField] private float m_BornRange = 10;
    [SerializeField] private float m_BornInterval = 1;

    private GameObject m_MainPlayer;

    private float startTime;

    void Start()
    {
        m_MainPlayer = GameObject.FindWithTag("Player_");
    }

    void Update()
    {
        if (!Player_.GameStart)
        {
            return;
        }
        //和主角距离判断，然后小于10，创建僵尸
        if (Vector3.Distance(m_MainPlayer.transform.position, transform.position) < m_BornRange)
        {
            if (Time.time - startTime>m_BornInterval)
            {
                startTime = Time.time;
                GenerateZombie();
            }
        }
    }

    void GenerateZombie()
    {
       var zombie= GameObject.Instantiate(m_Zombie);
       var position = transform.position;
       position.x += Random.Range(-2f,2f);
       position.z += Random.Range(-2f,2f);
       zombie.transform.position = position;
       //创建僵尸，设置随机移动速度
       zombie.GetComponent<NavMeshAgent>().speed = Random.Range(1f, 2f);
    }
}