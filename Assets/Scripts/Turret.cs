using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

    public float angularSpeed;
    float rotationDirection = 1f; // 1 for clockwise, -1 for counter-clockwise
    float dotProduct;


    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        //draw a line from the object in its up direction
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.red);

       

        Vector3 directionToTarget = (target.position - transform.position).normalized; // Calculate the direction towards the target
        //float dotProduct = Vector3.Dot(transform.up, directionToTarget); // Calculate the dot product between the object's up direction and the direction to the target

        float deltaAngle = ComputeShortAngle(transform.up, directionToTarget); // Calculate the angle between the object's up direction and the direction to the target

        float rotateDirection = Mathf.Sign(deltaAngle); // Determine the direction to rotate based on the sign of the angle
        float angleStep = angularSpeed * Time.deltaTime; // Calculate the angle step based on the angular speed and delta time

        if(angleStep > Mathf.Abs(deltaAngle)) // Check if the angle step is greater than the absolute value of the angle
        {
             //make the object rotate at angularSpeed degrees per second
        transform.Rotate(Vector3.forward, rotationDirection * angularSpeed * Time.deltaTime);
        }
        else
        {
            //make the object rotate at angularSpeed degrees per second in the direction of the target
            transform.Rotate(Vector3.forward, deltaAngle * Time.deltaTime);
        }


        if(dotProduct >0)
        {
            Debug.Log("In Front");
        }
        else
        {
            Debug.Log("Behind");
        }

    }

    private float ComputeShortAngle(Vector3 a, Vector3 b)
    {
        float dot = Vector3.Dot(a.normalized, b.normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angle;
    }
}
