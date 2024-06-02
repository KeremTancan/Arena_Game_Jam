using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBall : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f; 
    public float rotationSpeed = 100f;
    private GameObject[] Healths;

    private int currentPointIndex = 0; 

    void Start()
    {
        // Find all objects tagged as "Kalp" and add them to the Healths array
        GameObject[] kalpObjects = GameObject.FindGameObjectsWithTag("Kalp");
        Healths = new GameObject[kalpObjects.Length];
        for (int i = 0; i < kalpObjects.Length; i++)
        {
            Healths[i] = kalpObjects[i];
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        MoveToPatrolPoint();
    }

    void MoveToPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 direction = targetPoint.position - transform.position;
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DestroyHealthItem();
        }
    }

    void DestroyHealthItem()
    {
        // Check if there are any health items left
        if (Healths.Length > 0)
        {
            // Find the first non-null health item
            for (int i = 0; i < Healths.Length; i++)
            {
                if (Healths[i] != null)
                {
                    // Destroy the health item
                    Destroy(Healths[i]);
                    // Set the reference to null to mark it as destroyed
                    Healths[i] = null;
                    // Exit the loop after destroying one item
                    break;
                }
            }

            // Check if all health items are destroyed
            if (AllHealthItemsDestroyed())
            {
                RestartScene();
            }
        }
    }

    bool AllHealthItemsDestroyed()
    {
        // Check if all health items are null
        foreach (GameObject health in Healths)
        {
            if (health != null)
            {
                return false;
            }
        }
        return true;
    }

    void RestartScene()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
