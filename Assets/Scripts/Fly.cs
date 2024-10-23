using System.Collections;
using System.Collections.Generic;
//using Unity.XR.CoreUtils;
using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// 控制飞机的起飞降落
/// </summary>
public class Fly : MonoBehaviour
{
    private Animator m_Animator;
    //private XRSimpleInteractable m_Interactable;
    
    void Awake()
    {
        //m_Interactable = gameObject.GetComponent<XRSimpleInteractable>();
        m_Animator = gameObject.GetComponent<Animator>();
       // m_Interactable.selectEntered.AddListener(OnSelectedEnter);
    }

    //射线点击飞机，开始下落
   // private void OnSelectedEnter(SelectEnterEventArgs arg0)
    //{
    //    m_Animator.Play("Fly");
    //}

    //起飞的动画帧事件，代表游戏胜利
    public void OnFly()
    {
        GameObject.FindWithTag("Player_").GetComponent<Player_>().GameWin();
    }
}
