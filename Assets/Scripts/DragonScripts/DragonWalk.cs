using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWalk : MonoBehaviour, IDragonAction
{
    Animator anim;
    GameObject player;
    bool walkTowards = true;


    const string isWalkingStr = "IsWalking";
    public float movementSpeed = 6f;
    public float rotationSpeed = 3f;
    public Dragon dragon;


    public string Name
    {
        get
        {
            return "Move";
        }
    }

    public bool IsDoing
    {
        get
        {
            return IsDoing;
        }

        set
        {
            IsDoing = value;
        }
    }

    public bool CanDo()
    {
        return true;
    }

    // Use this for initialization
    void Start()
    {
        dragon = GetComponent<Dragon>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = dragon.player;
    }

    public void Do()
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

    public bool IsDone()
    {
        throw new System.NotImplementedException();
    }

    private void StartMovementAnimation()
    {
        anim.SetBool(isWalkingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isWalkingStr, false);
    }


}
