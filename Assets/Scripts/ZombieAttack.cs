using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸的手部碰撞到人，攻击逻辑
/// </summary>
public class ZombieAttack : MonoBehaviour
{
    [SerializeField] 
    private int HitDamage = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_"))
        {
            other.gameObject.GetComponent<Player_>().OnDamage(HitDamage);
        }
    }
}