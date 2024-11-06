using System.Collections;
using UnityEngine;

public class 步枪武器 : MonoBehaviour
{
    public int maxBullets = 300;      // 最大子弹数量
    public int currentBullets = 30;    // 当前子弹数量（初始化为30）
    public float fireCooldown = 0.2f;  // 开火冷却时间
    private float lastFireTime;        // 上次发射时间

    public Transform gunMuzzle;        // 枪口位置，用于发射射线
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public GameObject Special_effects_of_bullet_hitting; //子弹击中特效
    public GameObject muzzle_flash; //枪口火焰特效
    void Start()
    {
        lastFireTime = -fireCooldown; // 确保可以立即发射
       
    }

    // 发射方法，供外部调用
    public void Fire()
    {
        // 检测子弹数量
        if (currentBullets <= 0)
        {
            Debug.Log("没有子弹，无法发射！");
            return; // 如果没有子弹，直接返回
        }

        // 检测冷却时间
        if (Time.time - lastFireTime < fireCooldown)
        {
            Debug.Log("正在冷却，无法发射！");
            return; // 如果未到冷却时间，直接返回
        }

        // 发射子弹
        lastFireTime = Time.time; // 更新上次发射时间
        currentBullets--; // 减少子弹数量

        // 从枪口发射射线
        Ray ray = new Ray(gunMuzzle.position, gunMuzzle.forward);
        GameObject gun = Instantiate(muzzle_flash, gunMuzzle.transform.position, Quaternion.identity); // 创建火焰效果
        Destroy(gun,0.9f);
        RaycastHit hit;
        audioSource.Play();
        if (Physics.Raycast(ray, out hit))
        {
            // 如果击中物体，打印信息
            EnemyDamage ai = hit.collider.gameObject.GetComponent<EnemyDamage>();
            if (ai != null)
                ai.TakeDamage(10);
            GameObject muzzle = Instantiate(Special_effects_of_bullet_hitting, hit.point, Quaternion.identity); // 使用hit.point作为实例化位置
            Destroy(muzzle, 1f); // 1秒后销毁火焰效果
        }
        else
        {
            Debug.Log("没有击中任何物体");
        }
    }

    // 换弹方法，供外部调用
    public void Reload()
    {

        audioSource1.Play();
        currentBullets = 30; // 增加当前子弹数量
        maxBullets -= 30;
        Debug.Log("换弹成功，当前剩余子弹: " + maxBullets);
    }
}
