using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RespawnManager : MonoBehaviour
{
    public GameObject[] playerForms;
    public Transform defaultSpawnPoint;
    public GameObject deathParticlesPrefab;

    private Vector3 currentCheckpoint;
    private GameObject currentPlayer;

    public int maxDeath = 3;
    public int currentDeath = 0;

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

            currentDeath++;

            if (currentDeath>=maxDeath)
            {
                Invoke(nameof(RestartStage), 0.5f);
            }
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    void RestartStage()
    {
        currentDeath = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
