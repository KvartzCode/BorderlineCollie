using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyenaSpawner : MonoBehaviour
{
    public GameObject hyenaPrefab;

    private GameObject player;
    private int maxNumberOfHyenas = 5;
    private List<GameObject> hyenas = new();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnHyenas();
    }

    void Update()
    {
        foreach (var hyena in hyenas)
        {
            if ((hyena.transform.position - player.transform.position).sqrMagnitude > 5000)
            {
                MoveObject(hyena);
            }
        }
    }

    private void SpawnHyenas()
    {
        for (int i = 0; i < maxNumberOfHyenas; i++)
        {
            SpawnHyena();
        }
    }

    private void SpawnHyena()
    {
        Vector3 randomPosition = Random.insideUnitCircle * 60;

        while (randomPosition.sqrMagnitude < 1000)
            randomPosition = Random.insideUnitCircle * 60;



        GameObject newHyena = Instantiate(hyenaPrefab, player.transform.position + randomPosition, Quaternion.identity);
        hyenas.Add(newHyena);
    }

    private void MoveAllInactiveHyenas()
    {
        foreach (var hyena in hyenas)
        {
            if (hyena.GetComponent<Hyena>().state != HyenaState.Inactive) continue;
            Vector3 randomPosition = Random.insideUnitCircle * 60;
            while (randomPosition.sqrMagnitude < 1000)
                randomPosition = Random.insideUnitCircle * 60;

            hyena.transform.position = player.transform.position + randomPosition;
        }
    }

    private void MoveObject(GameObject objectToMove)
    {
        Vector3 randomPosition = Random.insideUnitCircle * 60;
        while (randomPosition.sqrMagnitude < 1000)
            randomPosition = Random.insideUnitCircle * 60;

        objectToMove.transform.position = player.transform.position + randomPosition;
    } 
}
