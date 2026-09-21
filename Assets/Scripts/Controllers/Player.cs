using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.InputSystem; 
 
public class Player : MonoBehaviour 
{ 
    public Transform enemyTransform; 
    public GameObject bombPrefab; 
    public Vector2 bombOffset;
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
        if (Keyboard.current.bKey.wasPressedThisFrame) 
        {
        SpawnBombAtOffset(bombOffset);
    }
 
    } 
 
    public void SpawnBombAtOffset(Vector3 inOffset) //your 'boss' asks, you do 
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


//SpawnBombTrail (for loop repeat inNumberOfBombs) 
// count distance ((i + 1) * inBombSpacing)
   public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
{
    for (int i = 0; i < inNumberOfBombs; i++)
    {
        float distance = (i + 1) * inBombSpacing;

        Vector3 offset = new Vector3(0, -distance, 0);

        SpawnBombAtOffset(offset);
    }
}



}

