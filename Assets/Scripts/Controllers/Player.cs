using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.InputSystem; 
 
public class Player : MonoBehaviour 
{ 
  
//////////SHIELD/////
public GameObject shield;
public float shieldDuration = 3f;
private float shieldTimer = 0f;


public Meteor meteorSpawner; // Reference to the Meteor script for spawning meteors
public float shieldRadius = 3f;
public float pushSpeed = 8f;
/// ////////




    public Transform enemyTransform; 
    public GameObject bombPrefab; 
    public GameObject powerupPrefab;
    public Vector2 bombOffset;
    public List<Transform> asteroidTransforms; 


    //Enemy Detection
    public int circlePoints = 8;
    public float radius = 3f;


    public float cornerBombDistance; // Distance from the player to spawn the bomb on a random corner

   public float warpRatio; // Ratio for warping the player towards the enemy position (0 to 1) 


   public float maxSpeed = 1f; // Speed for player movement


   public float accelerationTime = 1f; // Time it takes for the player to reach max speed
   private float acceleration; // Acceleration value calculated based on maxSpeed and accelerationTime
   private Vector3 velocity; // Current velocity of the player


   //Decceleration//
   public float decelerationTime = 2f;

    private bool wasMoving = false;
    private float decelerationTimer = 0f;
    private Vector3 velocityWhenReleased;


 
    void Start() 
    { 


        Debug.Log(NormalSizeVector(new Vector2(3, 4))); //test the NormalizeVector function 
        Debug.Log(NormalSizeVector(new Vector2(-3, 2))); 
        Debug.Log(NormalSizeVector(new Vector2(1.5f, -3.5f))); 

        //a = Δv / Δt;
        acceleration = maxSpeed / accelerationTime; // Calculate the acceleration based on maxSpeed and accelerationTime
    } 


     
    // Update is called once per frame (this is your 'boss') 
    void Update() 
    { 

        if (Keyboard.current.gKey.wasPressedThisFrame) // Activate the shield when the 'G' key is pressed
        {
            shield.SetActive(true); // Enable the shield GameObject
            shieldTimer = 0f; // Reset the shield timer
        }
        if (shield.activeSelf) // Check if the shield is currently active
        {
            shieldTimer += Time.deltaTime; // Increment the shield timer by the time elapsed since the last frame

            if (shieldTimer >= shieldDuration) // Check if the shield duration has been exceeded
            {
                shield.SetActive(false); // Disable the shield GameObject
            }
            else
            {
                PushMeteors(); // Call the PushMeteors function to push meteors away from the player while the shield is active
            }
        }






        if (Keyboard.current.bKey.wasPressedThisFrame) 
        {
        SpawnBombAtOffset(bombOffset);
    }


    if (Keyboard.current.cKey.wasPressedThisFrame) // Spawn a bomb on a random corner when the 'C' key is pressed
{
    SpawnBombOnRandomCorner(cornerBombDistance);
}

 
 if (Keyboard.current.rKey.wasPressedThisFrame) // Warp the player towards the enemy position when the 'R' key is pressed
{
    WarpPlayer(enemyTransform, warpRatio);
}


    PlayerMovement(); // Call the PlayerMovement function to handle player movement based on input

    EnemyDetection(); // Call the EnemyDetection function to detect enemies within the specified radius

    if(Keyboard.current.pKey.wasPressedThisFrame) // Spawn power-ups in a circle around the player when the 'P' key is pressed
    {
        SpawnPowerUps(radius, 5); // Spawn 5 power-ups in a circle around the player

    }
        //PlayerRotation();

    } 
 
    
  
