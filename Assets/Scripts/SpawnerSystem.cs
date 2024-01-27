using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject collie;

    Vector3 spawnPosition;
    public GameObject spawnAreaRandom;

    Vector2 randomPos;

    public float range;

    float timer;
    public bool playerIsFarAway;

    float distance;

    public float distanceFromSpawner = 10;

    GameObject[] countDogs;

    public float timeToMove = 5;

    public static bool followThePlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!followThePlayer) 
        {
            MoveRandomizeToPLayer();
        }
    }

    private void MoveRandomizeToPLayer()
    {

        countDogs = GameObject.FindGameObjectsWithTag("Collie");

        distance = Vector2.Distance(player.transform.position, spawnAreaRandom.transform.position);
        if (distance > distanceFromSpawner)
        {
            timer += Time.deltaTime;

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, 100 * Time.deltaTime);

            if(timer > timeToMove)
            {
                RandomizeSpawning();
                timer = 0;
            }
        }
    }

    private void RandomizeSpawning()
    {
        randomPos = Random.insideUnitCircle * range;
        spawnPosition = new Vector3(randomPos.x, randomPos.y, 0);

        spawnPosition += spawnAreaRandom.transform.position;

        countDogs[0].transform.position = new Vector2(spawnPosition.x, spawnPosition.y);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnAreaRandom.transform.position, range);
    }


}
