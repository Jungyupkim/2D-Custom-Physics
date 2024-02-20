using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// Additional Feature added
// - Capped Velocity of the ball using magnitude of the Velocity

public class Ball2D : MonoBehaviour
{
    // Variable used for Debugging
    private Vector3 lastpos;
    // ---------------------------

    [Range(0f, 1f)] //this is to restrict the below variables to have minimum/maximum value of 0 and 1. e.g. value of CoeffRestritution can go below 0 or go above 1
    public float CoeffRestitution = 1f;
    public float Mass = 1f;
    private float friction = 0.997f; // the higher the value of friction, the friction applied on the ball will decrease

    public HVector2D Position = new HVector2D(0, 0);
    public HVector2D Velocity = new HVector2D(0, 0);

    private HMatrix2D matrix = new HMatrix2D();

    public float Radius;

    public float ballTop, ballBottom, ballLeft, ballRight;
    private GameObject leftWall, rightWall, topWall, bottomWall;
    public float wallOffset;

    // function that checks if the ball is colliding with certain points and returning true or false depending on the position of the ball. It takes in two float values in its param
    public bool IsCollidingWith(float x, float y)
    {
        // declaring new vector point with the float values in the param
        HVector2D colliding = new HVector2D(x, y);
        // declaring vector point of the ball object
        HVector2D ball = new HVector2D(Position.x, Position.y);
        // using FindDistance function from Util.cs to find the distance between two points. (FindDistance uses pytagoras theorum to find the distance)
        float distance = Util.FindDistance(colliding, ball);
        // if the distance between the two points are smaller than the radius of the ball, it means the point is within the balls sprite, and thus colliding with the point.
        return distance <= Radius;
    }

    // function that checks if the ball is colliding with other balls and returning true or false depending on the position of the ball. It takes another ball object in its param
    public bool IsCollidingWith(Ball2D ball)
    {
        // declaring vector to find the vector between ball position and other balls position. (vector between two points can be found by minusing position of ball1 from position of ball2)
        // in this case, the dirVec is directing from ball to other balls position
        HVector2D dirVec = new HVector2D(transform.position - ball.transform.position);

        // if the vector length(vector length can be found by finding magnitude of the vector) is smaller than the sum of radius of two balls, that means the sprite of two balls are colliding
        // as such, it will return true if the length of vector is smaller than the sum of radiuses
        return dirVec.Magnitude() <= Radius + ball.Radius;
    }

    // function that checks if the ball is inside the hole and returning true or false depending on the position of the ball. It takes hole object in its param
    public bool IsInside(Hole2D hole)
    {
        // similar to IscollidingWith(Ball2D ball) function, this code is to find the vector from ball position to hole position.
        HVector2D dirVec = new HVector2D(transform.position - hole.transform.position);

        // but for this case, we will only retuen true if the vector length is smaller than the radius of the hole.
        // this is becuase the ball is not counted as "fallen" into the hole as long as the center point of the ball dont go past the hole edge.
        return dirVec.Magnitude() < hole.Radius;
    }

    // function to update the ball physics like velocity of the ball and the position of the ball
    public void UpdateBall2DPhysics(float deltaTime)
    {
        // if the balls "speed" (magnitude of the velocity) is more than 1500, it will fix the balls velocity to be 1500
        if(Velocity.Magnitude() >= 1500)
        {
            // normalise the velocity vector, then multiply it by 1500 to set the magnitude of the velocity to be 1500.
            Velocity.Normalize();
            Velocity = Velocity * 1500;
        }
        // below code is to simulate the friction along the surface. As the ball physics gets updated, it will lose 0.3 percent of its velocity (set value for friction is 0.997f for this project)
        Velocity *= friction; //Velocity = Velocity * 0.997f

        // finding displacement of the ball object, which will be used inside translate matrices.
        // displacement can be found by velocity * time
        float displacementX = Velocity.x * deltaTime;
        float displacementY = Velocity.y * deltaTime;

        // using setTranslateMAt function to make the translate mat
        /*
         * 1 0 dx
         * 0 1 dy
         * 0 0 1
         */
        matrix.setTranslationMat(displacementX, displacementY);
        // in order to find the position to translate the object, i need to multiply the translationmat with the current position of the ball
        HVector2D translateVector = matrix * Position;
        // continue adding on the vector from ball position to the position to be translated to simulate the balls constant movement.
        // the ball will only be translated once if the code was like "transform.position = (translateVector - Position).ToUnityVector3();"
        transform.position += (translateVector - Position).ToUnityVector3();       
    }

