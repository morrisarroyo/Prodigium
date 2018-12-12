using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : BaseCreature
{
    //public const string tag = "Dragon";
    const string isWalkingStr = "IsWalking";
    const string isFlyingStr = "IsFlying";
    const string basicAttackStr = "BasicAttack";
    const string flameAttackStr = "flameAttack";
    const float basicAttackMaxDistance = 1f;
    const float flameAttackMaxDistance = 1f;

    private Flame flame;
    private CapsuleCollider col;
    public int scoreValue = 10000;
    public ParticleSystem flameParticle;

    //public int health;
    //public int basicAttackDamage;
    //public float flyingSpeed = 2f;
    //public float movementSpeed = 6f;
    public float rotationSpeed = 3f;
    public float wakeDistance = 7f;
    //public float climbHeight = 5f;
    public GameObject player;
    public int healthNum;

    bool isAwake = false;
    bool isAttacking = false;
    bool isAlive = true;
    string previousAnimationName = "";
    List<IDragonAction> actions;

    Rigidbody rb;
    Animator anim;
    NavMeshAgent nav;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        flame = GetComponent<Flame>();
        col = GetComponent<CapsuleCollider>();
        if (player.Equals(null))
        {
            player = GetPlayerToTrack();
        }
        actions = new List<IDragonAction>()
        {
            //gameObject.GetComponent<DragonFly>()

            //gameObject.GetComponent<DragonWalk>()
            //, gameObject.GetComponent<BasicAttack>()
            //, gameObject.GetComponent<DragonRun>()
            //, gameObject.GetComponent<FlameAttack>()
            //*/
        };
        health = healthNum;
    }


    void Start()
    {
        //gameObject.GetComponent<BasicAttack>().Do();
        anim.SetBool("IsWaiting", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (HealthManager.health > 0) { 
            if (isAwake)
            {
                if (health > 0)
                {
                    if (HealthManager.health <= 0)
                    {
                        isAwake = false;
                    }
                    else
                    {
                        Do();
                    }
                }
                else
                {

                    if (isAlive)
                        Die();
                   
                    isAwake = false;
                    anim.SetBool("IsWaiting", true);
                    anim.StopPlayback();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, player.transform.position) < wakeDistance)
                {
                    isAwake = true;
                    anim.SetBool("IsWaiting", false);
                }
            }
        } else
        {
            anim.StopPlayback();
            gameObject.GetComponent<Flame>().Deactivate();
        }
        //Climb();
    }

    public override void Die()
    {
            Destroy(gameObject, 7f);
            anim.SetTrigger("Die");
            nav.isStopped = true;
            var em = flameParticle.emission;
            em.enabled = false;
            GetComponent<EnemyInteractable>().enabled = false;
            col.enabled = false;
            //GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            isAlive = false;
        //transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Weapon"))
        {
            TakeDamage(1000);
        }
    }

    public void Do()
    {
        if (actions.Count == 0)
        {

            actions.Add(gameObject.GetComponent<DragonWalk>());
            actions.Add(gameObject.GetComponent<BasicAttack>());
            actions.Add(gameObject.GetComponent<DragonRun>());
            actions.Add(gameObject.GetComponent<FlameAttack>());
            actions.Add(gameObject.GetComponent<DragonWalk>());
            actions.Add(gameObject.GetComponent<BasicAttack>());
            actions.Add(gameObject.GetComponent<DragonRun>());
            actions.Add(gameObject.GetComponent<FlameAttack>());
            actions.Add(gameObject.GetComponent<DragonWalk>());
            actions.Add(gameObject.GetComponent<BasicAttack>());
            actions.Add(gameObject.GetComponent<DragonRun>());
            actions.Add(gameObject.GetComponent<FlameAttack>());
            //actions.Add(gameObject.GetComponent<DragonFly>());

        }
        if (actions[0] is DragonAttack)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Rotate();

            if (!isAttacking)
            {
                isAttacking = true;
                actions[0].Do();

            }
            if (!actions[0].IsDoing)
            {
                previousAnimationName = actions[0].Name;
                actions.RemoveAt(0);
                isAttacking = false;
            }

        }
        else
        {
            if (!IsPlayingAnimation(previousAnimationName))
            {
                actions[0].Do();
                if (!actions[0].IsDoing)
                {
                    actions.RemoveAt(0);
                }
            }
        }

    }

    public bool IsPlayingAnimation(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void Rotate()
    {
        if (player != null)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            Vector3 targetDir = player.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
    public override void BasicAttack()
    {
    }
    GameObject GetPlayerToTrack()
    {
        GameObject player = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        return player;
    }


    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log(health);
        if (health <= 0) {
            ScoreManager.instance.score += scoreValue;
            Die();
        }
    }
    
}
