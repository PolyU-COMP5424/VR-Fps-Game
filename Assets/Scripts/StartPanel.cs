using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始页面，开始游戏
/// </summary>
public class StartPanel : MonoBehaviour
{
    [SerializeField]private Button m_StartBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        m_StartBtn.onClick.AddListener(OnStartClick);
    }

    private void OnStartClick()
    {
        gameObject.SetActive(false);
        Player_.GameStart = true;
    }

}
