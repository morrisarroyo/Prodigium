using UnityEngine;

public class Interactable : MonoBehaviour {

    // Distance player needs to get to object to interact
    public float radius = 1.5f;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact () {
        // This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    void Update () {

        // Check if player that is focused on this object is close enough to interact with
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius) {
                Interact();
                hasInteracted = true;
            }
        }
    }

    // Sets the player that is focusing the object 
    public void onFocused (Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    // Removes the player on defocus
    public void onDefocused() {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


    // Used to draw a wire sphere in a scene
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
