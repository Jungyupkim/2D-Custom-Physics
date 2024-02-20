using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorExercises : MonoBehaviour
{
    [SerializeField] LineFactory lineFactory;
    [SerializeField] HVector2D hVector2D;

    private Line drawnLine;

    private Vector2 startPt;
    private Vector2 endPt;

    private float GameHeight;
    private float GameWidth;
    private float maxX;
    private float maxY;
    // Start is called before the first frame update
    void Start()
    {
        CalculateGameDimensions();
        //Question2a();
        //Question2b(20);
        //Question2d();
        //Question2e(20);
        //Question3a();
        //Question3b();
        Question3c();
    }

    public void CalculateGameDimensions()
    {
        GameHeight = Camera.main.orthographicSize * 2f;
        GameWidth = Camera.main.aspect * GameHeight;

        maxX = GameWidth / 2;
        maxY = GameHeight / 2;
    }

    void Question2a()
    {
        startPt = new Vector2(0, 0);
        endPt = new Vector2(2, 3);

        drawnLine = lineFactory.GetLine(startPt, endPt, 0.02f, Color.black);

        drawnLine.EnableDrawing(true);

        Vector2 vec2 = endPt - startPt;
        Debug.Log("Magnitude = " + vec2.magnitude);
    }

    void Question2b(int n)
    {
        for (int i = 0; i < n; i++)
        {
            startPt = new Vector2(Random.Range(-maxX, maxX), 0);
            endPt = new Vector2(0, Random.Range(-maxY, maxY));

            drawnLine = lineFactory.GetLine(startPt, endPt, 0.02f, Color.black);
            drawnLine.EnableDrawing(true);
        }

    }

    void Question2d()
    {
        DebugExtension.DebugArrow(
            new Vector3(0, 0, 0),
            new Vector3(5, 5, 0),
            Color.red,
            60f);
    }

    void Question2e(int n)
    {
        for (int i = 0; i < n; i++)
        {
            DebugExtension.DebugArrow(
            new Vector3(0, 0, 0),
            new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), Random.Range(-maxY, maxY)),
            Color.red,
            60f);
        }

    }

    void Question3a()
    {   /*
        HVector2D a = new HVector2D(3f, 5f); 
        HVector2D b = new HVector2D(-4f, 2f);
        HVector2D c = a + b;

        DebugExtension.DebugArrow(new Vector2(0, 0), new Vector2(a.x, a.y), Color.red, 60f);
        DebugExtension.DebugArrow(new Vector2(0, 0), new Vector2(b.x, b.y), Color.green, 60f);
        DebugExtension.DebugArrow(new Vector2(0, 0), new Vector2(c.x, c.y), Color.white, 60f);
        DebugExtension.DebugArrow(new Vector2(b.x, b.y), new Vector2(c.x, c.y), Color.white, 60f);
        */

        HVector2D a = new HVector2D(3f, 5f);
        HVector2D b = new HVector2D(-4f, 2f);
        HVector2D c = a - b;

        DebugExtension.DebugArrow(new Vector2(0, 0), new Vector2(a.x, a.y), Color.red, 60f);
        DebugExtension.DebugArrow(new Vector2(0, 0), new Vector2(b.x, b.y), Color.green, 60f);
        DebugExtension.DebugArrow(new Vector2(0, 0), new Vector2(c.x, c.y), Color.white, 60f);
        DebugExtension.DebugArrow(new Vector2(a.x, a.y), new Vector2(-b.x, -b.y), Color.green, 60f);

        Debug.Log("Magnitude of a: "+ Mathf.Round(a.Magnitude() * 100.0f) * 0.01);
        Debug.Log("Magnitude of b: " + Mathf.Round(b.Magnitude() * 100.0f) * 0.01);
        Debug.Log("Magnitude of c: " + Mathf.Round(c.Magnitude() * 100.0f) * 0.01);

    }

    void Question3b()
    {   /*
        HVector2D a = new HVector2D(3f, 5f);
        HVector2D b = a * 2;

        DebugExtension.DebugArrow(Vector2.zero, new Vector2(a.x, a.y), Color.red, 60f);
        DebugExtension.DebugArrow(new Vector2(1, 0), new Vector2(b.x + 1, b.y), Color.green, 60f);
        */
        HVector2D a = new HVector2D(3f, 5f);
        HVector2D b = a / 2;

        DebugExtension.DebugArrow(Vector2.zero, new Vector2(a.x, a.y), Color.red, 60f);
        DebugExtension.DebugArrow(new Vector2(1, 0), new Vector2(b.x + 1, b.y), Color.green, 60f);
    }

    void Question3c()
    {
        HVector2D a = new HVector2D(3f, 5f);
        DebugExtension.DebugArrow(Vector2.zero, new Vector2(a.x, a.y), Color.red, 60f);
        a.Normalize();
        DebugExtension.DebugArrow(new Vector2(1, 0), new Vector2(a.x, a.y), Color.green, 60f);
        Debug.Log("Magnitude of a: " + Mathf.Round(a.Magnitude() * 100.0f) * 0.01);
    }
}
