using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject hyenaBushPrefab = null;
    public GameObject normalBushPrefab = null;

    private GameObject player;
    private int maxNumberOfHyenas = 5;
    private int maxNumberOfBushes = 2;
    private List<GameObject> hyenasBushes = new();
    private List<GameObject> normalBushes = new();

    private float spawnCircleArea = 60;
    private float minSpawnDistance = 1500;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnHyenaBushes();
        SpawnNormalBushes();
    }

    void Update()
    {
        MoveAllInactiveHyenasBushes();
        MoveAllNormalBushes();
    }

    private void SpawnHyenaBushes()
    {
        for (int i = 0; i < maxNumberOfHyenas; i++)
        {
            Vector3 randomPosition = GetRandomPositionAroundPlayer();

            GameObject newHyenaBush = Instantiate(hyenaBushPrefab, player.transform.position + randomPosition, Quaternion.identity);
            hyenasBushes.Add(newHyenaBush);
        }
    }

    private void SpawnNormalBushes()
    {
        for (int i = 0; i < maxNumberOfBushes; i++)
        {
            Vector3 randomPosition = GetRandomPositionAroundPlayer();

            GameObject newHyenaBush = Instantiate(normalBushPrefab, player.transform.position + randomPosition, Quaternion.identity);
            normalBushes.Add(newHyenaBush);
        }
    }

    private void MoveAllInactiveHyenasBushes()
    {
        foreach (var hyenaBush in hyenasBushes)
        {
            if((hyenaBush.transform.position - player.transform.position).sqrMagnitude > 5000)
            {
                if (hyenaBush.GetComponentInChildren<Hyena>().state == HyenaState.Inactive)
                    MoveObject(hyenaBush);
            }
        }
    }

    private void MoveAllNormalBushes()
    {
        foreach (var normalBush in normalBushes)
        {
            if ((normalBush.transform.position - player.transform.position).sqrMagnitude > 5000)
                MoveObject(normalBush);
        }
    }

    private void MoveObject(GameObject objectToMove)
    {
        Vector3 randomPosition = GetRandomPositionAroundPlayer();

        objectToMove.transform.position = player.transform.position + randomPosition;
    } 

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector3 randomPosition = Random.insideUnitCircle * spawnCircleArea;
        while (randomPosition.sqrMagnitude < minSpawnDistance)
            randomPosition = Random.insideUnitCircle * spawnCircleArea;

        return randomPosition;
    }
}
