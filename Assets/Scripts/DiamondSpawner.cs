using System.Collections;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamondPrefab; 
    public Transform[] spawnPoints;  
    private GameObject currentDiamond; 

    private void Start()
    {
        SpawnDiamond();
    }

    private void SpawnDiamond()
    {
        if (spawnPoints.Length == 0) return;
        
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        currentDiamond = Instantiate(diamondPrefab, spawnPoint.position, spawnPoint.rotation);
        StartCoroutine(WaitForDiamondToBeDestroyed());
    }

    private IEnumerator WaitForDiamondToBeDestroyed()
    {
        
        while (currentDiamond != null)
        {
            yield return null; 
        }
      
        yield return new WaitForSeconds(1f);
        SpawnDiamond();
    }
}