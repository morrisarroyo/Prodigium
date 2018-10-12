using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour, BaseCreature
{
    public const string tag = "Dragon";
    const string isWalkingStr = "IsWalking";
    const string isFlyingStr = "IsFlying";
    const string basicAttackStr = "BasicAttack";
    const string flameAttackStr = "flameAttack";
    const float basicAttackMaxDistance = 1f;
    const float flameAttackMaxDistance = 1f;

    public int health;
    public int basicAttackDamage;
    public float flyingSpeed = 2f;
    public float movementSpeed = 6f;
    public float rotationSpeed = 3f;
    public float climbHeight = 5f;

    bool isAttacking = false;
    List<IDragonAction> actions;
    IDragonAction currentAction;
    GameObject player;
    Rigidbody rb;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        player = GetPlayerToTrack();
        actions = new List<IDragonAction>() { gameObject.GetComponent<BasicAttack>(), gameObject.GetComponent<FlameAttack>() };
        currentAction = actions[1];
    }

    // Update is called once per frame
    void Update()
    {
        Do();
        //Climb();
    }

    public void Do()
    {
        if (actions[1] is IDragonAttack)
        {
            if (CanAttack())
            {
                Rotate();
                Attack();
            }
        }


        else
        {
            isAttacking = false;
            Move();
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
    public void BasicAttack()
    {
    }

    private bool CanAttack()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 10f)
        {
            Debug.Log(distance);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Attack()
    {
        if (CanAttack())
        {
            if (!isAttacking)
            {
                isAttacking = true;
                TriggerAttackAnimation();
            }
        }
    }

    private void TriggerAttackAnimation()
    {
        if (isAttacking)
            anim.SetTrigger(currentAction.Name);
    }

    public void Move()
    {
        if (player != null)
        {
            // The step size is equal to speed times frame time.
            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Move our position a step closer to the target.
            if (!transform.position.Equals(player.transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementStep);

                StartMovementAnimation();

                Vector3 targetDir = player.transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

            }
            else
            {
                StopMovementAnimation();
            }
        }
    }

    public void Climb()
    {
        float flyingStep = flyingSpeed * Time.deltaTime;
        if (transform.position.y < climbHeight)
        {
            Vector3 movement = transform.position;
            movement.y = climbHeight;
            transform.position = Vector3.MoveTowards(transform.position, movement, flyingStep);
        }
    }

    public void FlyAway()
    {
        if (player != null)
        {
            // The step size is equal to speed times frame time.
            float flyingStep = flyingSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed * Time.deltaTime;

            // Move our position a step closer to the target.
            if (Vector3.Distance(transform.position, player.transform.position) < 10f)
            {
                transform.LookAt(player.transform);
                transform.Rotate(0, 180, 0);
                StartFlyingAnimation();
                transform.Translate(Vector3.forward * flyingStep);

                Vector3 targetDir = player.transform.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);

            }
            else
            {

                StopFlyingAnimation();
            }
        }
    }

    private void StartFlyingAnimation()
    {
        anim.SetBool(isFlyingStr, true);
    }

    private void StopFlyingAnimation()
    {
        anim.SetBool(isFlyingStr, false);
    }

    private void StartMovementAnimation()
    {
        anim.SetBool(isWalkingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isWalkingStr, false);
    }


    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    private GameObject GetPlayerToTrack()
    {
        GameObject player = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        return player;
    }
}
