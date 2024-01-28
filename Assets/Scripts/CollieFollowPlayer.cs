using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollieFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject circus;


    Collider2D col;
    public SpriteRenderer spriteRendererHead;
    public SpriteRenderer spriteRendererBody;


    public float spawnCircleArea = 60;
    private float minSpawnDistance = 1500;

    public bool followPlayer;

    Rigidbody2D rb2d;
    Animator animator;
    public float closeEoughToStop;
    public float slowDownDistance;

    public float speed = 10;

    Vector2 direction;


    public bool instantiateTheCircus;

    AudioSource audioSource;
    public AudioClip[] laughingSounds;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        col.enabled = false;
        spriteRendererHead.enabled = false;
        rb2d = GetComponent<Rigidbody2D>();
        instantiateTheCircus = true;
        followPlayer = false;
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(LaughInBush), Random.Range(3, 11), 11);
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            FoundTheDog();
        }

        spriteRendererBody.flipX = spriteRendererHead.flipX = rb2d.velocity.x < 0;

    }

    public void FoundTheDog()
    {

        col.enabled = true;
        spriteRendererHead.enabled = true;
        spriteRendererBody.enabled = true;

        FollowPLayer();
        if (instantiateTheCircus)
        {
            SpawnCircus();
            instantiateTheCircus = false;
        }
    }

    public void FollowPLayer()
    {

        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        direction = player.transform.position - gameObject.transform.position;
        animator.SetFloat("Blend", 1);

        rb2d.velocity = direction.normalized * speed;
        if (distance < closeEoughToStop)
        {
            rb2d.velocity *= 0.5f;
            //rb2d.velocity = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (distance < slowDownDistance)
        {
            rb2d.velocity *= 0.01f;
        }

    }

    private void SpawnCircus()
    {
        CancelInvoke(nameof(LaughInBush));

        Vector3 randomPosition = GetRandomPositionAroundPlayer();

        Instantiate(circus, player.transform.position + randomPosition, Quaternion.identity);

    }


    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector3 randomPosition = Random.insideUnitCircle * spawnCircleArea;
        while (randomPosition.sqrMagnitude < minSpawnDistance)
            randomPosition = Random.insideUnitCircle * spawnCircleArea;

        return randomPosition;
    }

    private void MakeSound(AudioClip[] audioClips)
    {

        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
    private void LaughInBush()
    {
        VisualSoundCues.Instance.MadeSound(transform.position);
        MakeSound(laughingSounds);
    }
}
