using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{


    Camera cam;

    //Previous and current mouse positions
    Vector3 previousPosition;
    Vector3 currentPosition;

    //Timer
    float timer = 0.0f;
    float interval = 0.1f;

    //Checks if we already started drawing
    bool isDrawing = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      cam = Camera.main;  
    }

    // Update is called once per frame
    void Update()
    {
        //Get mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        //Convert mouse position from Screen to World
        currentPosition = cam.ScreenToWorldPoint(
            new Vector3(
                mousePosition.x,
                mousePosition.y,
                cam.nearClipPlane
            )
        );

        currentPosition.z = 0;

        //While the left mouse button is held down
        if (Mouse.current.leftButton.isPressed)
        {
            //Beginning of a new line
            if (isDrawing == false)
            {
                previousPosition = currentPosition;
                timer = 0.0f;
                isDrawing = true;
            }
            else
            {
                //Count how much time has passed
                timer += Time.deltaTime;

                //Every 0.1 seconds draw the next part of the line
                if (timer >= interval)
                {
                    Debug.DrawLine(
                        previousPosition,
                        currentPosition,
                        Color.white,
                        100.0f
                    );

                    //Current position becomes the new previous position
                    previousPosition = currentPosition;

                    //Restart the timer
                    timer = 0.0f;
                }
            }
        }
        else
        {
            //Mouse was released, so the line is finished
            isDrawing = false;
            timer = 0.0f;
        } 
    }
}
