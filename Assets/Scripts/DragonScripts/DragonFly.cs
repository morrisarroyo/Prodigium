using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class DragonFly : MonoBehaviour, IDragonAction
{
    Animator anim;
    Transform target;
    NavMeshAgent nav;
    public float distance = 15f;
    //public Vector3 destination;

    const string isFlyingStr = "IsFlying";
    const string isDescendingStr = "IsDescending";
    public float movementSpeed = 3f;
    public float climbSpeed = 2f;
    //public float rotationSpeed = 3f;
    Dragon dragon;
    public Transform defaultTarget;
    public List<Transform> targets;
    bool isFlying;
    bool isDescending;
    private int randomIndex = 0;
    public string Name
    {
        get
        {
            return "Fly";
        }
    }

    public bool IsDoing
    {
        get;
        set;
    }


    // Use this for initialization
    void Start()
    {
        dragon = gameObject.GetComponent<Dragon>();
        anim = gameObject.GetComponent<Animator>();
        nav = gameObject.GetComponent<NavMeshAgent>();
        target = defaultTarget;
        targets.Add(defaultTarget);
        //destination = transform.position + (Vector3.up * 10);
        IsDoing = false;
        isFlying = false;
        isDescending = false;
        RandomizeTarget();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Set(bool towards, float dist)
    {
        distance = dist;
    }

    public bool CanDo()
    {
        return true;
    }

    public void Do()
    {
        // The step size is equal to speed times frame time.

        // Move our position a step closer to the target.
        if (!IsDone())
        {
            IsDoing = true;
            Fly();
            if (IsDoneClimbing())
            {
                isFlying = false;
                StopClimbingAnimation();
                Descend();
            }
            else
            {

                StartClimbingAnimation();
            }
            nav.enabled = false;
        }
        else
        {
            StopDescendingAnimation();
            StopClimbingAnimation();
            transform.position = target.position;

            nav.enabled = true;
            IsDoing = false;
            isFlying = false;
            isDescending = false;
            RandomizeTarget();
        }
    }

    private void RandomizeTarget()
    {
        int rand = 0;
        do
        {
            rand = UnityEngine.Random.Range(0, targets.Count);
        } while (randomIndex == rand);
        target = targets[rand];
    }

    public bool IsDone()
    {

        if (Math.Abs(transform.position.x - target.position.x) <= 1
            && Math.Abs(transform.position.z - target.position.z) <= 1
            && transform.position.y <= target.position.y)
        {
            return true;
        }
        return false;
    }

    public bool IsDoneClimbing()
    {

        if (transform.position.y >= distance)
        {
            return true;
        }
        return false;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Fly()
    {
        if (isFlying)
        {

            //transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
            //Vector3 destination = transform.position + (Vector3.up * distance);
            //Debug.Log(transform.position);
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
            //nav.isStopped = true;
        }
        else if (isDescending)
        {
            StopClimbingAnimation();
            StartDescendingAnimation();
            //Debug.Log(transform.position);
            transform.Translate(Vector3.down * climbSpeed * Time.deltaTime);
        }
    }


    private void Descend()
    {
        if (!isDescending)
        {
            Vector3 newLocation = target.transform.position;
            newLocation.y += distance;
            transform.position = newLocation;
        }
        isDescending = true;
    }

    private void Climb()
    {
        isFlying = true;
    }
    private void StartClimbingAnimation()
    {
        anim.SetBool(isFlyingStr, true);
    }

    private void StopClimbingAnimation()
    {
        anim.SetBool(isFlyingStr, false);
    }
    private void StartDescendingAnimation()
    {
        if (transform.position.y <= 2)
        {
            anim.SetBool(isDescendingStr, true);
        }
    }

    private void StopDescendingAnimation()
    {
        anim.SetBool(isDescendingStr, false);
    }
}
