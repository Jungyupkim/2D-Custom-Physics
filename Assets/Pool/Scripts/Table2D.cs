using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Additional Feature added
// - spawning the balls in the pyramid pattern
public class Table2D : MonoBehaviour
{
    //private int maxNumBall = 15; //this variable was used to randomly generate 15 balls into the game scene
    public int row = 7;
    private float yOffset;
    private float xOffset;

    public GameObject redBall;
    public GameObject whiteBall;
    private GameObject container;
    public List<GameObject> balls;
    public List<GameObject> holes;
    //private HMatrix2D matrix = new HMatrix2D(); //this variable was used to try rotating the empty gameobject using matrices

    private GameObject table;

    private Vector2 tempVec; // You can use Vector2 just for the initialisation part in Start().

    // Use this for initialization
    void Start () 
    {
        tempVec = new Vector2();

        // finding 
        table = GameObject.Find("Table");

        // declaring empty gameobject and instantiating the object from the prefab. in this case, white ball will be instantiated, which will be used as the ball that will be controlled by the player
        GameObject wb = Instantiate(whiteBall);
        // Getting component from wb so that we have access to the variables of whiteball instantiated
        Ball2D wBall = wb.GetComponent<Ball2D>();
        // After that, add the instantiated whiteball to balls list, which will be used to check collisions with other balls in the game
        balls.Add(wb);

        // Spawning the ball in the middle of left side of the table
        HVector2D ballSpawnPoint = new HVector2D(table.transform.position.x - (table.GetComponent<SpriteRenderer>().sprite.rect.width / 4), table.transform.position.y);
        wBall.transform.position = ballSpawnPoint.ToUnityVector2();

        //below code was originally used to get a variable "Radius" from Hole2D to find coordinate to randomly locate the ball into the game
        //-----------------------------------------------
        //Hole2D hole = holes[0].GetComponent<Hole2D>();
        //-----------------------------------------------

        // setting a new Gameobject, which will be the parent object of the balls that are going to be instantiated
        // actually, this variable is unnecessary in current method of spawning balls in side pyramid shape, but I will leave it here first to show how it was used in first and second method of spawning the balls
        container = new GameObject("Container");
        
        // Third Attempt in Spawning the ball in side pyramid shape -----------------------------------------------------------------------------------------------
        // set the spawn point of the ball to the center of right side of the table.
        // get the x value of center of table first, then add the (width of table/4) to shit it towards right. y value will remain the same.
        HVector2D redBallSpawnPoint = new HVector2D(table.transform.position.x + (table.GetComponent<SpriteRenderer>().sprite.rect.width / 4), table.transform.position.y);
        // set the position of the empty gameobject container as reBallSpawnPoint which we configured just now
        container.transform.position = redBallSpawnPoint.ToUnityVector2();  
        // prepare variables to offset each lines of ball after instantiating, so that we can make it into side pyramid shape
        // values here are diameter of the ball + 1, so that the balls wont collide straight once it get insantiated
        // values for yOffset and xOffset is actually the same as diameter of sphere is the same, but I differenciated the variable for easy understanding of the code
        yOffset = (wBall.Radius * 2) + 1;
        xOffset = (wBall.Radius * 2) + 1;
        // assign new Vector2 value in tempVec for initialisation of balls. We set the position to be the
        // position to spawn the ball, which is in the middle of right part of the table
        tempVec = redBallSpawnPoint.ToUnityVector2();
        // we increase the y value of spawn point as we will later decrese the y value of the balls spawn position during initialisation
        tempVec.y += yOffset;
        // we set the i value to be 1 and loop it "row" number of times to get "row" number of rows
        for (int i = 1; i <= row; i++)                                                                               
        {                  
            // we use another for loops to set how many balls will be there in each column
            // since we are using int i as a limit, number of balls in each column will be the same as nth number of rows
            for (int j = 0; j < i; j++)                                                                              
            {    
                // first we instantiate the ball from the prefab
                // then, we reconfigure the position of tempvec, where we will spawn the balls at
                // then, we will add the newly instantiated ball into the list of gameobjects called balls
                // after instantiating
                GameObject b = Instantiate(redBall);       
                tempVec = new Vector2(tempVec.x, tempVec.y - yOffset);                                              
                b.transform.position = tempVec;                                                                      
                balls.Add(b);
            }

            // after spawning the balls in nth number of column, offset for the new row by adding x offset to move to next row, then revert back the y value of the position,
            // then add wBall.Radius to make the side pyramid shape
            tempVec = new Vector2(tempVec.x + xOffset , tempVec.y + (yOffset * i) + wBall.Radius );

            // for easier visualisation
            // offesting the tempVec without adding wBall.Radius in y value of tempVec
            // * * * * *
            //   * * * *
            //     * * *
            //       * *
            //         *
        }

        // First and Second Attempt to rotate an empty gameobject by using matrices(HMatrix2D) and Unity Rotate -------------------------------------------------------------------------------
        // First attempt of trying to rotate the empty object using matrices and meshFilter failed to do so as below method is trying to access meshfilter,
        // but since container gameobject does not have any mesh, there will also be no vertices and thus below code couldnt work.
        // but I felt likt below codes are still worth while mentioning it so I commented them out

        // Second attempt of rotating the empty gameobject after spawning balls in triangle shape was successful, but soon, I tried to spawn the balls diractly in side pyramid shape so that
        // I wouldnt need to rotate the objects to make it sideways

        /*
        // This method is very similar to the third method that I explained. but this script will print out normal pyramid shape
        // so we set the spawn point for the ball and declare variable for yOffset and xOffset
        // we set the value of yOffset to be negative this time as we will minus the y value after printing balls in each column to print pyramid shape

        HVector2D redBallSpawnPoint = new HVector2D(table.transform.position.x + (table.GetComponent<SpriteRenderer>().sprite.rect.width / 4), table.transform.position.y);
        container.transform.position = redBallSpawnPoint.ToUnityVector2();
        tempVec = redBallSpawnPoint.ToUnityVector2();
        yOffset = -(wBall.Radius * 2) - 1;
        xOffset = (wBall.Radius * 2) + 1;
        for (int i = 1; i <= row; i++)
        {
            for(int j = 0; j < i; j++)
            {
                // so once instantiating the ball, we make them child object of this empty gameobject called container
                GameObject b = Instantiate(redBall);
                tempVec = new Vector2(tempVec.x + xOffset, tempVec.y);
                b.transform.position = tempVec;
                balls.Add(b);
                b.transform.SetParent(container.transform);
            }

            // offseting for the new row
            tempVec = new Vector2(tempVec.x - (xOffset * i) - wBall.Radius, tempVec.y + yOffset);
        }
        // so once instantiating the balls, we rotate the container object along the z axis to put the pyramid shape to be side pyramid shape.
        // this is the second method that I tried to use for initialisation.
        container.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

        // these codes are for my first method of trying to initialise the balls through rotating the container object by using matrices and meshfilter.
        // the reason why it didnt work out is written above, but I will still explain what I intended to do and how it turned out

        // firstly I added MeshFilter component in container in attempt to get acces to its vertices so that I can rotate it through rotation mat
        container.AddComponent<MeshFilter>();
        MeshFilter mf = container.GetComponent<MeshFilter>();
        // then, I used this function to store all the positions of vertices in Container, then declared another list of vector3 for me to store the new position of vertices
        Vector3[] origVerts = mf.mesh.vertices;
        Vector3[] newVerts = new Vector3[origVerts.Length];
        // at this point, I was trying to figure out why this method is not working, then I figured out that there were no vertices in Container, So I was basically trying to rotate nothing.
        Debug.Log(origVerts.Length);
        // set the matrix with center point of the object I want to rotate and create a new matrix with this info
        matrix.setTranslationMat(container.transform.position.x, container.transform.position.y);
        HMatrix2D centerMatrix = matrix;
        // reset the matrix to configure other matrices
        matrix = new HMatrix2D();
        // set rotationMatrix using below function to rotate the object by 270 degree
        matrix.setRotationMat(270);
        HMatrix2D rotationMatrix = matrix;
        matrix = new HMatrix2D();
        // then set another matrix to let the object go back to its original position
        matrix.setTranslationMat(-container.transform.position.x, -container.transform.position.y);
        HMatrix2D negCenterMatrix = matrix;

        // multiply all the matrices to come out with one rotationMat
        HMatrix2D rotTrans = centerMatrix * rotationMatrix * negCenterMatrix;
        
        // then, using for loop, I tried to multiply all the vertices with rotationMatrix and the position of the vertices
        int k = 0;
        while (k < origVerts.Length)
        {
            newVerts[k] = (rotTrans * new HVector2D(newVerts[k].x, newVerts[k].y)).ToUnityVector3();
            k++;
        }
        //Then I declared those new vertices to be the vertices of container 
        mf.mesh.vertices = newVerts;
        rotationMatrix.Print();
        */
        //--------------------------------------------------------------------------------------------------------------------------------------------------

        // Generaring 15 random balls into the game scene without letting them overlap ---------------------------------------------------------------------
        /*
        // using for loop to instantiate maxNumBall amount of balls into the game scene 
        for (int i = 0; i < maxNumBall; i++) 
        {
            // Create one new ball by using instantiate function
            GameObject b = Instantiate(redBall);
            // then get Ball2D component of instantiated object to get access to their variables and functions
            Ball2D ball = b.GetComponent<Ball2D>();
            // the,add the newly instantiated ball object into list of gameObject
            balls.Add(b);

            // below code is to reposition the ball into random position within the set parameter 
            // do while loop will break once the checkBallCollision returns false. As such, below code will continue to relocate the popsition of spawned ball if the ball is colliding with other ball when spawned
            do
            {
                // declaring vector value of tempVec to be the position of the ball
                tempVec = b.transform.position;
                
                // set the minimum value of x and y to be the edge of the table(left and bottom wall) + width of the wall + balls diameter + radius of the ball 
                // and maximum value of x and y to be the edge of the table(top and right wall) - width of the wall - balls diameter - radius of the ball
                // so that the ball wont collide with the wall and drop into the hole right after the ball gets instantiated
                tempVec.x = Random.Range(table.transform.position.x - (table.GetComponent<SpriteRenderer>().sprite.rect.width / 2) + (ball.wallOffset * 2) + (ball.Radius * 2) + hole.Radius,
                    table.transform.position.x + (table.GetComponent<SpriteRenderer>().sprite.rect.width / 2) - (ball.wallOffset * 2) - (ball.Radius * 2) - hole.Radius);
                Debug.Log(ball.Radius);
                tempVec.y = Random.Range(table.transform.position.y - (table.GetComponent<SpriteRenderer>().sprite.rect.height / 2) + (ball.wallOffset * 2) + (ball.Radius * 2) + hole.Radius,
                    table.transform.position.y + (table.GetComponent<SpriteRenderer>().sprite.rect.height / 2) - (ball.wallOffset * 2) - (ball.Radius * 2) - hole.Radius);
                
                // after that, once getting the random value of x and y coordinate, set the balls position to be the random coodinate generated
                b.transform.position = tempVec;
                ball.Position.x = tempVec.x;          
                ball.Position.y = tempVec.y;

            } while (CheckBallCollision(ball));

        }
        */
        //---------------------------------------------------------------------------------------------------------------------------------------------------
    }
    // Update is called once per frame
    void Update () 
    {
        // use for loop to check all the balls in the list
        for(int i = 0; i < balls.Count; i++)     
        {  
            // devlaring a gameobject b1, which is the gameobject that is being detected by for loop at the moment 
            GameObject b1 = balls[i];
            // then, declare Ball2D object for us to get variables from b1
            Ball2D ball = b1.GetComponent<Ball2D>();

            // updating the balls velocity and its position once the balls in the list go through this for loop
            ball.UpdateBall2DPhysics(Time.deltaTime);
            
            // then, use another for loop to check if the current ball in ball[i] is interacting with other balls in the list
            for (int j = 0; j < balls.Count; j++)
            {
                // declare another gameobject b2, which we will check if b1 and b2 is colliding
                GameObject b2 = balls[j];
                // then, declare Ball2D object for us to get variables from b2
                Ball2D ball2 = b2.GetComponent<Ball2D>();

                // this is to compare the b1 with other balls iun the list, except for itself. (this is because second for loop will also detect the b1)
                // if the ball and ball2 is not the same ball in the list, (b1 != b2)
                if (ball != ball2)
                {
                    // using isCollidingWith function from ball2D.cs, we check if the ball is colliginh with ball2 
                    if (ball.IsCollidingWith(ball2))
                    {
                        // if the ball is colliding with baall2, execute this function to calculate the velocity of the ball and change the velocity and position of the balls respectively
                        HandleBallCollision(ball, ball2);
                        Debug.Log("ballz hurt");
                    }
                }
            }
            
            // this loop is to compare the ball and the holes in the list
            for (int k = 0; k < holes.Count; k++)
            {
                // declaring Hole2D object to get its variable like its position
                Hole2D hole = holes[k].GetComponent<Hole2D>();

                // using isinside function from ball2d script to check if the ball is inside the hole
                if (ball.IsInside(hole))
                {
                    // if the ball is inside the hole but the ball is player's ball
                    // we detect if the ball had player tag as I added player tag into player balls prefab
                    if (ball.CompareTag("Player"))
                    {
                        // revert the balls position to be its spawn point
                        ball.transform.position = new HVector2D(table.transform.position.x - (table.GetComponent<SpriteRenderer>().sprite.rect.width / 4), table.transform.position.y).ToUnityVector3();
                        // set the velocity of the ball to be 0, so that the ball wont move once it get "spawned" back
                        ball.Velocity = new HVector2D(0, 0);
                        break;
                    }
                    else
                    {
                        // if the ball that fell into the hole is not the players ball, deactivate the object from the scene 
                        b1.SetActive(false);
                        break;
                    }                       
                }
            }
        }
    }
    // this function is used to check if the balls are colliding when it spawned
    // it will return false only when the ball is not colliding so that it can break out from the do while loop in the start
    /*
    bool CheckBallCollision(Ball2D toCheck)
    {
        // using for loop to check the ball with all the balls in the ball list
        for (int i = 0; i < balls.Count; i++)
        {
            // Create one new ball
            GameObject b = balls[i];
            Ball2D ball = b.GetComponent<Ball2D>();

            if(ball.IsCollidingWith(toCheck) && toCheck != ball)
            {
               return true;
            }
        }

        return false;
    }
    */

