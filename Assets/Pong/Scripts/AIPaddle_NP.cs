using UnityEngine;
using System.Collections;

public class AIPaddle_NP : MonoBehaviour 
{
    public float Speed = 8f;

    GameObject ball, topWall, bottomWall;
    float topLimit, bottomLimit;

	void Start () 
	{
        // This could also be a public property in the Inspector.
        // Note that Find is somewhat slow, so shouldn't be used
        // in code that is repeated often - but in Start is ok.
        //
		ball = GameObject.Find ("Ball");

        topWall = GameObject.Find("TopWall");
        bottomWall = GameObject.Find("BottomWall");

        // Check that the paddle doesn't poke through the top or bottom walls.
        // This is done by clamping the max and min Y values.
        // Also, remember that in Unity the Y axis goes up!
        //
        topLimit = topWall.transform.position.y 
                    - topWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f
                    - GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;
        bottomLimit = bottomWall.transform.position.y
                    + bottomWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f
                    + GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;
    }
	
	void FixedUpdate () 
	{
		float rand = Random.Range(0.25f, 1.25f);
		float speed = Time.deltaTime * Speed * rand;

		if(ball.transform.position.y >= transform.position.y + 0.25f)
		{
			transform.Translate(new Vector2(0.0f, speed));
            if(transform.position.y > topLimit)
            {
                transform.position = new Vector3(transform.position.x, topLimit, transform.position.z);
            }
		}
		else if(ball.transform.position.y <= transform.position.y - 0.25f)
		{
			transform.Translate(new Vector2(0.0f, -speed));
            if (transform.position.y < bottomLimit)
            {
                transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
            }
        }	
	}
}
