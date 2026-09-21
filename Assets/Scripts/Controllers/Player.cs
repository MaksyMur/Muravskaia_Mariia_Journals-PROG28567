using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.InputSystem; 
 
public class Player : MonoBehaviour 
{ 
    public Transform enemyTransform; 
    public GameObject bombPrefab; 
    public List<Transform> asteroidTransforms; 
     
 
    void Start() 
    { 
        Debug.Log(NormalSizeVector(new Vector2(3, 4))); //test the NormalizeVector function 
        Debug.Log(NormalSizeVector(new Vector2(-3, 2))); 
        Debug.Log(NormalSizeVector(new Vector2(1.5f, -3.5f))); 
    } 
     
    // Update is called once per frame (this is your 'boss') 
    void Update() 
    { 
        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        { 
            SpawnBombAtOffset(Vector3.up); //spawn a bomb above the player 
        } 
 
    } 
 
    void SpawnBombAtOffset(Vector3 inOffset) //your 'boss' asks, you do 
    { 
       Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity); //spawn a bomb at the player's position plus the offset 
    } 
 
    Vector2 NormalSizeVector(Vector2 inVector) 
    { 
        // float magnitude = Mathf.Sqrt(inVector.x * inVector.x + inVector.y * inVector.y); //calculate the magnitude of the vector using Pythagorean theorem 
        // return new Vector2(inVector.x / magnitude, inVector.y / magnitude); //return the normalized vector 
 
 
        float magnitude = inVector.magnitude; //calculate the magnitude of the vector using the built-in magnitude property 
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude); //return the normalized vector 
        return outVector; //return the normalized vector 
 
    } 
}
