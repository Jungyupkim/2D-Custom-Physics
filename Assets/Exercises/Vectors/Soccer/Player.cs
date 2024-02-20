using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsCaptain = false;
    public Player OtherPlayer;

    float Dot(Vector3 b)                                                           
    {
        Vector3 a = transform.forward;
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    float AngleToPlayer(Player player)
    {
        Vector3 vectorToOtherPlayer = player.transform.position - transform.position;
        vectorToOtherPlayer = vectorToOtherPlayer.normalized;

        float magA = Mathf.Sqrt((float)(Mathf.Pow(transform.forward.x, 2) + Mathf.Pow(transform.forward.y, 2) + Mathf.Pow(transform.forward.z, 2)));
        float magB = Mathf.Sqrt((float)(Mathf.Pow(vectorToOtherPlayer.x, 2) + Mathf.Pow(vectorToOtherPlayer.y, 2) + Mathf.Pow(vectorToOtherPlayer.z, 2)));

        float angle = Mathf.Acos(Dot(vectorToOtherPlayer) /(magA*magB));

        Debug.Log($"Angle from{gameObject.name} to {OtherPlayer.gameObject.name} is {angle * Mathf.Rad2Deg}");

        return angle;     
    }

    void Update()
    {
        
        if (IsCaptain)
        {
            DebugExtension.DebugArrow(transform.position, OtherPlayer.transform.position - transform.position , Color.black);
            AngleToPlayer(OtherPlayer);
        }    
    }


}

