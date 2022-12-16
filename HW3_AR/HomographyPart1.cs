using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomographyPart1 : MonoBehaviour {

    public double[,] source = new double[4, 3] { { 256,390,0 }, { 418, 842,0 }, { 347,647, 0 }, { 741, 596,0 } };
    public double[,] destination = new double[4, 3] { { 256, 390, 0 }, { 418, 842, 0 }, { 347, 647, 0 }, { 741, 596, 0 } };


    private void Start() {
        double[,] homographicMatixOfTest = Homography.HomographyMatrixCalculator(source, destination);

        string outputOfCalculation = "";

        for (int i = 0; i < homographicMatixOfTest.GetLength(0); i++)
        {
            for (int j = 0; j < homographicMatixOfTest.GetLength(1); j++)
                outputOfCalculation += homographicMatixOfTest[i, j] + " ";
            outputOfCalculation += "\n";
        }

        Debug.Log("Homograpic Matrix Result: \n\n" + outputOfCalculation);
        
        // Part 1.3
        double[,] xy = new double[3, 1] { { source[2,0] }, { source[2,1] } , { 1 } };
        Homography.ProjectionMatrixCalculator(homographicMatixOfTest, xy, true);
    }

}
