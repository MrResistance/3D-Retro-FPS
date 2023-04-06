using IndieMarc.EnemyVision;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;

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
    [SerializeField]
    private GameObject shockwave;
    private SpriteRenderer sr;
    private EnemyVision enemy;
    private void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        mobBillboardAnimation = GetComponentInChildren<MobBillboardAnimation>();
        objPooler = GameObject.FindObjectOfType<ObjectPooler>();
        sfxManager = GetComponent<SFX_Manager>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnEnable()
    {
        anim.SetFloat("attackSpeed", attackSpeed);
    }

    private void Start()
    {
        enemy = GetComponent<EnemyVision>();
        enemy.onDeath += OnDeath;
        enemy.onAlert += OnAlert;
        enemy.onSeeTarget += OnSeen;
        enemy.onDetectTarget += OnDetect;
        enemy.onTouchTarget += OnTouch;
    }

    private void Update()
    {
        if (enemy.GetEnemy().state == EnemyState.Chase && enemy.CanSeeVisionTarget(enemy.seen_character))
        {
            AttackPlayer();
        }
    }
    //Can be either because seen or heard noise
    private void OnAlert(Vector3 target)
    {
    }

    private void OnSeen(VisionTarget target, int distance)
    {
        //Add code for when target get seen and enemy get alerted, 0=touch, 1=near, 2=far, 3=other
    }

    private void OnDetect(VisionTarget target, int distance)
    {
        //Add code for when the enemy detect you as a threat (and start chasing), 0=touch, 1=near, 2=far, 3=other
        if (distance < 3)
        {
            AttackPlayer();
        }
    }

    private void OnTouch(VisionTarget target)
    {
        //Add code for when you get caughts
        AttackPlayer();
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            enemy.GetEnemy().StopMove();
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
        }
    }
    public void Shockwave()
    {
        shockwave.GetComponent<Animator>().Play("Shockwave");
        objPooler.SpawnFromPool("GroundStomp", new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity);
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
