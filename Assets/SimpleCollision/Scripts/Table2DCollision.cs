using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table2DCollision : MonoBehaviour
{
    // A very simple demo class that requires the two balls to be set in the Inspector.
    //
    public GameObject white;
    public GameObject red;
    public Wall2DCollision wall;

    // References to the Ball2D script attached to each ball gameobject.
    //
    Ball2DCollision whiteBall;
    Ball2DCollision redBall;



    void Start()
    {
        whiteBall = white.GetComponent<Ball2DCollision>();
        redBall = red.GetComponent<Ball2DCollision>();
    }

    void Update()
    {
        if (whiteBall.IsCollidingWith(wall) || redBall.IsCollidingWith(wall))
        {
            float projLen = Vector2.Dot(-whiteBall.Velocity, wall.normalVec);
            Vector2 p = wall.normalVec * projLen;

            Vector2 vf = (2 * p) + whiteBall.Velocity;

            whiteBall.Velocity = vf;
            Debug.Log(p);
        }

        if (whiteBall.IsCollidingWith(redBall))
        {
            Vector2 collisionNormal = whiteBall.transform.position - redBall.transform.position;
            Vector2 unitNormal = collisionNormal.normalized;

            float overlapDistance = whiteBall.Radius + redBall.Radius - collisionNormal.magnitude;
            float distanceToMoveBack = (whiteBall.Velocity.magnitude / (whiteBall.Velocity.magnitude + redBall.Velocity.magnitude)) * overlapDistance;

            float Time = distanceToMoveBack / whiteBall.Velocity.magnitude;
            Vector2 displacement = whiteBall.Velocity * Time;

            if (distanceToMoveBack != 0)
            {
                whiteBall.transform.position = (Vector2)whiteBall.transform.position - displacement;
            }

            Vector2 collisionNormal2 = redBall.transform.position - whiteBall.transform.position;
            Vector2 unitNormal2 = collisionNormal2.normalized;

            float overlapDistance2 = whiteBall.Radius + redBall.Radius - collisionNormal2.magnitude;
            float distanceToMoveBack2 = (redBall.Velocity.magnitude / (whiteBall.Velocity.magnitude + redBall.Velocity.magnitude)) * overlapDistance2;

            float Time2 = distanceToMoveBack2 / redBall.Velocity.magnitude;
            Vector2 displacement2 = redBall.Velocity * Time2;

            if (distanceToMoveBack2 != 0)
            {
                redBall.transform.position = (Vector2)redBall.transform.position - displacement2;
            }



            float averageCER = (whiteBall.CoeffRestitution + redBall.CoeffRestitution) / 2f;

            float v1Proj = Vector3.Dot(whiteBall.Velocity.normalized, unitNormal);
            float v2Proj = Vector3.Dot(redBall.Velocity.normalized, unitNormal);

            float v1PrimeProj = (((whiteBall.Mass - (averageCER * redBall.Mass)) * v1Proj) + ((1 + averageCER) * redBall.Mass * v2Proj)) / (whiteBall.Mass + redBall.Mass);
            float v2PrimeProj = (((redBall.Mass - (averageCER * whiteBall.Mass)) * v2Proj) + ((1 + averageCER) * whiteBall.Mass * v1Proj)) / (whiteBall.Mass + redBall.Mass);

            Vector2 v1ProjVec = v1Proj * collisionNormal;
            Vector2 v1PrimeProjVec = v1PrimeProj * collisionNormal;
            Vector2 v2ProjVec = v2Proj * collisionNormal;
            Vector2 v2PrimeProjVec = v2PrimeProj * collisionNormal;

            whiteBall.Velocity = whiteBall.Velocity + v1PrimeProjVec - v1ProjVec;
            redBall.Velocity = redBall.Velocity + v2PrimeProjVec - v2ProjVec;
        }


        /*
        if (whiteBall.IsCollidingWith(wall) || redBall.IsCollidingWith(wall))
        {
            Debug.Log("collision");
        }
        */

        //// This is a very simple scene with just two balls. 
        ////
        //if (whiteBall.IsCollidingWith(redBall))
        //{
        //    // Your code to set the ball velocities after two balls ahve collided.
        //}
    }
}