    // function to detect the collision between the walls and ball, and apply necessary changes to the position/velocity of the ball
    public void BoundaryCollision()
    {
        // declare two float variable, which will later be used to change the position of the ball
        float xpos, ypos;

        // if the ball is colliding with left wall or colliding with right wall...
        if (ballLeft < leftWall.transform.position.x + wallOffset || ballRight > rightWall.transform.position.x - wallOffset)
        {
            // if the velocity.x is positive, that means the ball is projecting towards right.
            if (Velocity.x > 0f)
            {
                // as such, that means the ball is colliding with the right wall and thus, in order to reposition the balls x position after collision,
                // we minus the value of walloffset and radius to bring the ball towards left to make the ball not colliding/clipping with the wall sprites.
                xpos = rightWall.transform.position.x - wallOffset - Radius;
            }
            else
            {
                // if the ball is colliding with the left wall, we will reposition its x postion by plusing walloffset and ball radius to bring the ball towards right to make the ball not colliding/clipping with the wall sprites.
                xpos = leftWall.transform.position.x + wallOffset + Radius;
            }
            // declaring the new vector v to reposition the ball outside the wall sprite.
            HVector2D v = new HVector2D(xpos - Position.x, 0f);
            // declare translate matrix to translate the ball outside the wall sprite
            matrix.setTranslationMat(v.x, v.y);
            // multiply the translationMat with current position to get the position to relocate the ball outside of the wall sprite
            HVector2D translateVector = matrix * Position;
            // finally relocate the ball outside of the wall sprite by changing the ball.transform.position using the translateVector stored in HVector2D
            transform.position = translateVector.ToUnityVector2();

            // change the velocity.x to be negative so that the ball will be "bounced" off. e.g. if the ball is travelling right and collide, the positive velocity.x value will turn negative,
            // and thus the ball will start to travel towards left, getting bounced off from the right wall.
            // 0.9 is multiplied to simulate energy loss after hitting the wall. in this equation, 10 percent of its energy is lost after the collision with the wall.
            Velocity.x = -Velocity.x * 0.9f;
        }

        // if the ball is colliding with top wall or colliding with bottom wall...
        if (ballBottom < bottomWall.transform.position.y + wallOffset || ballTop > topWall.transform.position.y - wallOffset)
        {
            // if the velocity.y is positive, that means the ball is projecting towards up.
            if (Velocity.y > 0f)
            {
                // as such, that means the ball is colliding with the top wall and thus, in order to reposition the balls y position after collision,
                // we minus the value of walloffset and radius to bring the ball towards down to make the ball not colliding/clipping with the wall sprites.
                ypos = topWall.transform.position.y - wallOffset - Radius;
            }
            else
            {
                // if the ball is colliding with the bottom wall, we will reposition its x postion by plusing walloffset and ball radius to bring the ball towards right to make the ball not colliding/clipping with the wall sprites.
                ypos = bottomWall.transform.position.y + wallOffset + Radius;
            }
            // declaring the new vector v to reposition the ball outside the wall sprite.
            HVector2D v = new HVector2D(0f, ypos - Position.y);
            // declare translate matrix to translate the ball outside the wall sprite
            matrix.setTranslationMat(v.x, v.y);
            // multiply the translationMat with current position to get the position to relocate the ball outside of the wall sprite
            HVector2D translateVector = matrix * Position;
            // finally relocate the ball outside of the wall sprite by changing the ball.transform.position using the translateVector stored in HVector2D
            transform.position = translateVector.ToUnityVector2();

            // change the velocity.y to be negative so that the ball will be "bounced" off. e.g. if the ball is travelling up and collide, the positive velocity.y value will turn negative,
            // and thus the ball will start to travel towards bottom, getting bounced off from the top wall.
            // 0.9 is multiplied to simulate energy loss after hitting the wall. in this equation, 10 percent of its energy is lost after the collision with the wall.
            Velocity.y = -Velocity.y * 0.9f;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        // once the game loads, it will store the float value of x and y value of the Balls position in HVector2D
        Position.x = transform.position.x;
        Position.y = transform.position.y;

        // finding the gameobject with those name to gain access to the gameobjects variable. in this, case, we are trying to find the wall game objects 
        leftWall = GameObject.Find("Left");
        rightWall = GameObject.Find("Right");
        topWall = GameObject.Find("Top");
        bottomWall = GameObject.Find("Bottom");
        // using sprite renderer inside the wall game objects to find the height of the sprite in topWall
        // then, devide it to half to get the wallOffset, which will be used to relocate the position of the ball when colliding with the wall
        wallOffset = topWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;

        // below function is to get the Radius of ball, which has Ball2D.cs attached to it. -------
        // Getting spriteRender to gain access to different functions in SpriteRender Class
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        // Using sprite.rect.size to get the vector value of the sprites width/height (vector value of width and height is same as width and height of circle is the same)
        HVector2D sprite_size = new HVector2D(sprite.rect.size);
        // Finding the local size of the sprite by deviding its vector length to its number of pixel in the sprite per one unit in world space
        HVector2D local_sprite_size = sprite_size / sprite.pixelsPerUnit;
        // Finding the radius of the ball by uising simple math (Radius = Diameter / 2)
        Radius = local_sprite_size.x / 2f;
    }

    
    public void FixedUpdate()
    {
        // debugging (this is to track the movement of the balls)
        //-----------------------------------------
        Debug.DrawLine(lastpos, transform.position, Color.red, 5f);
        lastpos = transform.position;
        //-----------------------------------------

        // updating the position of the ball every fixed frame-rate frame
        Position.x = transform.position.x;
        Position.y = transform.position.y;

        // updating the position of different variables used for the BoundaryCollision
        ballTop = transform.position.y + Radius;
        ballBottom = transform.position.y - Radius;
        ballLeft = transform.position.x - Radius;
        ballRight = transform.position.x + Radius;

        // call the function to check and handle the collision between the ball and the wall after updating all the variables needed
        BoundaryCollision();
        
    }
}
