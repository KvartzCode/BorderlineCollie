using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollieFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject circus;

    private float spawnCircleArea = 60;
    private float minSpawnDistance = 1500;

    public bool followPlayer;

    public float closeEoughToStop;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            FollowPLayer();
            SpawnCircus();
            followPlayer = false;
        }

    }


    public void FollowPLayer()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (distance > closeEoughToStop)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
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
