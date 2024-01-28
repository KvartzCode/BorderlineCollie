using UnityEngine;

public class SpriteOrderController : MonoBehaviour
{
    public Transform spriteBase;
    public int behindOrder = 0; // Order in Layer when player is behind
    public int inFrontOrder = 1; // Order in Layer when player is in front

    private Transform playerFeet; // Reference to the player's transform
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerFeet = GameObject.FindGameObjectWithTag("PlayerFeet").transform; // I'm too lazy to do an optimal solution atm.
    }

    void Update()
    {
        // Compare player's y-position with the tree's y-position
        if (playerFeet.position.y > spriteBase.position.y)
        {
            // Player is in front of the tree
            spriteRenderer.sortingOrder = inFrontOrder;
        }
        else
        {
            // Player is behind the tree
            spriteRenderer.sortingOrder = behindOrder;
        }
    }
}