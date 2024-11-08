using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public List<GameObject> points;           // 敌人生成的地点
    public GameObject enemies;                // 敌人 Prefab
    public GameObject enemiesclone;           // 用于保存生成的敌人的父物体

    public float waittime;                    // 生成敌人的间隔时间
    public int maxEnemies;                    // 敌人最大数量
    private int currentEnemyCount = 0;        // 当前场景中的敌人数量

    public GameObject enemies2;               // 第二种敌人 Prefab
    private int enemiesSpawnedCount = 0;      // 已生成的普通敌人数量

    private void Start()
    {
        // 订阅 EnemyDamage 中的 OnEnemyDeath 事件
        EnemyDamage.OnEnemyDeath += HandleEnemyDeath;
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {
        while (true)
        {
            // 每隔一段时间生成敌人
            yield return new WaitForSeconds(waittime);

            // 仅当当前敌人数少于上限时生成敌人
            if (currentEnemyCount < maxEnemies)
            {

                GameObject enemyToSpawn;

                // 每生成 10 个 enemies，就生成 1 个 enemies2
                if (enemiesSpawnedCount % 10 == 0 && enemiesSpawnedCount > 0)
                {
                    enemyToSpawn = enemies2; // 生成 enemies2
                }
                else
                {
                    enemyToSpawn = enemies; // 生成默认的 enemies
                }
                // 随机选择一个点生成敌人
                GameObject e = Instantiate(enemyToSpawn , points[Random.Range(0, points.Count)].transform.position, Quaternion.identity);
                e.transform.SetParent(enemiesclone.transform);

                // 增加当前敌人数量
                currentEnemyCount++;
                enemiesSpawnedCount++;
            }
        }
    }

    // 当敌人死亡时调用，减少敌人数量
    private void HandleEnemyDeath(GameObject enemy)
    {
        currentEnemyCount--;  // 减少敌人计数
    }

    private void OnDestroy()
    {
        // 当脚本对象销毁时取消订阅事件，防止内存泄漏
        EnemyDamage.OnEnemyDeath -= HandleEnemyDeath;
    }
}