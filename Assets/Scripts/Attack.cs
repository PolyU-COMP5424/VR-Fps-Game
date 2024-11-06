using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using static Valve.VR.InteractionSystem.Hand;

public class Attack : Throwable
{ 
    public SteamVR_Action_Boolean 捡丢按键绑定;
    public SteamVR_Action_Boolean 开火按键绑定;
    public SteamVR_Action_Boolean 重装按键绑定;
    public 步枪武器 gun;
  
    public Animator animator;
    private void Start()
    {
        
    }
    protected override void HandAttachedUpdate(Hand hand)
    {
        //base.HandAttachedUpdate(hand);
      
        if (捡丢按键绑定.GetStateDown(SteamVR_Input_Sources.Any))
        {
            hand.DetachObject(this.gameObject, true);
        }
        if(开火按键绑定.GetState(SteamVR_Input_Sources.Any))
        {
           
            gun.Fire();
        }
        if(重装按键绑定.GetStateDown(SteamVR_Input_Sources.Any))
        {
            animator.SetTrigger("HuanDan");
            gun.Reload();
        }
    }

}
