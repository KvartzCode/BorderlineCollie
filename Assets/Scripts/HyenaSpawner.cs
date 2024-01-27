using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyenaSpawner : MonoBehaviour
{
    public GameObject hyenaBushPrefab = null;
    public GameObject normalBushPrefab = null;

    private GameObject player;
    private int maxNumberOfHyenas = 5;
    private int maxNumberOfBushes = 2;
    private List<GameObject> hyenasBushes = new();
    private List<GameObject> bushes = new();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnHyenaBushes();
        SpawnNormalBushes();
    }

    void Update()
    {
        MoveAllInactiveHyenasBushes();
    }

    private void SpawnHyenaBushes()
    {
        for (int i = 0; i < maxNumberOfHyenas; i++)
        {
            SpawnHyenaBush();
        }
    }

    private void SpawnHyenaBush()
    {
        Vector3 randomPosition = Random.insideUnitCircle * 60;

        while (randomPosition.sqrMagnitude < 1500)
            randomPosition = Random.insideUnitCircle * 60;

        GameObject newHyenaBush = Instantiate(hyenaBushPrefab, player.transform.position + randomPosition, Quaternion.identity);
        hyenasBushes.Add(newHyenaBush);
    }

    private void SpawnNormalBushes()
    {
        for (int i = 0; i < maxNumberOfBushes; i++)
        {
            Vector3 randomPosition = Random.insideUnitCircle * 60;

            while (randomPosition.sqrMagnitude < 1500)
                randomPosition = Random.insideUnitCircle * 60;

            GameObject newHyenaBush = Instantiate(normalBushPrefab, player.transform.position + randomPosition, Quaternion.identity);
            bushes.Add(newHyenaBush);
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

    private void MoveObject(GameObject objectToMove)
    {
        Vector3 randomPosition = Random.insideUnitCircle * 60;
        while (randomPosition.sqrMagnitude < 1500)
            randomPosition = Random.insideUnitCircle * 60;

        objectToMove.transform.position = player.transform.position + randomPosition;
    } 
}
