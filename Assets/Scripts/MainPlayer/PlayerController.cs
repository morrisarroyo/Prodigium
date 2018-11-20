using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : BaseCreature
{
    //public GameObject test;
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
    void Start()
    {
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
        if (newFocus != focus)
        {
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
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
			|| Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
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
        } else if (Application.platform == RuntimePlatform.PS4) {
            Vector3 nextDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (nextDir != Vector3.zero)
            {
                Vector3 camF = cam.transform.forward;
                Vector3 camR = cam.transform.right;
                camF.y = 0;
                camR.y = 0;
                camF = camF.normalized;
                camR = camR.normalized;

                Vector3 movement = transform.position + (camF * nextDir.z + camR * nextDir.x);
                transform.rotation = Quaternion.LookRotation((camF * nextDir.z + camR * nextDir.x).normalized);
                movement.y = 0;

                Ray ray = cam.ViewportPointToRay(cam.WorldToViewportPoint(movement));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
            }
        }
        /* PS4 Controls */
        //Input.GetAxis("Vertical"); // Left Stick Y Axis
        //Input.GetAxis("Horizontal"); // Left Stick X Axis
        //Input.GetAxis("PS4RightStickX"); // Right Stick Y Axis
        //Input.GetAxis("PS4RightStickY"); // Right Stick X Axis
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
        HealthManager.health = health;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

	public void ConsumeHealthPotion(){
		if (gear.RemoveHealthPotion()) {
			health += 100;
			if (health > healthValue)
				health = healthValue;
                HealthManager.health = health;
        }
        Debug.Log (health);
	}
}
