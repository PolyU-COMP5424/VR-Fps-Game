using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Move : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveAction;

    public bool isMove=true;
    [SerializeField] private CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAction.axis.magnitude > 0.1&& isMove)
        {
            Vector3 POS1 = Player.instance.hmdTransform.TransformDirection(moveAction.axis.x, 0, moveAction.axis.y);
            characterController.Move(Time.deltaTime*Vector3.ProjectOnPlane(POS1, Vector3.up )-(new Vector3(0,8,0)*Time.deltaTime));
        }

    }
}