    // this script is to set the velocity of the two colliding balls to simulate the balls getting "bounced" off
    void HandleBallCollision(Ball2D ball1, Ball2D ball2)
    {
        // find the vector between collided balls 
        HVector2D collisionNormal = ball1.Position - ball2.Position;
        // declare another HVector2D to store this vector and normalise it to get the direction of this vector
        HVector2D unitNormal = collisionNormal;
        unitNormal.Normalize();
        // find the overlapped distance between two balls by adding their radius and subtracting the length between the center point of two balls
        float overlapDistance = ball1.Radius + ball2.Radius - collisionNormal.Magnitude();

        // once the overlapped distance is found, use below function to adjust the position of the ball to right before the two balls get collided/sprites getting clipped
        AdjustBallsDistance(ball1, ball2, overlapDistance);

        // find the average Coefficient of Restitution, but for this project, we will assume that this value will be 1 and thus wont lose any energy when the balls collide with other balls
        float averageCER = 1f; //(ball1.CoeffRestitution + ball2.CoeffRestitution) / 2f;
        // find the vector length/magnitude of v1 projection and v2 projection. we will later multiply this to unit normal to get the vector of each projection.
        float v1Proj = ball1.Velocity.DotProduct(unitNormal);
        float v2Proj = ball2.Velocity.DotProduct(unitNormal);

        // then, once we figured out v1 and v2 projection, use below math equation to find the value(vector length/magnitude) of v1 and v2 prime projc
        float v1PrimeProj = (((ball1.Mass - (averageCER * ball2.Mass)) * v1Proj) + ((1 + averageCER) * ball2.Mass * v2Proj)) / (ball1.Mass + ball2.Mass);
        float v2PrimeProj = (((ball2.Mass - (averageCER * ball1.Mass)) * v2Proj) + ((1 + averageCER) * ball1.Mass * v1Proj)) / (ball1.Mass + ball2.Mass);

        // finally find the vector of each projection by muliplying the configured magnitude of each projections
        HVector2D v1ProjVec = unitNormal * v1Proj;
        HVector2D v1PrimeProjVec = unitNormal * v1PrimeProj;
        HVector2D v2ProjVec = unitNormal * v2Proj;
        HVector2D v2PrimeProjVec = unitNormal * v2PrimeProj;

        // then use below equation to find the velocity of each balls
        ball1.Velocity = (ball1.Velocity + v1PrimeProjVec - v1ProjVec);
        ball2.Velocity = (ball2.Velocity + v2PrimeProjVec - v2ProjVec);
    }

