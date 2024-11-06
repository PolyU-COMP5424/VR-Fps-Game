using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth = 0f;
    public float giveDamage = 5f;


    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Guarding var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingpointRadius = 4f;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie Animation")]
    public Animator anim;

    [Header("Zombie mood/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    private void Awake()
    {
        presentHealth = zombieHealth;
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);
        if (!playerInvisionRadius && !playerInattackingRadius) Guard();
        if (playerInvisionRadius && !playerInattackingRadius) Pursueplayer();
        if (playerInvisionRadius && playerInattackingRadius) AttackingPlayer();
        
    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingpointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);

            if (currentZombiePosition >= walkPoints.Length)
            {
                
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        //change zombie facing
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }

    private void Pursueplayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
        }
    }

    private void AttackingPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(AttackingRaycastArea.transform.position,AttackingRaycastArea.transform.forward,out hitInfo,attackingRadius))

            {
                Debug.Log("Attacking"+hitInfo.transform.name);
             
                PlayerMovement playerBody= hitInfo.transform.GetComponent<PlayerMovement>();

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }
                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Attacking", true);
                anim.SetBool("Died", false);
            }
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack= false;
    }

    public void zombieHitDamage(float amount)
    {
        presentHealth -=amount;

        if(presentHealth < 95f)
        {
            visionRadius = 100f;
        }
    }

}
