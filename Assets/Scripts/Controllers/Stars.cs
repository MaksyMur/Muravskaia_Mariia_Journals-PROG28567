using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Tracks how long the current line has been drawing
    private float drawingTimer = 0f;

    // Tracks the current star in the list
    private int i = 0;


    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }


    private void DrawConstellation()
    {
        
        drawingTimer += Time.deltaTime; // Increment the drawing timer by the time elapsed since the last frame

        
        float ratio = drawingTimer / drawingTime; // Calculate the ratio of the drawing timer to the total drawing time

        // Find the current end position of the line
        Vector3 lineEnd = Vector3.Lerp(
            starTransforms[i].position,
            starTransforms[i + 1].position,
            ratio
        );

      

       Debug.DrawLine(
        starTransforms[i].position, lineEnd, Color.green,
        drawingTime * starTransforms.Count
        );

          if (ratio >= 1f) // Check if the line has finished drawing
        {
            drawingTimer = 0f; // Reset the drawing timer
            i++; // Move to the next star in the list

            if (i >= starTransforms.Count - 1) // Check if we have reached the last star
            {
                i = 0; // Reset to the first star
            }
        }
    }
}

