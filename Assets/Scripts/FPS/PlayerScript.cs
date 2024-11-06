using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player_ Movement")]
    public float playerSpeed = 1.9f;

    [Header("Player_ Script Camera")]
    public Transform playerCamera;

    [Header("Player_ Health Thing")]
    public float playerHealth = 120f;
    public float presentHealth;


    [Header("player Animator and Gravity")]
    public CharacterController cC;

    [Header("Player_ Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;

    private void Start()
    {
        presentHealth = playerHealth;
    }
    private void Update()
    {
        playerMove();
    }
    void playerMove()
    {
        float horizontal_axis=Input.GetAxisRaw("Vertical");
        float vertical_axis = Input.GetAxisRaw("Horizontal");
        Vector3 direction= new Vector3(horizontal_axis,0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle=Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation=Quaternion.Euler(0f,targetAngle,0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(direction.normalized*playerSpeed*Time.deltaTime);
        }
    }

    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        if(presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject,1.0f);
    }
        
}
