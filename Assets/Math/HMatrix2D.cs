using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMatrix2D
{
    public float[,] Entries { get; set; } = new float[3, 3];

    // creating an default Matrix
    // 1 0 0
    // 0 1 0
    // 0 0 1
    public HMatrix2D()
    {
        setIdentity();
    }

    // converting the nested list to HMatrix object using for loop
    // y is for rows and x is for columns
    public HMatrix2D(float[,] multiArray)
    {

        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
            {
                Entries[x, y] = multiArray[x, y];
            }         
    }

    // converting multiple float values into matrix (only accepts 9 floats as we are using 3 by 3 matrix)
    public HMatrix2D(float m00, float m01, float m02,
             float m10, float m11, float m12,
             float m20, float m21, float m22)
    {

        Entries[0, 0] = m00;
        Entries[0, 1] = m01;
        Entries[0, 2] = m02;

        Entries[1, 0] = m10;
        Entries[1, 1] = m11;
        Entries[1, 2] = m12;

        Entries[2, 0] = m20;
        Entries[2, 1] = m21;
        Entries[2, 2] = m22;
    }

    public static HMatrix2D operator + (HMatrix2D left, HMatrix2D right)
    {
        // adding the respective values in the matrices
        return new HMatrix2D(
            left.Entries[0, 0] + right.Entries[0, 0],
            left.Entries[0, 1] + right.Entries[0, 1],
            left.Entries[0, 2] + right.Entries[0, 2],
            left.Entries[1, 0] + right.Entries[1, 0],
            left.Entries[1, 1] + right.Entries[1, 1],
            left.Entries[1, 2] + right.Entries[1, 2],
            left.Entries[2, 0] + right.Entries[2, 0],
            left.Entries[2, 1] + right.Entries[2, 1],
            left.Entries[2, 2] + right.Entries[2, 2]
            );
    }

    public static HMatrix2D operator - (HMatrix2D left, HMatrix2D right)
    {
        // subtracting the respective values in the matrices
        return new HMatrix2D(
            left.Entries[0, 0] - right.Entries[0, 0],
            left.Entries[0, 1] - right.Entries[0, 1],
            left.Entries[0, 2] - right.Entries[0, 2],
            left.Entries[1, 0] - right.Entries[1, 0],
            left.Entries[1, 1] - right.Entries[1, 1],
            left.Entries[1, 2] - right.Entries[1, 2],
            left.Entries[2, 0] - right.Entries[2, 0],
            left.Entries[2, 1] - right.Entries[2, 1],
            left.Entries[2, 2] - right.Entries[2, 2]
            );
    }

    public static HMatrix2D operator * (HMatrix2D left, float scalar)
    {
        // multiplying each values in the matrix by float value
        return new HMatrix2D(
            left.Entries[0, 0] * scalar,
            left.Entries[0, 1] * scalar,
            left.Entries[0, 2] * scalar,
            left.Entries[1, 0] * scalar,
            left.Entries[1, 1] * scalar,
            left.Entries[1, 2] * scalar,
            left.Entries[2, 0] * scalar,
            left.Entries[2, 1] * scalar,
            left.Entries[2, 2] * scalar
            );
    }

    public static HMatrix2D operator * (HMatrix2D left, HMatrix2D right)
    {
        return new HMatrix2D
        (
            /*
            00 01 02    00 xx xx
            xx xx xx    10 xx xx
            xx xx xx    20 xx xx
            */
            left.Entries[0, 0] * right.Entries[0, 0] + left.Entries[0, 1] * right.Entries[1, 0] + left.Entries[0, 2] * right.Entries[2, 0],

            /*
            00 01 02    xx 01 xx
            xx xx xx    xx 11 xx
            xx xx xx    xx 21 xx
            */
            left.Entries[0, 0] * right.Entries[0, 1] + left.Entries[0, 1] * right.Entries[1, 1] + left.Entries[0, 2] * right.Entries[2, 1],

            /*
            00 01 02    xx xx 02
            xx xx xx    xx xx 12
            xx xx xx    xx xx 22
            */
            left.Entries[0, 0] * right.Entries[0, 2] + left.Entries[0, 1] * right.Entries[1, 2] + left.Entries[0, 2] * right.Entries[2, 2],

            /*
            xx xx xx    00 xx xx
            10 11 12    10 xx xx
            xx xx xx    20 xx xx
            */
            left.Entries[1, 0] * right.Entries[0, 0] + left.Entries[1, 1] * right.Entries[1, 0] + left.Entries[1, 2] * right.Entries[2, 0],

            /*
            xx xx xx    xx 01 xx
            10 11 12    xx 11 xx
            xx xx xx    xx 21 xx
            */
            left.Entries[1, 0] * right.Entries[0, 1] + left.Entries[1, 1] * right.Entries[1, 1] + left.Entries[1, 2] * right.Entries[2, 1],

            /*
            xx xx xx    xx xx 02
            10 11 12    xx xx 12
            xx xx xx    xx xx 22
            */
            left.Entries[1, 0] * right.Entries[0, 2] + left.Entries[1, 1] * right.Entries[1, 2] + left.Entries[1, 2] * right.Entries[2, 2],

            /*
            xx xx xx    00 xx xx
            xx xx xx    10 xx xx
            02 12 22    20 xx xx
            */
            left.Entries[2, 0] * right.Entries[0, 0] + left.Entries[2, 1] * right.Entries[1, 0] + left.Entries[2, 2] * right.Entries[2, 0],

            /*
            xx xx xx    xx 10 xx
            xx xx xx    xx 11 xx
            02 12 22    xx 12 xx
            */
            left.Entries[2, 0] * right.Entries[0, 1] + left.Entries[2, 1] * right.Entries[1, 1] + left.Entries[2, 2] * right.Entries[2, 1],

            /*
            xx xx xx    xx xx 02
            xx xx xx    xx xx 12
            02 12 22    xx xx 22
            */
            left.Entries[2, 0] * right.Entries[0, 2] + left.Entries[2, 1] * right.Entries[1, 2] + left.Entries[2, 2] * right.Entries[2, 2]
        );
    }

    // this function is to multiply matrix and vector (3 by 3 Matrix * 3 by 1 Matrix)
    // but for this, since we only need x an y value to make a HVector, we are doing 2 by 2 Matrix * 2 by 1 Matrix to get new HVector2D object
    public static HVector2D operator * (HMatrix2D left, HVector2D right)
    {
        return new HVector2D
        (
           left.Entries[0 ,0] * right.x + left.Entries[0, 1] * right.y + left.Entries[0 ,2] * right.h,
           left.Entries[1, 0] * right.x + left.Entries[1, 1] * right.y + left.Entries[1, 2] * right.h
        );
    }

    public static bool operator == (HMatrix2D left, HMatrix2D right)
    {
        // using two for loop to check if every element in the matrix is the same
        // in order for matrix to be equal, they must have same values in each row and columns
        for (int y = 0; y < left.Entries.Length; y++)
            for (int x = 0; x < left.Entries[y, x]; x++)
                if (left.Entries[x, y] != right.Entries[x, y])
                    return false;
        return true;

        /*
        for (int y = 0; y < left.Entries.Length; y++)
        {
            for (int x = 0; x < left.Entries[y, x]; x++)
            {
                if (left.Entries[x,y] != right.Entries[x,y])
                {
                    return false;
                }
            }
        }
        return true;
        */
    }

    public static bool operator != (HMatrix2D left, HMatrix2D right)
    {
        // this is reverse of equal function. if the matrix is the same, return false, and if the matrix is not the same, return true
        for (int y = 0; y < left.Entries.Length; y++)
            for (int x = 0; x < left.Entries[y, x]; x++)
                if (left.Entries[x, y] != right.Entries[x, y])
                    return true;
        return false;

        /*
        for (int y = 0; y < left.Entries.Length; y++)
        {
            for (int x = 0; x < left.Entries[y, x]; x++)
            {
                if (left.Entries[x, y] != right.Entries[x, y])
                {
                    return true;
                }
            }
        }
        return false;
        */
    }
    
    
    // overriding the virtual method Equals
    public override bool Equals(object obj)
    {
        // of the obj in the param is null, return false as theres niothing to compare
        if (obj == null)
            return false;
        // if the object that you are trying to compare is not the same, return false (we alrdy configured != operator to compare matrices)
        if (GetType() != obj.GetType())
            return false;
        // if the object that you are trying to compare is the same, return true, and if not false (we alrdy configured == operator to compare matrices and return true and false respectively)
        return GetType() == obj.GetType();

    }

    // dictionary will use getHashCode to quickly get a set of results from the collections and verify equality of by using equals
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    

    // this will transpose the matrix (make it lie down and flip in a sense)
    public HMatrix2D transpose()
    {
        // 00 01 02     00 10 20
        // 10 11 12 --> 01 11 21
        // 20 21 22     02 12 22
        
        return new HMatrix2D(
        Entries[0, 0] = Entries[0, 0],
        Entries[0, 1] = Entries[1, 0],
        Entries[0, 2] = Entries[2, 0],

        Entries[1, 0] = Entries[0, 1],
        Entries[1, 1] = Entries[1, 1],
        Entries[1, 2] = Entries[2, 1],

        Entries[2, 0] = Entries[0, 2],
        Entries[2, 1] = Entries[1, 2],
        Entries[2, 2] = Entries[2, 2]
        );
    }

    // this function will multiply the values in matrices diagonally in a sense and add add or subtract those values to get one float value
    public float getDeterminant()
    {
        // a b c | a b c
        // d e f | d e f
        // g h i | g h i

        // aei + bfg + cdh - afh - bdi - ceg
        return(
           Entries[0, 0] * Entries[1, 1] * Entries[2, 2] +
           Entries[0, 1] * Entries[1, 2] * Entries[2, 0] +
           Entries[0, 2] * Entries[1, 0] * Entries[2, 1] -
           Entries[0, 0] * Entries[1, 2] * Entries[2, 1] -
           Entries[0, 1] * Entries[1, 0] * Entries[2, 2] -
           Entries[0, 2] * Entries[1, 1] * Entries[2, 0]
           );
    }
    
    public void setIdentity()
    {
        // setting default matrix of
        // 1 0 0
        // 0 1 0
        // 0 0 1
        // using two for loops. 00, 11, 22 will be set to 1 and the rest will be 0
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                Entries[y, x] = x == y ? 1 : 0;
        /*
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (x == y)
                {
                    Entries[y, x] = 1;
                }
                else
                {
                    Entries[y, x] = 0;
                }
            }
        }
      */
    }

    public void setTranslationMat(float transX, float transY)
    {
        // setting the matrix in this way to multiply with vector to get a new position to shift the object/points
        // 1 0 dx
        // 0 1 dy
        // 0 0 1
        setIdentity();
        Entries[0, 2] = transX;
        Entries[1, 2] = transY; 
    }

    public void setRotationMat(float rotDeg)
    {
        // multiply with other matrix and vectors to translate, rotate, then revert back to its original position
        // cos() -sin() 0
        // sin() -cos() 0
        //  0      0    1
        setIdentity();
        float rad = rotDeg * Mathf.Deg2Rad;
        Entries[0, 0] = Mathf.Cos(rad);
        Entries[0, 1] = -Mathf.Sin(rad);
        Entries[1, 0] = Mathf.Sin(rad);
        Entries[1, 1] = -Mathf.Cos(rad);
    }

    public void setScalingMat(float scaleX, float scaleY)
    {
        // matrix use to scale up or down the sprites/other points
        // x 0 0
        // 0 y 0
        // 0 0 1
        setIdentity();
        Entries[0, 0] = scaleX;
        Entries[1, 1] = scaleY;
    }

    public void Print()
    {
        // printing out each rows and columns of values in matrices into the console using two for loops
        string result = "";
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                result += Entries[r, c] + "  ";
            }
            result += "\n";
        }
        Debug.Log(result);
    }

}
