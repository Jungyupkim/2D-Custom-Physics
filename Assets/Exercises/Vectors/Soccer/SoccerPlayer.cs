using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    public bool IsCaptain = false;
    public SoccerPlayer[] OtherPlayers;
    public float rotationSpeed = 1f;

    float angle = 0f;

    float Dot(Vector3 b)
    {
        Vector3 a = transform.forward;
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }
    
    private void Start()
    {
        OtherPlayers = FindObjectsOfType<SoccerPlayer>().Where(t => t != this).ToArray();
    }
       
    void DrawVectors()
    {
        foreach (SoccerPlayer other in OtherPlayers)
        {
            Debug.DrawLine(transform.position, other.transform.position, Color.black);
        }
    }
    
    float AngleToPlayer(Player player)
    {
        Vector3 vectorToOtherPlayer = player.transform.position - transform.position;
        vectorToOtherPlayer = vectorToOtherPlayer.normalized;

        float magA = Mathf.Sqrt((float)(Mathf.Pow(transform.forward.x, 2) + Mathf.Pow(transform.forward.y, 2) + Mathf.Pow(transform.forward.z, 2)));
        float magB = Mathf.Sqrt((float)(Mathf.Pow(vectorToOtherPlayer.x, 2) + Mathf.Pow(vectorToOtherPlayer.y, 2) + Mathf.Pow(vectorToOtherPlayer.z, 2)));

        float angle = Mathf.Acos(Dot(vectorToOtherPlayer) / (magA * magB));

        Debug.Log($"Angle from{gameObject.name} to {player.name} is {angle * Mathf.Rad2Deg}");

        return angle;
    }

    void Update()
    {
        DebugExtension.DebugArrow(transform.position, transform.forward, Color.red);
        if (IsCaptain)
        {
            angle += Input.GetAxis("Horizontal") * rotationSpeed;
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);

            DrawVectors();

            SoccerPlayer targetPlayer = FindClosestPlayerDot();
            targetPlayer.GetComponent<Renderer>().material.color = Color.green;

            foreach (SoccerPlayer other in OtherPlayers.Where(t => t != targetPlayer))
            {
                other.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    SoccerPlayer FindClosestPlayerDot()
    {
        SoccerPlayer closest = null;
        float minAngle = 180f;

        for (int i = 0; i < OtherPlayers.Length; i++)
        {
            Vector3 toPlayer = OtherPlayers[i].transform.position - transform.position;
            toPlayer = toPlayer.normalized;

            float dot = Dot(toPlayer);
            float angle = Mathf.Acos(dot);
            angle = angle * Mathf.Rad2Deg;

            if (angle < minAngle)
            {
                minAngle = angle;
                closest = OtherPlayers[i];
            }
        }
        return closest;
    }

}
