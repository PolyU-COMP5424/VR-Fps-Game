using System.Collections;
using UnityEngine;
using Valve.VR;

public class 步枪武器 : MonoBehaviour
{
    public int maxBullets = 300;      // 最大子弹数量
    public int currentBullets = 30;    // 当前子弹数量（初始化为30）
    public int maxBulletsInClip = 30;  // 默认弹夹容量
    public int currentBulletsInClip = 30;  // 当前弹夹内子弹数量（初始化为30）
    public float fireCooldown = 0.2f;  // 开火冷却时间
    private float lastFireTime;        // 上次发射时间

    public Transform gunMuzzle;        // 枪口位置，用于发射射线
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public GameObject Special_effects_of_bullet_hitting; //子弹击中特效
    public GameObject muzzle_flash; //枪口火焰特效

    private bool isSkillActive = false; // 技能是否激活
    private float skillDuration = 10f;  // 技能持续时间
    private float skillCooldown = 20f; // 技能冷却时间
    private float skillStartTime;      // 技能开始时间
    private float skillCooldownStartTime; // 技能冷却开始时间

    // 提升射速和最大子弹数量的倍数
    public float fireRateMultiplier = 0.5f;
    public int additionalBulletsInClip = 15;  // 技能激活时增加的弹夹子弹数量

    public SteamVR_Action_Boolean FireSpeedUp;

    void Start()
    {
        lastFireTime = -fireCooldown; // 确保可以立即发射
    }

    void Update()
    {
        // 按下E键激活技能
        if (FireSpeedUp.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (!isSkillActive && Time.time - skillCooldownStartTime >= skillCooldown) // 检查冷却时间
            {
                ActivateSkill();
            }
        }

        // 如果技能激活，检查是否超时结束技能
        if (isSkillActive && Time.time - skillStartTime >= skillDuration)
        {
            DeactivateSkill(); // 技能持续时间到，自动结束
        }
    }

    // 激活技能
    public void ActivateSkill()
    {
        isSkillActive = true;
        skillStartTime = Time.time; // 记录技能开始时间
        skillCooldownStartTime = Time.time; // 记录技能冷却开始时间

        maxBulletsInClip += additionalBulletsInClip; // 增加最大子弹数量
        fireCooldown *= fireRateMultiplier;    // 提升射速

        Debug.Log("技能激活，射速和最大子弹数量提升！");
    }

    // 取消技能效果
    private void DeactivateSkill()
    {
        isSkillActive = false;

        maxBulletsInClip -= additionalBulletsInClip; // 恢复原最大子弹数量
        fireCooldown /= fireRateMultiplier; // 恢复原射速

        Debug.Log("技能结束，恢复正常状态！");
    }

    // 发射方法，供外部调用
    public void Fire()
    {
        // 检测子弹数量
        if (currentBulletsInClip <= 0)
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
        currentBulletsInClip--; // 减少弹夹内子弹数量

        // 从枪口发射射线
        Ray ray = new Ray(gunMuzzle.position, gunMuzzle.forward);
        GameObject gun = Instantiate(muzzle_flash, gunMuzzle.transform.position, Quaternion.identity); // 创建火焰效果
        Destroy(gun, 0.9f);
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

        // 使用技能激活后的最大弹夹容量来进行换弹
        currentBulletsInClip = Mathf.Min(maxBulletsInClip, currentBullets); // 确保换弹时不会超过库存子弹数量
        currentBullets -= currentBulletsInClip; // 减少库存子弹

        Debug.Log("换弹成功，当前弹夹子弹: " + currentBulletsInClip + "，剩余库存: " + currentBullets);
    }
}
