using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSquare"))
        {
            FindObjectOfType<RespawnManager>().KillPlayer();
        }

        if (other.CompareTag("PlayerTriangle"))
        {
            FindObjectOfType<RespawnManager>().KillPlayer();
        }

        if (other.CompareTag("PlayerCircle"))
        {
            FindObjectOfType<RespawnManager>().KillPlayer();
        }
    }
}