    #region Spawn Bombs
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
    #endregion


//SpawnBombTrail (for loop repeat inNumberOfBombs) 
// count distance ((i + 1) * inBombSpacing)
   #region Spawn Bomb Trail
   public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
{
    for (int i = 0; i < inNumberOfBombs; i++)
    {
        float distance = (i + 1) * inBombSpacing;

        Vector3 offset = new Vector3(0, -distance, 0);

        SpawnBombAtOffset(offset);
    }
}
#endregion

#region Spawn Bomb on Random Corner
public void SpawnBombOnRandomCorner(float inDistance) 
{
    int randomCorner = Random.Range(0, 4); // Generate a random number between 0 and 3

    Vector3 direction;

   if (randomCorner == 0) // Top-right corner
{
    direction = (Vector3.up + Vector3.right).normalized;
}
else if (randomCorner == 1) // Top-left corner
{
    direction = (Vector3.up + Vector3.left).normalized;
}
else if (randomCorner == 2) // Bottom-right corner
{
    direction = (Vector3.down + Vector3.right).normalized;
}
else // Bottom-left corner
{
    direction = (Vector3.down + Vector3.left).normalized;
}
SpawnBombAtOffset(direction * inDistance); // Spawn a bomb at the calculated offset
}
#endregion

#region Warp Player
public void WarpPlayer(Transform target, float ratio) // Warp the player towards the target position based on the ratio
{
    if (ratio > 1)
    {
        ratio = 1;
    }

    transform.position = Vector3.Lerp(transform.position, target.position, ratio); // Lerp the player's position towards the target position based on the ratio
}
#endregion

#region Detect Asteroids
public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids) // Detect asteroids within the specified range
{
    for (int i = 0; i < inAsteroids.Count; i++) // Loop through the list of asteroids
    {
        // Calculate distance between Player and current asteroid
        float distance = Vector3.Distance(
            transform.position,
            inAsteroids[i].position
        );

        if (distance <= inMaxRange) // Check if the asteroid is within the specified range
        {
            Debug.DrawLine(transform.position, inAsteroids[i].position, Color.green); // Draw a green line to the asteroid
        }
    }
}
#endregion

// public void PlayerMovement(Vector3 velocity)
// {
//     transform.position += velocity * Time.deltaTime; // Move the player based on the velocity and deltaTime
// }

#region Player Movement
public void PlayerMovement()
{
    bool movementInput = false;

    //MOVE PLAYER WITH ARROW KEYS//
    if (Keyboard.current.leftArrowKey.isPressed)
    {
        velocity += Time.deltaTime * acceleration * Vector3.left;
        movementInput = true;
    }

    if (Keyboard.current.rightArrowKey.isPressed)
    {
        velocity += Time.deltaTime * acceleration * Vector3.right;
        movementInput = true;
    }

    if (Keyboard.current.upArrowKey.isPressed)
    {
        velocity += Time.deltaTime * acceleration * Vector3.up;
        movementInput = true;
    }

    if (Keyboard.current.downArrowKey.isPressed)
    {
        velocity += Time.deltaTime * acceleration * Vector3.down;
        movementInput = true;
    }

    // Clamp velocity to maximum speed
    if (velocity.magnitude > maxSpeed)
    {
        velocity = maxSpeed * velocity.normalized;
    }

    // Player is pressing a movement key
    if (movementInput)
    {
        wasMoving = true;
        decelerationTimer = 0f;
    }
    // Player just released the movement keys
    else if (wasMoving)
    {
        velocityWhenReleased = velocity;
        decelerationTimer = 0f;
        wasMoving = false;
    }

    // Slow the player down after releasing the input
    if (!movementInput && velocity.magnitude > 0)
    {
        decelerationTimer += Time.deltaTime;

        float ratio = decelerationTimer / decelerationTime;

        velocity = Vector3.Lerp(
            velocityWhenReleased,
            Vector3.zero,
            ratio
        );

        // Stop completely when deceleration time is reached
        if (decelerationTimer >= decelerationTime)
        {
            velocity = Vector3.zero;
        }
    }

    transform.position += velocity * Time.deltaTime;
}
#endregion

#region Enemy Detection

