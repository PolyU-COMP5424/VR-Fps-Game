using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject camera;
    public Healthbar healthbar;

    public int maxHp = 100;
    public int currentHp;

    public float healCooldown = 5f; 
    private bool canHeal = true; 
    public SteamVR_Action_Boolean heal;

    void Start()
    {
        currentHp = maxHp;
        healthbar.SetMaxHealth(maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (heal.GetStateDown(SteamVR_Input_Sources.Any) && canHeal)
        {
            Heal(20); 
        }
    }

    public void underacctick(int d) 
    { 
        currentHp-=d;

        healthbar.SetHealth(currentHp);
        if (currentHp < 0){
            camera.SetActive(true);
            Destroy(gameObject);
        };
        Debug.Log(currentHp);
    }

    private void Heal(int amount)
    {
        currentHp = Mathf.Min(currentHp + amount, maxHp); 
        healthbar.SetHealth(currentHp);
        Debug.Log("Healed: " + amount + " points");

        
        canHeal = false;
        Invoke("ResetHealCooldown", healCooldown);
    }

    private void ResetHealCooldown()
    {
        canHeal = true;
    }
}
