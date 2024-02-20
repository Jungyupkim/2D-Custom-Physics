using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2DCollision : MonoBehaviour
{
    [Range(0f, 1f)]
    public float CoeffRestitution = 1f;
    public float Mass = 1f;
    public float Radius { get; private set; }

    // Let the ball have an initial velocity set in the Inspector.
    //
    public Vector2 Velocity = Vector3.zero;

    void Start()
    {
        Radius = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
    }
    public bool IsCollidingWith(Wall2DCollision other)
    {
        Vector2 lineToPointVec = other.bottom.position - transform.position;

        float proj = Vector2.Dot(lineToPointVec, other.wallVec.normalized);

        float distanceFromLine = Mathf.Sqrt(lineToPointVec.SqrMagnitude() - Mathf.Pow(proj, 2));

        return distanceFromLine <= Radius * 1.25f;
    }

    public bool IsCollidingWith(Ball2DCollision other)
    {
        Vector3 dirVec = transform.position - other.transform.position;
        return dirVec.magnitude < Radius + other.Radius;
    }

    void Update()
    {
        float displacementX = Velocity.x * Time.deltaTime;
        float displacementY = Velocity.y * Time.deltaTime;

        transform.Translate(new Vector2(displacementX, displacementY));
    }
}
