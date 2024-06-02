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
        if (Healths.Length > 0)
        {
            for (int i = 0; i < Healths.Length; i++)
            {
                if (Healths[i] != null)
                {
                    Destroy(Healths[i]);
                    Healths[i] = null;
                    break;
                }
            }
            
            if (AllHealthItemsDestroyed())
            {
                RestartScene();
            }
        }
    }

    bool AllHealthItemsDestroyed()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
