using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector2 movement = new Vector2(moveX, moveY);

        // Move the player using Rigidbody
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }
}
