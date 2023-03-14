using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float attackSpeed;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animation
    public Animator anim;
    public MobBillboardAnimation mobBillboardAnimation;

    public ObjectPooler objPooler;

    private Health health;
    private SFX_Manager sfxManager;

    [SerializeField]
    private Transform firePosA;
    [SerializeField]
    private Transform firePosB;

    private SpriteRenderer sr;
    private void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("attackSpeed", attackSpeed);
        mobBillboardAnimation = GetComponentInChildren<MobBillboardAnimation>();
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
        sfxManager = GetComponent<SFX_Manager>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            anim.SetTrigger("Shoot");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }
    }

    public void FireProjectile()
    {
        sfxManager.PlayFireWeaponSound();
        switch (gameObject.name)
        {
            case string s when s.Contains("Pistol"):
                objPooler.SpawnFromPool("Enemy Pistol Projectile", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                break;
            case string s when s.Contains("Shotgun"):
                int pelletCount = Random.Range(6, 11);
                for (int i = 0; i < pelletCount; i++)
                {
                    objPooler.SpawnFromPool("Enemy Shotgun Projectile", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                }
                break;
            case string s when s.Contains("Minigun"):
                if (!sr.flipX)
                {
                    objPooler.SpawnFromPool("Enemy Minigun Projectile", firePosA.transform.position, Quaternion.identity);
                }
                else if (sr.flipX)
                {
                    objPooler.SpawnFromPool("Enemy Minigun Projectile", firePosB.transform.position, Quaternion.identity);
                }
                
                break;
            case string s when s.Contains("RPG"):
                if (sr.flipX)
                {
                    objPooler.SpawnFromPool("Enemy RPG Projectile", new Vector3(firePosA.position.x, firePosA.position.y, firePosA.position.z + 1), Quaternion.identity);
                    objPooler.SpawnFromPool("Enemy RPG Projectile", new Vector3(firePosB.position.x, firePosB.position.y, firePosB.position.z + 1), Quaternion.identity);
                }
                else if (!sr.flipX)
                {
                    objPooler.SpawnFromPool("Enemy RPG Projectile", new Vector3(firePosA.position.x, firePosA.position.y, firePosA.position.z - 1), Quaternion.identity);
                    objPooler.SpawnFromPool("Enemy RPG Projectile", new Vector3(firePosB.position.x, firePosB.position.y, firePosB.position.z - 1), Quaternion.identity);
                }
                break;
            case string s when s.Contains("BBEG"):
                objPooler.SpawnFromPool("Enemy BBEG Projectile", firePosA.transform.position, Quaternion.identity);
                objPooler.SpawnFromPool("Enemy BBEG Projectile", firePosB.transform.position, Quaternion.identity);
                break;
            case string s when s.Contains("HeadCrab"):

                break;

        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
