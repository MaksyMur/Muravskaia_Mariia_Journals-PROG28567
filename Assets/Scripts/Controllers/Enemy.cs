using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

 public Transform playerTransform; // Reference to the player's transform
 

public Vector3 velocity; 

public float acceleration;
public float maxSpeed;
public float stopDistance;
    
    private void Update()
    {
        EnemyMovement();
    }

public void EnemyMovement()
    {
       Vector3 direction = (playerTransform.position - transform.position).normalized; // Calculate the direction towards the player

       velocity += Time.deltaTime * acceleration * direction;

       if (velocity.magnitude > maxSpeed) // Clamp velocity to maximum speed
       {
           velocity = maxSpeed * velocity.normalized;
       }
        
        float distance = Vector3.Distance(transform.position, playerTransform.position); // Calculate distance between Enemy and Player

        if (distance <= stopDistance) // Check if the Enemy is within the specified stop distance
        {
            velocity = Vector3.zero; // Stop the Enemy's movement
        }
            transform.position += velocity * Time.deltaTime; // Move the Enemy based on the velocity and deltaTime

        }
    }
   

