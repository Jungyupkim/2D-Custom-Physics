using UnityEngine;
using System.Collections;

public class Paddle_NP : MonoBehaviour {
	
	public KeyCode up;
	public KeyCode down;
	
	GameObject rightWall;
	float wallHeight, paddleHeight;
	
	void Start()
	{
		rightWall = GameObject.Find ("RightWall");
		wallHeight = rightWall.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
		paddleHeight = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
	}
	
	void FixedUpdate () {
		if (Input.GetKey(up)) {
			if(gameObject.transform.position.y + paddleHeight/2 < rightWall.transform.position.y + wallHeight/2)
			{
				transform.Translate(new Vector2(0.0f, 0.1f));
			}
		}
		
		if (Input.GetKey(down)) 
		{
			if(gameObject.transform.position.y - paddleHeight/2 > rightWall.transform.position.y - wallHeight/2)
			{
				transform.Translate(new Vector2(0.0f, -0.1f));
			}
		}
	}
}


