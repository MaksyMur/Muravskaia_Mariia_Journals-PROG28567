using UnityEngine;

public class SquareSpawner : MonoBehaviour
{

    Camera cam;

     //Controls the size of the squares
    public float squareSize = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
    
        {
             Vector3 mousePosition = Input.mousePosition; //get mouse position in screen space
            Vector3 worldPosition = cam.ScreenToWorldPoint(
                new Vector3(
                    mousePosition.x,
                    mousePosition.y,
                    cam.nearClipPlane
                )
            );
             worldPosition.z = 0;
             
              float halfSize = squareSize / 2;
               
            
             
             //Four corners of the square

            //TOP LEFT corner:
         Vector3 topLeft = new Vector3(
                worldPosition.x - halfSize,
                worldPosition.y + halfSize,
                0
            );


            //TOP RIGHT corner:
            Vector3 topRight = new Vector3(
                worldPosition.x + halfSize,
                worldPosition.y + halfSize,
                0
            );


            //BOTTOM LEFT corner:
            Vector3 bottomLeft = new Vector3(
                worldPosition.x - halfSize,
                worldPosition.y - halfSize,
                0
            );


            //BOTTOM RIGHT corner:
            Vector3 bottomRight = new Vector3(
                worldPosition.x + halfSize,
                worldPosition.y - halfSize,
                0
            );



              if (Input.GetMouseButtonDown(0)) // left mouse button clicked
        {
            Debug.DrawLine(
                topLeft,
                topRight,
                Color.white,
                100.0f
            );

            Debug.DrawLine(
                topRight,
                bottomRight,
                Color.white,
                100.0f
            );

            Debug.DrawLine(
                bottomRight,
                bottomLeft,
                Color.white,
                100.0f
            );

            Debug.DrawLine(
                bottomLeft,
                topLeft,
                Color.white,
                100.0f
            );
        }

        
        
        Color transparentWhite = new Color( 
            1.0f,
            1.0f,
            1.0f,
            0.5f
        );

        //Color transparentWhite = Color.red;

        //Draw the transparent square 
        Debug.DrawLine(
            topLeft,
            topRight,
            transparentWhite
        );

        Debug.DrawLine(
            topRight,
            bottomRight,
            transparentWhite
        );

        Debug.DrawLine(
            bottomRight,
            bottomLeft,
            transparentWhite
        );

        Debug.DrawLine(
            bottomLeft,
            topLeft,
            transparentWhite
        );

        }
    }
}
