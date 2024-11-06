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
                // 随机选择一个点生成敌人
                GameObject e = Instantiate(enemies.gameObject, points[Random.Range(0, points.Count)].transform.position, Quaternion.identity);
                e.transform.SetParent(enemiesclone.transform);

                // 增加当前敌人数量
                currentEnemyCount++;
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