    // this function is to relocate the colliding balls back to the position where they collide/clip tgt
    void AdjustBallsDistance(Ball2D ball1, Ball2D ball2, float overlapDistance)
    {
        // find the distance for ball1 to moveback. we dont need to clculate for ball2, as ball2 will also execute the same function to find its distance to move back
        // this is because all the balls in the list is continuously checking for collision in the update using for loop
        // this distance can be found by Magnitude of Velocity of ball1/(Magnitude of Velocity of ball1 + Magnitude of Velocity of ball2) * overlapped distance we found just now
        float distanceToMoveBack = ball1.Velocity.Magnitude() / (ball1.Velocity.Magnitude() + ball2.Velocity.Magnitude()) * overlapDistance;
        
        // then find the time taken to move back the balls by using the blow equation
        // distance to moveback / Magnitude of Velocity of ball1
        float Time = distanceToMoveBack / ball1.Velocity.Magnitude();
        // declare another HVector2D object to store displacement of ball1(Velocity of ball1 * time)
        HVector2D displacement = ball1.Velocity * Time;
        // if the distance to moveback is 0,
        if (distanceToMoveBack > 0)
        {
            // move back the ball1 to undo the collision of ball1 and ball2
            // after that, we will apply pysics to simulate bouncing balls
            ball1.Position -= displacement;
        }

    }
}