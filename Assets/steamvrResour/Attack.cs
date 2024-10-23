using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using static Valve.VR.InteractionSystem.Hand;

public class Attack : Throwable
{ 
    public SteamVR_Action_Boolean �񶪰�����;
    public SteamVR_Action_Boolean ���𰴼���;
    public SteamVR_Action_Boolean ��װ������;
    public ��ǹ���� gun;
  
    public Animator animator;
    private void Start()
    {
        
    }
    protected override void HandAttachedUpdate(Hand hand)
    {
        //base.HandAttachedUpdate(hand);
      
        if (�񶪰�����.GetStateDown(SteamVR_Input_Sources.Any))
        {
            hand.DetachObject(this.gameObject, true);
        }
        if(���𰴼���.GetState(SteamVR_Input_Sources.Any))
        {
           
            gun.Fire();
        }
        if(��װ������.GetStateDown(SteamVR_Input_Sources.Any))
        {
            animator.SetTrigger("HuanDan");
            gun.Reload();
        }
    }

}
