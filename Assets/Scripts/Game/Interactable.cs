using UnityEngine;

public class Interactable : MonoBehaviour {

    // Distance player needs to get to object to interact
    public float radius = 1.5f;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact () {

    }

    void Update () {

        // Check if player that is focused on this object is close enough to interact with
        if (isFocus) {
            float distance = Vector3.Distance(player.position, transform.position);
            if (!hasInteracted && distance <= radius)
            {
                hasInteracted = true;
                Interact();
            }
        }
    }

    // Sets the player that is focusing the object 
    public void onFocused (Transform playerTransform) {
        isFocus = true;
        hasInteracted = false;
        player = playerTransform;
    }

    // Removes the player on defocus
    public void onDefocused() {
        isFocus = false;
        hasInteracted = false;
        player = null;
    }


    // Used to draw a wire sphere in a scene
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
