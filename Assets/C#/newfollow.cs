using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public NavMeshAgent nav;       // ���˵�NavMeshAgent������·���滮
    public Transform target;       // ���Ŀ��
    public float attackRadius = 2.0f;  // ������Χ
    public Animator anim;          // ����������
    public float timeBtwAttack = 1.5f; // �������
    public int attackDamage = 10;  // �����˺�
    private bool previouslyAttack = false; // �Ƿ��ѹ�����
    public LayerMask playerLayer;  // ���ͼ�㣬���ڼ���Ƿ��ڹ�����Χ��

    private void Start()
    {
        // ���Ŀ��δ��ֵ���Զ����Ҵ���"Player"��ǩ������
        if (target == null)
        {
            target = GameObject.FindWithTag("Player")?.transform;  // �á�Player����ǩ�ҵ��������δ�������
        }

        // ���������Ϣ�����û���ҵ�Ŀ��
        if (target == null)
        {
            Debug.LogError("Target not assigned and no object with 'Player' tag found.");
        }

        // ��ȡ NavMeshAgent �� Animator ���
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null && nav != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            // �������ڹ�����Χ�⣬����׷�����
            if (distanceToPlayer > attackRadius)
            {
                nav.SetDestination(target.position);
                anim.SetBool("attack", false);  // ֹͣ��������
            }
            // ����ڹ�����Χ�ڣ�ֹͣ�ƶ�������
            else if (distanceToPlayer <= attackRadius)
            {
                nav.SetDestination(transform.position); // ֹͣ�ƶ�
                anim.SetBool("attack", true);  // ���Ź�������

                // ���й���
                if (!previouslyAttack)
                {
                    AttackPlayer();
                    previouslyAttack = true;
                    Invoke(nameof(ResetAttack), timeBtwAttack);  // ������ȴ
                }
            }
        }
    }

    // ִ�й����߼�
    void AttackPlayer()
    {
        // �������Ƿ��ڹ�����Χ��
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);

        foreach (var hitPlayer in hitPlayers)
        {
            // �򵥵�����˺������߼�
            EnemyDamage playerHealth = hitPlayer.GetComponent<EnemyDamage>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);  // ����˺�
                Debug.Log("Enemy attacked the player, causing damage.");
            }
        }
    }

    // ���ù���״̬
    void ResetAttack()
    {
        previouslyAttack = false;
    }

    // �ڳ����п��ӻ�������Χ�������ã�
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);  // ���ӻ�������Χ
    }
}
