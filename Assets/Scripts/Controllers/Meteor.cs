using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Meteor : MonoBehaviour
{

    public float spawnInterval = 2f;
    private float spawnTimer = 0f;

    public GameObject meteorPrefab; // Prefab for the meteor object

   public List<Vector3> meteorDirections = new List<Vector3>(); 


   public List<GameObject> meteors = new List<GameObject>(); // List to store the instantiated meteor objects

   private List<float> rotationSpeeds = new List<float>(); // List to store the rotation speeds for each meteor

    public float speed = 3f; // Speed at which the meteor moves downwards and to the left
    private Vector3 direction; // Direction for the meteor to move downwards and to the left

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //meteor spawn on the top of the screen
        Vector3 spawnPosition = new Vector3(
            Random.Range(-17f, 17f), // Random x position within the screen bounds
            11f, // y position above the screen
            0f // z position
        );

        // Generate a random rotation speed between -180f and 180f
        float rotationSpeed = Random.Range(-180f, 180f);
        rotationSpeeds.Add(rotationSpeed);

        direction = new Vector3(-1, -1, 0).normalized; // Direction for the meteor to move downwards and to the left

        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity); // Instantiate the meteor prefab at the spawn position with no rotation
        meteors.Add(meteor);

        meteorDirections.Add(direction);
    }

    // Update is called once per frame
    void Update()
{
    // Move and rotate every meteor each frame
    for (int i = 0; i < meteors.Count; i++)
    {
        meteors[i].transform.position += meteorDirections[i] * speed * Time.deltaTime; // Move the meteor downwards and to the left based on the direction and speed

        meteors[i].transform.Rotate(Vector3.forward, rotationSpeeds[i] * Time.deltaTime); // Rotate the meteor around the z-axis based on its rotation speed
    }

    // Count time between meteor spawns
    spawnTimer += Time.deltaTime;

    if (spawnTimer >= spawnInterval)
    {
        spawnTimer = 0f;

        Vector3 spawnPosition = new Vector3(
            Random.Range(-17f, 17f),
            11f,
            0f
        );

        float rotationSpeed = Random.Range(-180f, 180f);

        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity); // Instantiate the meteor prefab at the spawn position with no rotation

        meteors.Add(meteor);
        meteorDirections.Add(direction);
        rotationSpeeds.Add(rotationSpeed);
    }
}
}
