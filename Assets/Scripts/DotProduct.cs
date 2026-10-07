using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DotProduct : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the red and blue vectors based on the angles
        Vector3 redVector = ComputerVectorFromAngle(redAngle);
        Vector3 blueVector = ComputerVectorFromAngle(blueAngle);

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
        float dotProduct = ComputeDotProduct(redVector, blueVector);
        Debug.Log(dotProduct);
        }
       



        //calculate the value of redValue * blueValue
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     float redValue = Mathf.Cos(redAngle * Mathf.Deg2Rad) * Mathf.Sin(redAngle * Mathf.Deg2Rad);
        //     float blueValue = Mathf.Cos(blueAngle * Mathf.Deg2Rad) * Mathf.Sin(blueAngle * Mathf.Deg2Rad);

        //     float dotProduct = redValue * blueValue;

        //     Debug.Log("Dot Product: " + dotProduct);
    
        // }
    }

    private Vector3 ComputerVectorFromAngle(float angle)
    {
        float angleInRads = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRads);
        float y = Mathf.Sin(angleInRads);
        return new Vector3(x, y, 0);
    }

    private float ComputeDotProduct(Vector3 a, Vector3 b)
    {
        float dot = a.x * b.x + a.y * b.y + a.z * b.z;
        return dot;
    }

}
