using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Additional Feature added
// - Capped Velocity of the ball using magnitude of the Velocity
// - Changing colour of the line depending on the magnitude of the velocity; blue: low velocity, yellow: medium velocity, red: high velocity
// - shorter line is required to set high velocity of the ball compared to the original code
// - stop the ball from moving once the mouse button is pressed on the ball so that its easier for player to aim
public class PoolCue : MonoBehaviour
{
    public LineFactory lineFactory;
    // This gameobject is to find the ball object to track so that player can control the ball by slinging it
    private GameObject ballObject;

    private Line drawnLine;
    private Ball2D ball;

    // Update is called once per frame
    void Update()
    {
        // finding thr gameobjevt eith the tag "Player"
        ballObject = GameObject.FindGameObjectWithTag("Player");
        // getting components from Ball2D class in ballObject to get access to the variables from Ball2d.cs in ballObject.
        ball = ballObject.GetComponent<Ball2D>();

        // since we are using variables from the ball with specific tag "Player", user will onlt be able to control the ball with "Player" Tag, which is white ball.

        // if the unity detects if the player press the "left" mouse button,
        if (Input.GetMouseButtonDown(0))
        {
            
            // it will decalre a vector value of mousposition in the screen
            var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // if the ball is in the game scene and if the mousepoint is colliding with the ball,
            if (ball != null && ball.IsCollidingWith(startLinePos.x, startLinePos.y))
            {
                // set the velocity of the ball to zero when the player press mouse button on the ball
                ball.Velocity = new HVector2D(0, 0);
                // unity will start to draw a black line with thickness of 2f from the mousepoint(startLinePos) to the position of the ball(ball.transform.position)
                drawnLine = lineFactory.GetLine(ball.transform.position, startLinePos, 2f, Color.black);
                // enable the drawnline to make the line visible in the scene 
                drawnLine.EnableDrawing(true);
            }
        }

        // if the button gets released and the line still exists in the game scene,
        if (Input.GetMouseButtonUp(0) && drawnLine != null)
        {
            // disable the line first
            drawnLine.EnableDrawing(false);
            // caculate the final vecotr from mouse position to the ball position
            HVector2D v = new HVector2D(drawnLine.start - drawnLine.end);
            // if the vector length of velocity * 4 is more than 1500, it will revert the magnitude of velocity to be 1500 to prevent the ball from going too fast
            if (v.Magnitude() * 4 >= 1500)
            {
                // normalise the vecotr first, then times 1500 to set the magnitude of the vector to be 1500
                v.Normalize();
                v = v * 1500;
                // magnitude of velocity is now 1500
                ball.Velocity = v;
            }
            // if the vector length of velocity dont exceed the maximum magnitude,
            else
            {
                // we will proceed to set the velocity of the ball to be 4 times of the magnitude of HVector2D v
                ball.Velocity = v * 4;
                   
            }
            
            // and finally, set the drawnLine object to be null to get rid of it from the game scene
            drawnLine = null;
        }

        // if the drawnLine is not null (which means when the player still havent releasew the mouse button)
        if (drawnLine != null)
        {
            // it will update the start and end position of the drawnline to update the length of the line drawn in the scene
            drawnLine.start = ball.transform.position;
            drawnLine.end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // and while updating the line, create a new HVector2D to store the vector value of the line, which will then be used to change the velocity of the ball.
            HVector2D lineVec = new HVector2D(drawnLine.start - drawnLine.end);

            // below codes will change the color of the line depending on the vector length of lineVec to indicate how fast the ball will get mlaunched upon release
            // vector length is multiplied by 4 so that the player can launch the ball faster without drawing the line too long.
            // if the vector length * 4 is less or equal to 700, the color of the line will be blue
            // blue represents that the speed of the ball wont be too high
            if (lineVec.Magnitude() * 4 <= 700)
            {
                // declare the color of the line to be blue
                drawnLine.color = Color.blue;
            }
            // if the vector length * 4 is more or equals 800, the color of the line will be yellow
            // yellow represents that the speed of the ball will be around medium
            if (lineVec.Magnitude() * 4 >= 800)
            {
                // declare the color of the line to be yellow
                drawnLine.color = Color.yellow;
            }
            // if the vector length * 4 is more or equals to 1500, the color of the line will be red
            // red represents that the speed of the ball will be at its maximum
            if (lineVec.Magnitude() * 4 >= 1500)
            {
                // declare the color of the line to be red
                drawnLine.color = Color.red;
            }
        }
    }
}
