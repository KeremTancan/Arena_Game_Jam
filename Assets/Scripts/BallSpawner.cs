using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnInterval = 10f; 
    public Transform spawnPoint1; 
    public Transform spawnPoint2; 
    public GameObject warningIndicator; 
    public float warningDuration = 2f; 
    private Vector2 warningOffset = new Vector2(-2f, 0f); 

    private float timeSinceLastSpawn;

    private void Start()
    {
        timeSinceLastSpawn = 0f; 
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
        Transform selectedSpawnPoint = (Random.value > 0.5f) ? spawnPoint1 : spawnPoint2;
        Vector2 warningPosition = (Vector2)selectedSpawnPoint.position + warningOffset;
        GameObject warning = Instantiate(warningIndicator, warningPosition, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);
        Destroy(warning);
        Instantiate(ballPrefab, selectedSpawnPoint.position, Quaternion.identity);
    }
}