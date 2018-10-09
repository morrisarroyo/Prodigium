using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour, BaseCreature
{
    public const string tag = "Dragon";
    const string isWalkingStr = "IsWalking";
    const string isTurningStr = "IsTurning";
    const string basicAttack = "BasicAttack";


    public int health;
    public int basicAttackDamage;
    public float movementSpeed = 6f;
    public float rotationSpeed = 3f;

    public List<string> actions;

    GameObject player;
    Rigidbody rb;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        player = getPlayerToTrack();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //BasicAttack();
    }

    public void BasicAttack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1.0f)
            TriggerBasicAttack();
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


    private void StartMovementAnimation()
    {
        anim.SetBool(isWalkingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isWalkingStr, false);
    }

    private void TriggerBasicAttack()
    {
        anim.SetTrigger(basicAttack);
    }
    /*
    private void StartTurningAnimation()
    {
        anim.SetBool(isTurningStr, true);
    }

    private void StopTurningAnimation()
    {
        anim.SetBool(isTurningStr, false);
    }
    */



    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    private GameObject getPlayerToTrack()
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
