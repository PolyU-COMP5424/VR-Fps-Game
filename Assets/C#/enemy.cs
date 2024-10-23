using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public List<GameObject> points;           // �������ɵĵص�
    public GameObject enemies;                // ���� Prefab
    public GameObject enemiesclone;           // ���ڱ������ɵĵ��˵ĸ�����

    public float waittime;                    // ���ɵ��˵ļ��ʱ��
    public int maxEnemies;                    // �����������
    private int currentEnemyCount = 0;        // ��ǰ�����еĵ�������

    private void Start()
    {
        // ���� EnemyDamage �е� OnEnemyDeath �¼�
        EnemyDamage.OnEnemyDeath += HandleEnemyDeath;
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {
        while (true)
        {
            // ÿ��һ��ʱ�����ɵ���
            yield return new WaitForSeconds(waittime);

            // ������ǰ��������������ʱ���ɵ���
            if (currentEnemyCount < maxEnemies)
            {
                // ���ѡ��һ�������ɵ���
                GameObject e = Instantiate(enemies.gameObject, points[Random.Range(0, points.Count)].transform.position, Quaternion.identity);
                e.transform.SetParent(enemiesclone.transform);

                // ���ӵ�ǰ��������
                currentEnemyCount++;
            }
        }
    }

    // ����������ʱ���ã����ٵ�������
    private void HandleEnemyDeath(GameObject enemy)
    {
        currentEnemyCount--;  // ���ٵ��˼���
    }

    private void OnDestroy()
    {
        // ���ű���������ʱȡ�������¼�����ֹ�ڴ�й©
        EnemyDamage.OnEnemyDeath -= HandleEnemyDeath;
    }
}