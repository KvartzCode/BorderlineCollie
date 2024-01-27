using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyenaSpawner : MonoBehaviour
{
    public GameObject hyenaPrefab;

    private GameObject player;
    private Vector3 lastSpawnLocation;
    private int maxNumberOfHyenas = 5;
    private List<GameObject> hyenas = new();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnHyenas();
    }

    // Update is called once per frame
    void Update()
    {
        if ((lastSpawnLocation - player.transform.position).sqrMagnitude > 1000)
            MoveAllInactiveHyenas();
    }

    private void SpawnHyenas()
    {
        lastSpawnLocation = player.transform.position;
        for (int i = 0; i < maxNumberOfHyenas; i++)
        {
            SpawnHyena();
        }
    }

    private void SpawnHyena()
    {
        Vector3 randomPosition = Random.insideUnitCircle * 45;

        while (randomPosition.sqrMagnitude < 200)
            randomPosition = Random.insideUnitCircle * 45;

        GameObject newHyena = Instantiate(hyenaPrefab, player.transform.position + randomPosition, Quaternion.identity);
        hyenas.Add(newHyena);
    }

    private void MoveAllInactiveHyenas()
    {
        lastSpawnLocation = player.transform.position;
        foreach (var hyena in hyenas)
        {
            if (hyena.GetComponent<Hyena>().state != HyenaState.Inactive) continue;
            Vector3 randomPosition = Random.insideUnitCircle * 45;
            while (randomPosition.sqrMagnitude < 200)
                randomPosition = Random.insideUnitCircle * 45;

            hyena.transform.position = player.transform.position + randomPosition;
        }
    }
}
