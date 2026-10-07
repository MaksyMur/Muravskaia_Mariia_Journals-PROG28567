using UnityEngine;

public class Turret : MonoBehaviour
{

    public float angularSpeed;

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

        //make the object rotate at angularSpeed degrees per second
        transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);

        Vector3 directionToTarget = (target.position - transform.position).normalized; // Calculate the direction towards the target
        float dotProduct = Vector3.Dot(transform.up, directionToTarget); // Calculate the dot product between the object's up direction and the direction to the target

        if(dotProduct >0)
        {
            Debug.Log("In Front");
        }
        else
        {
            Debug.Log("Behind");
        }

    }
}
