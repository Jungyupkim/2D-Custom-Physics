using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Util
{
    // Declaring one accessible function that takes in two HVector2D in its param
    // This function is to find the distance between two vector points by using pytagoras theorem.
    public static float FindDistance(HVector2D p1, HVector2D p2)
    {
        // this will return the distance found between two points after applying pytagoras theorem
        return MathF.Sqrt(((p1.x - p2.x) * (p1.x - p2.x)) + ((p1.y - p2.y) * (p1.y - p2.y)));
    }
}
