using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    Vector3 targetPoint; // Target point for the asteroid to move towards

    // Start is called before the first frame update
    void Start()
    {
        ChooseNewTarget(); // Choose an initial random target point for the asteroid to move towards
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void ChooseNewTarget() // Choose a new random target point for the asteroid to move towards
{
    Vector3 randomDirection = new Vector3(
        Random.Range(-1f, 1f),
        Random.Range(-1f, 1f),
        0
    ).normalized;

    targetPoint = transform.position + randomDirection * maxFloatDistance;
}

public void AsteroidMovement() // Move the asteroid towards the target point
{
  Vector3 direction = (targetPoint - transform.position).normalized; // Calculate the direction towards the target point

    transform.position += direction * moveSpeed * Time.deltaTime; 

    float distance = Vector3.Distance(transform.position, targetPoint);

    if (distance <= arrivalDistance) // Check if the asteroid has reached the target point
    {
        ChooseNewTarget(); // Choose a new random target point for the asteroid to move towards
    }
}



}
