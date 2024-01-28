using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollieFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject circus;


    Collider2D col;
    SpriteRenderer spriteRendererHead;
    SpriteRenderer spriteRendererBody;


    private float spawnCircleArea = 60;
    private float minSpawnDistance = 1500;

    public bool followPlayer;

    Rigidbody2D rb2d;

    public float closeEoughToStop;
    public float slowDownDistance;

    public float speed = 10;

    Vector2 direction;


    public bool instantiateTheCircus;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRendererHead = GetComponentInChildren<SpriteRenderer>();
        spriteRendererBody = GetComponentInChildren<SpriteRenderer>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        col.enabled = false;
        spriteRendererHead.enabled = false;
        rb2d = GetComponent<Rigidbody2D>();
        instantiateTheCircus = true;
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            FoundTheDog();
        }

    }

    public void FoundTheDog()
    {

        Debug.Log("FOUND");
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
        Debug.Log("FOLLOW");

        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        direction = player.transform.position - gameObject.transform.position;

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
}
