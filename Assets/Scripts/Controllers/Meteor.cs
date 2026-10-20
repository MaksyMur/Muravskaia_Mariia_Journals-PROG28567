using UnityEngine;
using System.Collections.Generic;

public class Meteor : MonoBehaviour
{

    public float spawnInterval = 2f;
    private float spawnTimer = 0f;

    public GameObject meteorPrefab; // Prefab for the meteor object
   private List<GameObject> meteors = new List<GameObject>();

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

        direction = new Vector3(-1, -1, 0).normalized; // Direction for the meteor to move downwards and to the left

        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity); // Instantiate the meteor prefab at the spawn position with no rotation
        meteors.Add(meteor);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject meteor in meteors)
        {
            meteor.transform.position += direction * speed * Time.deltaTime;
        }

        spawnTimer += Time.deltaTime; // Increment the spawn timer by the time elapsed since the last frame
        if (spawnTimer >= spawnInterval) // Check if the spawn timer has reached the spawn interval
        {
            spawnTimer = 0f; // Reset the spawn timer

            Vector3 spawnPosition = new Vector3(
                Random.Range(-17f, 17f), // Random x position within the screen bounds
                11f, // y position above the screen
                0f // z position
            );

            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity); // Instantiate a new meteor prefab at the spawn position with no rotation
            meteors.Add(meteor);
        }
    }
}
