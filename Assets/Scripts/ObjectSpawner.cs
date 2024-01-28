using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject hyenaBushPrefab;
    public GameObject normalBushPrefab;
    public GameObject treePrefab;
    public GameObject collieBushPrefab;
    public GameObject carouselHorsePrefab;

    private GameObject player;
    private int maxNumberOfHyenas = 5;
    private int maxNumberOfBushes = 2;
    private int maxNumberOfTrees = 4;
    private List<GameObject> hyenasBushes = new();
    private List<GameObject> normalBushes = new();
    private List<GameObject> trees = new();
    private GameObject collieBush;
    private GameObject carouselHorse;

    private float spawnCircleArea = 80;
    private float minSpawnDistance = 2000;
    private float maxDistanceFromPlayer = 8000;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnHyenaBushes();
        SpawnNormalBushes();
        SpawnTrees();

        SpawnCarouselHorse();
    }

    void Update()
    {
        MoveAllInactiveHyenasBushes();
        MoveAllNormalBushes();
        MoveAllTrees();

        MoveCarouselHorse();
    }

    #region Spawning

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

    private void SpawnTrees()
    {
        for (int i = 0; i < maxNumberOfTrees; i++)
        {
            Vector3 randomPosition = GetRandomPositionAroundPlayer();

            GameObject newTree = Instantiate(treePrefab, player.transform.position + randomPosition, Quaternion.identity);
            trees.Add(newTree);
        }
    }

    private void SpawnCollieBush()
    {
        Vector3 randomPosition = GetRandomPositionAroundPlayer();

        collieBush = Instantiate(collieBushPrefab, player.transform.position + randomPosition, Quaternion.identity);
    }

    private void SpawnCarouselHorse()
    {
        Vector3 randomPosition = GetRandomPositionAroundPlayer();

        carouselHorse = Instantiate(carouselHorsePrefab, player.transform.position + randomPosition, Quaternion.identity);
    }
    #endregion

    #region Moving

    private void MoveAllInactiveHyenasBushes()
    {
        foreach (var hyenaBush in hyenasBushes)
        {
            if((hyenaBush.transform.position - player.transform.position).sqrMagnitude > maxDistanceFromPlayer)
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
            if ((normalBush.transform.position - player.transform.position).sqrMagnitude > maxDistanceFromPlayer)
                MoveObject(normalBush);
        }
    }

    private void MoveAllTrees()
    {
        foreach (var tree in trees)
        {
            if ((tree.transform.position - player.transform.position).sqrMagnitude > maxDistanceFromPlayer)
                MoveObject(tree);
        }
    }

    private void MoveCarouselHorse()
    {
        if ((carouselHorse.transform.position - player.transform.position).sqrMagnitude > maxDistanceFromPlayer)
            MoveObject(carouselHorse);
    }

    private void MoveObject(GameObject objectToMove)
    {
        Vector3 randomPosition = GetRandomPositionAroundPlayer();

        objectToMove.transform.position = player.transform.position + randomPosition;
    }

    #endregion

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector3 randomPosition = Random.insideUnitCircle * spawnCircleArea;
        while (randomPosition.sqrMagnitude < minSpawnDistance)
            randomPosition = Random.insideUnitCircle * spawnCircleArea;

        return randomPosition;
    }
}
