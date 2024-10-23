using UnityEngine;

public class GunSystem : MonoBehaviour
{
    // 武器属性
    public int damage; // 武器伤害
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots; // 射击间隔、散射、射程、重装时间、每次射击间隔
    public int bulletsLeft, magazineSize, bulletsPerTap, currentAmmo, maxAmmoSize; // 剩余子弹、弹匣容量、每次发射的子弹数、当前弹药、最大弹药容量
    public bool allowButtonHold; // 是否允许按住射击
    int bulletsShot; // 当前射击的子弹数量

    // 布尔值 
    bool shooting, readyToShoot, reloading; // 射击状态、是否准备好射击、是否正在重装

    // 参考变量
    public Transform attackPoint; // 攻击点
    public RaycastHit rayHit; // 射线碰撞信息
    public LayerMask whatIsEnemy; // 敌人的层

    // 图形效果
    public GameObject muzzleFlash, bulletHoleGraphic; // 火焰效果和子弹孔图形

    private void Awake()
    {
        readyToShoot = true; // 初始化为准备好射击
    }

    private void Update()
    {
        MyInput(); // 处理输入

        // 更新弹药数量文本（如果有需要的话，可以添加 UI 组件）
        // text.SetText("Ammo: " + bulletsLeft + " / " + magazineSize); // 更新弹药数量文本
    }

    private void MyInput()
    {
        // 根据设置确定射击输入
        if (allowButtonHold) shooting = Input.GetButton("Fire1"); // 按住射击
        else shooting = Input.GetButtonDown("Fire1"); // 点击射击

        // 重装输入
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload(); // 按R键重装

        // 射击
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) // 如果准备好射击并且满足条件
        {
            bulletsShot = bulletsPerTap; // 设置当前射击的子弹数量
            Shoot_(); // 执行射击
        }
    }

    public void 发射()
    {
        // 射击
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) // 如果准备好射击并且满足条件
        {
            bulletsShot = bulletsPerTap; // 设置当前射击的子弹数量
            Shoot_(); // 执行射击
        }
    }
    public void 装弹()
    {
        if (bulletsLeft < magazineSize && !reloading) Reload(); // 按R键重装
    }
    private void Shoot_()
    {
        readyToShoot = false; // 设置为不准备射击

        // 计算散射
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // 计算带有散射的方向
        Vector3 direction = attackPoint.forward + new Vector3(x, y, 0); // 使用攻击点的前向方向加上散射

        // 射线检测
        if (Physics.Raycast(attackPoint.position, direction, out rayHit, range, whatIsEnemy)) // 从攻击点位置发射射线
        {
            Debug.Log(rayHit.collider.name); // 打印碰撞体名称

            // 如果碰撞体是敌人，则造成伤害
            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<EnemyDamage>().TakeDamage(damage); // 对敌人造成伤害

            Zombie1 zombie1 = rayHit.collider.GetComponent<Zombie1>(); // 获取碰撞体上的 Zombie1 组件
            if (zombie1 != null) // 检查是否是 Zombie1
            {
                zombie1.zombieHitDamage(damage); // 对僵尸造成伤害
            }
        }
        if(rayHit.point==Vector3.zero)rayHit.point = new Vector3(999,999,999);
        // 图形效果
        GameObject impact = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0)); // 创建子弹孔图形
        GameObject muzzle = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity); // 创建火焰效果
        Destroy(impact, 1f); // 1秒后销毁子弹孔
        Destroy(muzzle, 1f); // 1秒后销毁火焰效果

        bulletsLeft--; // 减少剩余子弹
        bulletsShot--; // 减少当前射击的子弹数量

        Invoke("ResetShot", timeBetweenShooting); // 延迟调用重置射击状态的方法

        if (bulletsShot > 0 && bulletsLeft > 0) // 如果还有子弹待射击且剩余子弹大于0
            Invoke("Shoot", timeBetweenShots); // 延迟再次射击
    }

    private void ResetShot()
    {
        readyToShoot = true; // 重置射击状态为准备好
    }

    private void Reload()
    {
        reloading = true; // 设置为正在重装
        Invoke("ReloadFinished", reloadTime); // 延迟调用重装完成的方法
    }

    private void ReloadFinished()
    {
        // 重装逻辑
        bulletsLeft = magazineSize; // 将剩余子弹数设置为弹匣容量
        reloading = false; // 重装完成
    }

    public void AddAmmo(int x)
    {
        // 添加弹药逻辑
        // currentAmmo += x; // 这里可以添加具体实现
    }
}
