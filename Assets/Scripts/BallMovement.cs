using System;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float rotationSpeed = 100f; 

    private void Start()
    {
        Destroy(gameObject,7f);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}