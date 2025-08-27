using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSquare") || other.CompareTag("PlayerCircle") || other.CompareTag("PlayerTriangle"))
        {
            FindObjectOfType<RespawnManager>().SetCheckpoint(transform.position);
            Debug.Log("Checkpoint reached at: " + transform.position);
        }
    }
}
