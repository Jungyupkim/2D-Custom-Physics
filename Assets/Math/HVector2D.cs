using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HVector2D
{
    public float x, y;
    public float h;

    // this is to convert the unity vector2 to HVector2D
    // we set value of h as 1, as we need this when we use Hvector and HMatrix together
    public HVector2D(Vector2 _vec)
    {
        x = _vec.x;
        y = _vec.y;
        h = 1.0f;
    }
    // setting HVector2D object with 2 floats, _x and _y. these float values will be x and y values in the HVector2D object
    public HVector2D(float _x, float _y)
    {
        x = _x;
        y = _y;
        h = 1.0f;
    }

    // changing the + operator in a way that it can also do addition of two HVector2D
    public static HVector2D operator +(HVector2D a, HVector2D b)
    {
        // addition and subtraction is simple. (addition)new HVector2D object = (x1 + x2, y1 + y2)
        return new HVector2D(a.x + b.x, a.y + b.y);
    }

    // changing the - operator in a way that it can also do subtraction of two HVector2D
    public static HVector2D operator -(HVector2D a, HVector2D b)
    {
        //(subtraction) new HVector2D object = (x1 - x2, y1 - y2)
        return new HVector2D(a.x - b.x, a.y - b.y);
    }

    // changing the * operator in a way that it can also do multiplication of vector and a float value
    public static HVector2D operator * (HVector2D a, float scalar)
    {
        // multiplication of vector and float value is simple as well. new HVector2D object = (x * float value, y * float value)
        return new HVector2D(a.x * scalar, a.y * scalar);
    }

    // changing the / operator in a way that it can also do devision of vector and a float value
    public static HVector2D operator /(HVector2D a, float scalar)
    {
        // multiplication of vector and float value is simple as well. new HVector2D object = (x * float value, y * float value)
        return new HVector2D(a.x / scalar, a.y / scalar);
    }

    // Finding length of the vector using pytagoras theorem
    public float Magnitude()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    // normalise the vector to find out unit normal of the vector (it can be used to find direction of where you want to project vector and etc)
    public void Normalize()
    {
        // find the vector length of the vector first, then devide the x and y value of vector with the magnitude to normalise the vector
        float mag = Magnitude();
        x /= mag;
        y /= mag;
    }

    // Dot product will tell us about angle between 2 vectors. returns float value of correlation between direction vector and forward vector.
    // if the float value is 0, the vectors are perpendicular and if the value is negative, it means the vector has negative direction. Positive value means the vector has positive direction
    public float DotProduct(HVector2D vec)
    {
        // this float can be figured out with below equation
        // (x1 * x2 + y1 * y2)
        return (x * vec.x + y * vec.y);
    }

    // projection function will show the projection of direction normal found through Dotproduct function
    public HVector2D Projection(HVector2D b)
    {
        // it can be found with below equation
        //( VecA dotProduct(B) / VecB dotProduct(B) ) * Projection of A
        HVector2D proj = b * (DotProduct(b) / b.DotProduct(b));
        return proj;
    }

    // function to find the abgle between two points
    // since the dot product will tell us about angles between 2 vectors, we use this to find the angle between two points.
    // we devide the float value found through dot product of A and B (direction normal, which is going to be adjacent side of the ) by multiplication of A and Bs magnitude (which is going to be a hypotenuse)
    // then using cos rule, we use Acos to find the angle between the two points
    public float FindAngle(HVector2D vec)
    {
        return (float)Mathf.Acos(DotProduct(vec) / (Magnitude() * vec.Magnitude()));
    }

    // Converting HVector2D object to Vector2
    public Vector2 ToUnityVector2()
    {
        return new Vector2(x, y);
    }

    // Converting HVector2D object to Vector3. Z is 0 as we dont use 3D
    public Vector3 ToUnityVector3()
    {
        return new Vector3(x, y, 0);
    }

    // Printing out the HVector2D object in our console to check their x and y value
    public void Print()
    {
        Debug.Log(x + " " + y);
    }

}
