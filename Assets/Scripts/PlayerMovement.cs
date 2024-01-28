using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] public Animator headAnimator;
    [SerializeField] public  Animator bodyAnimator;
    [SerializeField] GameObject deathScreen;

    GetScaredRange getScaredRange;

    Rigidbody2D rb;
    private Interactable interactable;
    Vector2 moveDirection;

    public AudioClip[] whistleSounds;
    public AudioClip[] screamSounds;
    public AudioClip[] laughSounds;
    AudioSource audioSource;

    public bool canMove = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canMove = true;
        getScaredRange = GetComponent<GetScaredRange>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(!canMove ) { return; }
        Move();
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>())
        {
            interactable = collision.GetComponent<Interactable>();
        }
    }
    
    private void MakeSound(AudioClip[] audioClips)
    {

        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
    
    void Move()
    {
        Vector2 direction = moveDirection.normalized;
        // Calculate the original movement vector
        Vector2 originalMovement = direction * moveSpeed * Time.fixedDeltaTime;

        // Use Rigidbody2D to move the player
        rb.MovePosition(rb.position + originalMovement);

        // Check if there's a collision while moving
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, originalMovement.magnitude);

        if (hit.collider != null)
        {
            // Calculate remaining distance after the collision
            float remainingDistance = originalMovement.magnitude - hit.distance;

            // Calculate and apply continuous movement along the collider
            Vector2 remainingMovement = direction * remainingDistance;
            rb.MovePosition(rb.position + remainingMovement);
        }
    }

    public void GetScared()
    {
        headAnimator.SetTrigger("scared");
        MakeSound(screamSounds);
    }
    public void GetHappy()
    {
        headAnimator.SetTrigger("happy");
        MakeSound(laughSounds);

    }

    #region Input Methods

    public void OnMove(InputValue input)
    {
        if (!canMove) { return; }

        moveDirection = input.Get<Vector2>();

        Debug.Log(moveDirection);

        headAnimator.SetFloat("Blend", moveDirection.magnitude);
        bodyAnimator.SetFloat("Blend", moveDirection.magnitude);

        rb.velocity = moveDirection * moveSpeed;
    }


    public void OnWhistle()
    {
        if (!canMove) { return; }
        MakeSound(whistleSounds);
        getScaredRange.ScareHyenas();
        Debug.Log("WHISTLE!");
    }

    public void OnInteract()
    {
        if (!canMove) { return; }

        Debug.Log("Interact Triggered");

        if(interactable != null )
        {
            interactable.Interact();
        }
    }

    internal void Die()
    {
        if(canMove == true)
        {
            MakeSound(screamSounds);
            deathScreen.SetActive(true);
            canMove = false;
        }
        
    }

    #endregion
}
