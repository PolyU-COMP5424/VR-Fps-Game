using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float health = 100f;
    public delegate void DeathEventHandler(GameObject enemy);
    public static event DeathEventHandler OnEnemyDeath;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

        void Die()
        {
            if (OnEnemyDeath != null)
            {
                OnEnemyDeath(gameObject);  // ´«µÝµ±Ç°ËÀÍöµÄµÐÈË¶ÔÏó
            }

            // Ïú»ÙµÐÈË¶ÔÏó
            Destroy(gameObject);
        }
    }
}