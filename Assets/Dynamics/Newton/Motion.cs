using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;

        float dx = velocity.x * dt;
        float dy = velocity.y;
        float dz = velocity.z;

        transform.position = (new Vector3(dx, dy, dz) + transform.position);
    }
}
