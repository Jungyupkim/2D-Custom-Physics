using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public Transform planet;
    public float force = 5f;
    public float gravityStrength = 5f;

    private Vector3 gravityDir, gravityNorm;
    private Vector3 moveDir;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        gravityDir = planet.transform.position - rb.transform.position ;
        moveDir = new Vector3(gravityDir.y, -gravityDir.x, 0f);
        moveDir = moveDir.normalized * -1f;

        rb.velocity = moveDir * force;

        gravityNorm = gravityDir.normalized;
        rb.AddForce(gravityNorm * gravityStrength);
        //signedAngle will return the angle between two vectors rotating around the axis
        //signedAngle will always return the smallest angle
        float angle = Vector3.SignedAngle(Vector3.right, moveDir, Vector3.forward);
        //using quaternion to rotate the rb by "angle" degree along the z axis
        rb.MoveRotation(Quaternion.Euler(0f, 0f, angle));
    
        DebugExtension.DebugArrow(rb.transform.position, gravityDir, Color.red);
        DebugExtension.DebugArrow(rb.transform.position, moveDir, Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
