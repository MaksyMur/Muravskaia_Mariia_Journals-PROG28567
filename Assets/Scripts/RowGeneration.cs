using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    //InputField where we enter the number of squares
    public TMP_InputField squareNumberInput;

    //Size of each square
    public float squareSize = 1.0f;

    //This method is called when the Generate button is pressed
    public void Generate()
    {
        //Get the number of squares from the InputField
        int numberOfSquares = int.Parse(squareNumberInput.text);

        //Repeat once for every square
        for (int i = 0; i < numberOfSquares; i++)
        {
            //Position of the current square
            float xPosition = i * squareSize;

            //Four corners of the square
            Vector3 bottomLeft = new Vector3(xPosition, 0, 0);
            Vector3 bottomRight = new Vector3(xPosition + squareSize, 0, 0);
            Vector3 topLeft = new Vector3(xPosition, squareSize, 0);
            Vector3 topRight = new Vector3(xPosition + squareSize, squareSize, 0);

            //Draw the square
            Debug.DrawLine(bottomLeft, bottomRight, Color.white, 100.0f);
            Debug.DrawLine(bottomRight, topRight, Color.white, 100.0f);
            Debug.DrawLine(topRight, topLeft, Color.white, 100.0f);
            Debug.DrawLine(topLeft, bottomLeft, Color.white, 100.0f);
        }
    }
}