using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject camera;

    public int hp = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void underacctick(int d) 
    { 
        hp-=d;
        if (hp < 0){
            camera.SetActive(true);
            Destroy(gameObject);
        };
        Debug.Log(hp);
    }
}
