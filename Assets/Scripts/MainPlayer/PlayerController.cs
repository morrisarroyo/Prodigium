using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : BaseCreature {

    public int healthValue;
    public int basicAttackDamageValue = 10;
    public int movementSpeedValue = 5;

    // Used to set focus
    public Interactable focus;

    // Used to specify a layer
    public LayerMask movementMask;
    public bool dead;

    // Camera variable
    Camera cam;

    // Used to move player
    PlayerMotor motor;

    Combat combat;

    Animator anim;

    NavMeshAgent nav;

	Gear gear;

    // Use this for initialization
    void Start() {
        dead = false;
        health = healthValue;
        basicAttackDamage = basicAttackDamageValue;
        movementSpeed = movementSpeedValue;
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        combat = GetComponent<Combat>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = movementSpeed;
		gear = GetComponent<Gear> ();
    }

    // Update is called once per frame
    void Update() {
		if (!dead) {
			Move ();
			if (Input.GetKeyDown (KeyCode.Q))
				ConsumeHealthPotion ();
		}
    }

    // Sets focus to interactable
    public void SetFocus (Interactable newFocus) {
        // If focus is already set then defocus that first
        if (newFocus != focus) {
            if (focus != null)
                focus.onDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.onFocused(transform);

    }

    // Removes focus from interactable
    public void RemoveFocus () {
        if (focus != null) {
            focus.onDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }

    public override void Move()
    {

        //On LEFT mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Moves player to what the raycast hits without focusing on any objects
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Move to where we hit
                motor.MoveToPoint(hit.point);

                // Stop focusing any objects
                RemoveFocus();
            }
        }

        // On RIGHT mouse click
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Moves player to what the raycast hits when focusing on an object
            if (Physics.Raycast(ray, out hit, 100))
            {
                //C heck if we hit interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                // Set focus if the player finds an interactable 
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    public override void BasicAttack()
    {
        if (!anim.GetBool("attacking"))
        {
			Weapon weapon = GetComponent<Gear>().currentWeapon;
			int damageTotal = basicAttackDamage;
			if (weapon != null)
				damageTotal = Mathf.RoundToInt(basicAttackDamage * weapon.attackModifier);
			combat.dealDamage(focus.GetComponent<BaseCreature>(), damageTotal);
            anim.SetTrigger("attack");
        }
    }

    public override void Die()
    {
        dead = true;
        anim.SetTrigger("dead");
        GetComponent<NavMeshAgent>().enabled = false;
    }

    public override void TakeDamage(int damage)
    {
		Armour armour = GetComponent<Gear> ().currentArmour;
		int damageTotal = damage;
		if (armour != null)
			damageTotal = Mathf.RoundToInt(damage / armour.armourModifier);
        health = health - damageTotal;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

	public void ConsumeHealthPotion(){
		if (gear.RemoveHealthPotion()) {
			health += 100;
			if (health > healthValue)
				health = healthValue;
		}
		Debug.Log (health);
	}
}
