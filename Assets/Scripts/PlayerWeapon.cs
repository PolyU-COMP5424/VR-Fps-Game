using System;
using System.Collections;
using System.Collections.Generic;
using MFPS;
using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// 枪的逻辑
/// </summary>
public class PlayerWeapon : MonoBehaviour
{
    private XRInputEvent m_InputEvent;
    public Transform m_FirePoint;
    public GameObject m_BloodEffect;
    public GameObject m_FireEffect;
    public AudioSource m_FireAudio;

    private bool m_Fire;
    private float m_StartTime;
    [SerializeField]private float m_FireInterval = 0.1f;

    void Start()
    {
        //监听trigger键点击
        m_InputEvent = gameObject.GetComponentInParent<XRInputEvent>();
        m_InputEvent.OnTriggerButton.AddListener(OnTriggerButton);
    }

    private void OnTriggerButton(bool click)
    {
        m_Fire = click;
    }

    private void Update()
    {
        if (Player_.GameStart)
        {
            //射击逻辑，没interval秒一次射击
            if (m_Fire)
            {
                if (Time.time - m_StartTime >= m_FireInterval)
                {
                    m_StartTime = Time.time;
                    Fire();
                }
            }
        }
    }

    /// <summary>
    /// 射击逻辑
    /// </summary>
    private void Fire()
    {
        m_FireAudio.Play();
        if (Physics.Raycast(m_FirePoint.position, m_FirePoint.forward, out RaycastHit hit))
        {
            //攻击僵尸和僵尸头部伤害不同
            var effect = m_FireEffect;
            if (hit.collider.CompareTag("Zombie"))
            {
                effect = m_BloodEffect;
                var zombie = hit.collider.GetComponent<Zombie>();
                zombie.OnDamage(50);
            }
            else if (hit.collider.CompareTag("ZombieHead"))
            {
                effect = m_BloodEffect;

                var zombie = hit.collider.GetComponentInParent<Zombie>();
                zombie.OnDamage(100);
            }

            //播放设计的特效
            var go = GameObject.Instantiate(effect);
            go.transform.position = hit.point;
            Destroy(go, 2f);
        }
    }
}