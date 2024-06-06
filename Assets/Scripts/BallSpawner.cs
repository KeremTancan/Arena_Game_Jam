using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnInterval = 10f;
    public GameObject warningIndicator;
    public float warningDuration = 2f;
    public Vector2 warningOffset = new Vector2(-2f, 0f);

    private float timeSinceLastSpawn;
    private List<Transform> spawnPoints;

    private void Start()
    {
        timeSinceLastSpawn = 0f;
        
        spawnPoints = new List<Transform>();
        GameObject[] spObjects = GameObject.FindGameObjectsWithTag("sp");
        foreach (GameObject spObject in spObjects)
        {
            spawnPoints.Add(spObject.transform);
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            StartCoroutine(SpawnBallWithWarning());
            timeSinceLastSpawn = 0f;
        }
    }

    private IEnumerator SpawnBallWithWarning()
    {
        if (spawnPoints.Count == 0)
        {
            yield break;
        }
        
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Vector2 warningPosition = (Vector2)selectedSpawnPoint.position + warningOffset;
        GameObject warning = Instantiate(warningIndicator, warningPosition, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);
        Destroy(warning);
        Instantiate(ballPrefab, selectedSpawnPoint.position, Quaternion.identity);
    }
}