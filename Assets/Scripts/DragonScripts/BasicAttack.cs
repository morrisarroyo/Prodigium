using UnityEngine;

public class BasicAttack : DragonAttack
{
    public int attackDamage = 10;
    Combat combat;
    Animator anim;
    GameObject player;
    Dragon dragon;

    int currentRepeatCount;
    bool playerInRange;

    public override string Name
    {
        get
        {
            return "BasicAttack";
        }
    }


    public override bool IsDoing
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        dragon = GetComponent<Dragon>();
        combat = GetComponent<Combat>();
        IsDoing = false;
        repeatCount = 3; // -1
        currentRepeatCount = repeatCount;
        playerInRange = false;
        player = dragon.player;
        //minTriggerRange = 1f;
        //maxTriggerRange = 3f;
    }



    // Update is called once per frame
    void Update()
    {

        player = dragon.player;
    }

    public override void Do()
    {
        if (!IsDone())
        {
            IsDoing = true;
            TriggerAttackAnimation();
        }
    }

    public override bool IsDone()
    {
        return currentRepeatCount <= 0;
    }

    public override bool CanDo()
    {
        return true;
    }

    private void TriggerAttackAnimation()
    {
        if (IsDoing)
        {
            if (IsDone())
            {
                IsDoing = false;
                currentRepeatCount = repeatCount;
            }
            else
            {
                anim.SetTrigger(Name);
            }

            --currentRepeatCount;
        }
    }

    public void DealDamage()
    {
        if (playerInRange)
        {
            combat.dealDamage(player.GetComponent<PlayerController>(), attackDamage);
            Debug.Log("Dealt Basic Attack Damage (" + attackDamage + ")");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Dealt Dragon Attack Damage!!!");
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Dealt Dragon Attack Damage!!!");
            playerInRange = false;
        }
    }
}

