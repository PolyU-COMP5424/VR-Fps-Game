using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 玩家 : MonoBehaviour
{
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
        if (hp < 0) Debug.Log("玩家死亡");
        Debug.Log(hp);
    }
}
