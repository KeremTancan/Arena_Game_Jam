using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Assign the ball prefab in the inspector
    public float spawnInterval = 15f; // Time between spawns

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
            SpawnBall(); 
            timeSinceLastSpawn = 0f; 
        }
    }

    private void SpawnBall()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
    
}