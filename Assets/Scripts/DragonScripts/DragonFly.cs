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
    public float movementSpeed = 3f;
    public float climbSpeed = 2f;
    //public float rotationSpeed = 3f;
    Dragon dragon;
    public Transform defaultTarget;
    bool isFlying;

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
        //destination = transform.position + (Vector3.up * 10);
        IsDoing = false;
        isFlying = false;
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
            StartMovementAnimation();
            Fly();
            nav.enabled = false;
        }
        else
        {
            nav.enabled = true;
            IsDoing = false;
            isFlying = false;
            StopMovementAnimation();
        }
    }


    public bool IsDone()
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
            Debug.Log(transform.position);
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
            //nav.isStopped = true;
        }
    }

    private void Climb()
    {
        isFlying = true;
    }
    private void StartMovementAnimation()
    {
        anim.SetBool(isFlyingStr, true);
    }

    private void StopMovementAnimation()
    {
        anim.SetBool(isFlyingStr, false);
    }
}
