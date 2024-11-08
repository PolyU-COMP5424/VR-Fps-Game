using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject camera;
    public Healthbar healthbar;

    public int maxHp = 100;
    public int currentHp;
    void Start()
    {
        currentHp = maxHp;
        healthbar.SetMaxHealth(maxHp);
    }

    // Update is called once per frame
    void Update()
    {

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
}
