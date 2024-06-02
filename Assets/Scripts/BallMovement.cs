using System;
using Unity.VisualScripting;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the ball moving left
    public float rotationSpeed = 100f; // Speed of the ball's rotation

    private void Start()
    {
        Destroy(gameObject,7f);
    }

    private void Update()
    {
        // Move the ball to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the ball around its own axis
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}