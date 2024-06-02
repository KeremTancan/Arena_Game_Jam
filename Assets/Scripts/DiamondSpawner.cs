using System.Collections;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamondPrefab; // Diamond prefab to spawn
    public Transform[] spawnPoints;  // Array of possible spawn points
    private GameObject currentDiamond; // Reference to the currently spawned diamond

    private void Start()
    {
        SpawnDiamond();
    }

    private void SpawnDiamond()
    {
        if (spawnPoints.Length == 0) return;

        // Randomly select a spawn point from the array
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate the diamond prefab at the selected spawn point
        currentDiamond = Instantiate(diamondPrefab, spawnPoint.position, spawnPoint.rotation);

        // Start coroutine to wait for diamond destruction
        StartCoroutine(WaitForDiamondToBeDestroyed());
    }

    private IEnumerator WaitForDiamondToBeDestroyed()
    {
        // Wait until the current diamond is destroyed
        while (currentDiamond != null)
        {
            yield return null; // Wait for the next frame
        }

        // Wait for 1 second before spawning the next diamond
        yield return new WaitForSeconds(1f);

        // Spawn the next diamond
        SpawnDiamond();
    }
}