public void EnemyDetection()
{
    float angleStep = 360f / circlePoints;

    Color circleColor = Color.green; // Default color for the circle
    // Draw the circle and check if the enemy is inside it
    for (int i = 0; i < circlePoints; i++)
    {
        float angle = i * angleStep * Mathf.Deg2Rad;
    // Calculate the position of the point on the circle
    float x = radius * Mathf.Cos(angle);
    float y = radius * Mathf.Sin(angle);
    
    // Create a point on the circle relative to the player's position
        Vector3 point = new Vector3(x, y, 0) + transform.position;
        float nextAngle = (i + 1) * angleStep * Mathf.Deg2Rad;

    // Calculate the position of the next point on the circle
        float nextX = radius * Mathf.Cos(nextAngle);
        float nextY = radius * Mathf.Sin(nextAngle);

        Vector3 nextPoint = new Vector3(nextX, nextY, 0) + transform.position;

        //draw point to next point
        Debug.DrawLine(point, nextPoint, circleColor);

        float distance = Vector3.Distance(transform.position, enemyTransform.position);

        //check if enemy id inside the circle
        if (distance <= radius)
        {
            circleColor = Color.red;
        }
        else
        {
            circleColor = Color.green;
        }
    }
    
    }
    #endregion

#region Spawn PowerUps

public void SpawnPowerUps(float radius, int numberOfPowerups)
{
float angleStep = 360f / numberOfPowerups;
for (int i = 0; i < numberOfPowerups; i++)
{
    float angle = i * angleStep * Mathf.Deg2Rad;

    // Calculate the position of the powerup on the circle
    float x = radius * Mathf.Cos(angle);
    float y = radius * Mathf.Sin(angle);

    Vector3 powerupPosition = new Vector3(x, y, 0) + transform.position;

    // Instantiate the powerup prefab at the calculated position
    Instantiate(powerupPrefab, powerupPosition, Quaternion.identity);
}
}

void PushMeteors()
{
    for (int i = 0; i < meteorSpawner.meteors.Count; i++)
    {
        GameObject meteor = meteorSpawner.meteors[i];

        // Calculate distance between player and meteor
        float distance = Vector3.Distance(
            transform.position,
            meteor.transform.position
        );

        // Check if meteor is within shield radius
        if (distance <= shieldRadius)
        {
            // Calculate direction away from player
            Vector3 pushDirection =
                (meteor.transform.position - transform.position).normalized;

            // Change the meteor's movement direction
            meteorSpawner.meteorDirections[i] = pushDirection;
        }
    }
}





}





#endregion

// #region Player Rotation
// private void PlayerRotation()
//     {
//         if (velocity.magnitude == 0) return; // If the player is not moving, do not rotate
//         float deltaAngle = ComputeShortAngle(transform.up, velocity); // Calculate the angle between the player's up direction and the velocity direction
//         if (deltaAngle == 0) return; // If the angle is zero, do not rotate

//         float rotateDirection = Mathf.Sign(deltaAngle); // Determine the direction to rotate based on the
//         float rotationStep = angularSpeed * Time.deltaTime; // Calculate the rotation step based on the angular speed and delta time

//         if(rotationStep > Mathf.Abs(deltaAngle)) // Check if the rotation step is greater than the absolute value of the angle
//         {
//             transform.Rotate(0, 0, rotationStep * rotationDirection); // Rotate the player by the remaining angle to face the velocity direction
//         }
//         else
//         {
//             transform.Rotate(0, 0, deltaAngle); // Rotate the player by the rotation step in the determined direction
//         }
        
           
//     }
//     #endregion


//     private float ComputeShortAngle(Vector3 a, Vector3 b)
//     {
//         float angleA = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg; // Calculate the angle of vector a in degrees
//         float angleB = Mathf.Atan2(b.y, b.x) * Mathf.Rad2Deg; // Calculate the angle of vector b in degrees

//         return Mathf.DeltaAngle(angleA, angleB); // Return the shortest angle between the two angles
//     }

    
// }






