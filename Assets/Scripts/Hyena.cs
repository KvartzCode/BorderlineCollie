using UnityEngine;

public enum HyenaState
{
    Inactive,
    Retreating, // Running away from player
    Lurking, // Hding outside player view until attack is triggered
    Approaching, // Can be scared away during this state
    Attacking
}

public class Hyena : MonoBehaviour
{

    GetScaredRange getScaredRange;
    public HyenaState state = HyenaState.Inactive;
    public SpriteRenderer bodySprite;
    public SpriteRenderer headSprite;
    public Animator animator;
    public AudioClip[] laughingSounds;
    public AudioClip[] screamSounds;
    private GameObject player;
    private Rigidbody2D rb2d;
    private Collider2D collider2d;

    private int confidence = 1;
    private float actionTimer = 0;
    private float timeBetweenActions = 1;

    private float attackTimer = 10;
    AudioSource audioSource;

    void Start()
    { 
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        getScaredRange = player.GetComponent<GetScaredRange>();
        getScaredRange.AddHyena(this);
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        bodySprite.enabled = headSprite.enabled = false;
        collider2d.enabled = false;
        InvokeRepeating(nameof(LaughInBush), Random.Range(3, 11), 11);
    }

    void Update()
    {
        if (actionTimer <= 0)
        {
            actionTimer = timeBetweenActions;
            PerformAction();
        }

        actionTimer -= Time.deltaTime;

        bodySprite.flipX = headSprite.flipX = rb2d.velocity.x < 0;
    }

    private void PerformAction()
    {
        Vector2 direction = player.transform.position - transform.position;
        switch (state)
        {
            case HyenaState.Inactive:
                rb2d.velocity = Vector2.zero;
                break;

            case HyenaState.Retreating:
                rb2d.velocity = direction.normalized * -14;
                animator.SetFloat("Blend", 0.8f);
                if (direction.sqrMagnitude > 900)
                {
                    rb2d.velocity = Vector2.zero;
                    state = HyenaState.Lurking;
                    timeBetweenActions = 8;
                    actionTimer = 0;
                    bodySprite.enabled = headSprite.enabled = false;
                    collider2d.enabled = false;
                }
                break;

            case HyenaState.Lurking:
                Vector3 randomPosition = Random.insideUnitCircle.normalized * 35;

                transform.position = player.transform.position + randomPosition;

                if (Random.Range(0, 15) + confidence >= 15)
                {
                    state = HyenaState.Approaching;
                    timeBetweenActions = 0.1f;
                    actionTimer = 0;
                    bodySprite.enabled = headSprite.enabled = true;
                    collider2d.enabled = true;
                }
                break;

            case HyenaState.Approaching:
                rb2d.velocity = direction.normalized * 4;
                attackTimer -= 0.1f;
                animator.SetFloat("Blend", 0.5f);
                if(direction.sqrMagnitude <= 6 || attackTimer <= 0)
                {
                    state = HyenaState.Attacking;
                    timeBetweenActions = 0.1f;
                    actionTimer = 0;
                    attackTimer = 10;
                }
                break;

            case HyenaState.Attacking:
                rb2d.velocity = direction.normalized * 15;
                animator.SetFloat("Blend", 1);
                break;
        }
    }
    private void MakeSound(AudioClip[] audioClips )
    {
        
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
    private void LaughInBush()
    {
        VisualSoundCues.Instance.MadeSound(transform.position);
        MakeSound(laughingSounds);
    }
    public void Activate()
    {
        CancelInvoke(nameof(LaughInBush));
        player.GetComponent<PlayerMovement>().GetScared();
        state = HyenaState.Retreating;
        timeBetweenActions = 0.5f;
        actionTimer = 0;
        var foundHyenas = FindObjectsOfType<Hyena>();
        bodySprite.enabled = headSprite.enabled = true;
        collider2d.enabled = true;
       

        foreach (var hyena in foundHyenas)
        {
            if (hyena == this) continue;
            hyena.OtherHyenaActivated();
        }
    }

    private void OtherHyenaActivated()
    {
        confidence++;
    }

    public void GetScared()
    {
        if (state == HyenaState.Approaching || state == HyenaState.Attacking)
        {
            confidence++;
            state = HyenaState.Retreating;
            timeBetweenActions = 0.5f;
            attackTimer = 10;
            MakeSound(screamSounds);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
            
        }
    }

    private void OnDestroy()
    {
        getScaredRange.RemoveHyena(this);
    }
}
