using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.LinearAlgebra.Double;

public class Homography : MonoBehaviour
{

    #region Public_Static_Methods

    public static double[,] HomographyMatrixCalculator(double[,] sourceTarget, double[,] destinationTarget)
    {
        var x = HomographyMatrixCalculatorHelper(sourceTarget, destinationTarget);
        double[,] hm = new double[3, 3];

        int row = 0;
        for (int i = 0; i < x.Length; i++)
        {
            if (i % 3 == 0 && i != 0)
                ++row;
            hm[row, i % 3] = x[i];
        }


        return hm;
    }

    public static double[,] ProjectionMatrixCalculator(double[,] hm, double[,] xy, bool log)
    {
        double[,] match = Homography.HomographyApplier(hm, xy);
        if (log)
        {
            Debug.Log("(x,y) : " + xy[0, 0] + " , " + xy[1, 0] + " , " + xy[2, 0]);
            Debug.Log("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
        }

        return match;
    }

    public static double[,] CalcInverseProjection(double[,] hm, double[,] uv, bool log)
    {
        double[,] match = Homography.ApplyInverseHomography(hm, uv);
        if (log)
        {
            Debug.Log("(u,v) : " + uv[0, 0] + " , " + uv[1, 0] + " , " + uv[2, 0]);
            Debug.Log("(x,y) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
        }

        return match;
    }

    #endregion

    #region Private_Static_Methods

    public static double[] HomographyMatrixCalculatorHelper(double[,] sour, double[,] dest)
    {
        int Column = sour.GetLength(0);
        int Rows = 9;

        double[,] TempMatrix = new double[2 * Column, Rows];

        // current point
        int p = 0;
        for (int i = 0; i < 2 * Column; i++)
        {
            if (i % 2 == 0)
            {
                TempMatrix[i, 0] = -1 * sour[p, 0];TempMatrix[i, 1] = -1 * sour[p, 1];
                TempMatrix[i, 2] = -1; TempMatrix[i, 3] = 0;
                TempMatrix[i, 4] = 0; TempMatrix[i, 5] = 0;
                TempMatrix[i, 6] = sour[p, 0] * dest[p, 0];TempMatrix[i, 7] = dest[p, 0] * sour[p, 1];
                TempMatrix[i, 8] = dest[p, 0];
            }
            else
            {
                TempMatrix[i, 0] = 0;
                TempMatrix[i, 1] = 0;
                TempMatrix[i, 2] = 0;
                TempMatrix[i, 3] = -1 * sour[p, 0];
                TempMatrix[i, 4] = -1 * sour[p, 1];
                TempMatrix[i, 5] = -1;
                TempMatrix[i, 6] = sour[p, 0] * dest[p, 1];
                TempMatrix[i, 7] = dest[p, 1] * sour[p, 1];
                TempMatrix[i, 8] = dest[p, 1];
                ++p;
            }
        }

        var finalizeMatrix = DenseMatrix.OfArray(TempMatrix);
        var svd = finalizeMatrix.Svd(true);

        return svd.VT.Row(svd.VT.RowCount - 1).ToArray();

    }

    private static double[,] ApplyInverseHomography(double[,] hm, double[,] a)
    {
        var m = DenseMatrix.OfArray(hm).Inverse();
        return HomographyApplier(m.ToArray(), a);
    }

    private static double[,] HomographyApplier(double[,] hm, double[,] a)
    {
        double[,] temp = MultiplyMatrices(hm, a);
        double[,] res = new double[,] { { temp[0, 0] / temp[2, 0] }, { temp[1, 0] / temp[2, 0] }, { 1 } };

        return res;
    }

    public static double[,] MultiplyMatrices(double[,] a, double[,] b)
    {
        int m = a.GetLength(0);
        int n = b.GetLength(1);

        double[,] res = new double[m, n];

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                res[i, j] = 0;
                for (int k = 0; k < a.GetLength(1); k++)
                {
                    res[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return res;
    }

    #endregion
}