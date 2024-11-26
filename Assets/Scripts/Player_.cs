using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 人物逻辑
/// </summary>
public class Player_ : MonoBehaviour
{
    public int HP = 100;
    public bool m_Alive => HP > 0;

    public TextMeshProUGUI m_HPText;
    public GameObject m_GameOver;
    public GameObject m_GameWin;

    public GameObject m_Locomotion;
    
    public static bool GameStart = false;
    
    /// <summary>
    /// 人物被僵尸抓伤逻辑
    /// </summary>
    /// <param name="damage"></param>
    public void OnDamage(int damage)
    {
        HP = Mathf.Clamp(HP - damage, 0, 100);
        m_HPText.text = "HP: " + HP;

        //死亡后，停止创建僵尸，停止传送
        if (!m_Alive)
        {
            m_GameOver.SetActive(true);
            GameStart = false;
            m_Locomotion.SetActive(false);
        }
    }

    /// <summary>
    /// 游戏胜利
    /// </summary>
    public void GameWin()
    {
        m_GameWin.SetActive(true);
        GameStart = false;
        
        //销毁所有僵尸
        var zombies = GameObject.FindObjectsOfType<Zombie>();
        for (int i = 0; i < zombies.Length; i++)
        {
            Destroy(zombies[i].gameObject);
        }
    }
}