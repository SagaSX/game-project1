using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject[] playerForms;
    public Transform defaultSpawnPoint;
    public GameObject deathParticlesPrefab;

    private Vector3 currentCheckpoint;
    private GameObject currentPlayer;

    void Start()
    {
        // Start with default spawn point
        currentCheckpoint = defaultSpawnPoint.position;
        SpawnRandomPlayer();
    }

    void SpawnRandomPlayer()
    {
        int index = Random.Range(0, playerForms.Length);
        GameObject newPlayer = Instantiate(playerForms[index], currentCheckpoint, Quaternion.identity);
        currentPlayer = newPlayer;
    }

    public void KillPlayer()
    {
        if (currentPlayer != null)
        {
            Instantiate(deathParticlesPrefab, currentPlayer.transform.position, Quaternion.identity);
            Destroy(currentPlayer);
            Invoke(nameof(SpawnRandomPlayer), 1f);
            SFXManager.Instance.PlaySound(SFXManager.Instance.deathSound);
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
}
