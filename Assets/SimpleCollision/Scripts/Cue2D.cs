using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This uses the LineDraw component to draw a line indicating the direction and
// magnitude to move the white ball.

public class Cue2D : MonoBehaviour
{
    public GameObject cueBall;
    public LineFactory lineFactory;

    // Just to speed up the ball, or a very long line needs to be drawn.
    //
    public float speedFactor = 5f;

    private Line drawnLine;
    private Vector2 pos;

    // The cueBall's Ball2D script
    //
    Ball2DCollision ball2D; 

    void Start()
    {
        ball2D = cueBall.GetComponent<Ball2DCollision>();
    }

    void Update()
    {
        // You should have done this in your Dynamics worksheet. You can copy
        // and paste all your code into this Cue2D file, or try to complete 
        // the code below. (If you did the Dynamics worksheet, copying your own
        // code from there should be easier!)

        //if (Input.GetMouseButtonDown(0))
        //{
        //    pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    float distance = ;

        //    if (distance > )
        //    {
        //        return;
        //    }

        //    if (ball != null)
        //    {
        //        drawnLine = ;
        //        drawnLine.EnableDrawing(true);
        //    }
        //}
        //else if (Input.GetMouseButtonUp(0) && drawnLine != null)
        //{
        //    drawnLine.EnableDrawing(false);
        //    Vector2 velocity = new Vector2(???);

        //    // This is the physics part.
        //    //
        //    ball.Velocity = ;
        //    drawnLine = null;
        //}

        //if (drawnLine != null)
        //{
        //    drawnLine.end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Mathf.Sqrt(Mathf.Pow((cueBall.transform.position.x - startLinePos.x), 2) + Mathf.Pow((cueBall.transform.position.y - startLinePos.y), 2));

            if (distance >= ball2D.Radius)
            {
                Debug.Log(distance);
                return;
            }
                
            if (ball2D != null)
            {
                drawnLine = lineFactory.GetLine(cueBall.transform.position, startLinePos, 0.2f, Color.black);
                drawnLine.EnableDrawing(true);
            }
            
        }

        else if (Input.GetMouseButtonUp(0) && drawnLine != null)
        {
            drawnLine.EnableDrawing(false);
            HVector2D v = new HVector2D(drawnLine.start - drawnLine.end);
            ball2D.Velocity = v.ToUnityVector2();
            drawnLine = null;
        }

        if (drawnLine != null)
        {
            drawnLine.end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void Clear()
    {
        var activeLines = lineFactory.GetActive();

        foreach (var line in activeLines)
        {
            line.gameObject.SetActive(false);
        }
    }
}
