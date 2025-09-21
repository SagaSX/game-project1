using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RespawnManager : MonoBehaviour
{
    public GameObject[] playerForms;       // Square, Triangle, Circle
    public Transform defaultSpawnPoint;
    public GameObject deathParticlesPrefab;
    public GameObject respawnMenuPanel;    // UI Panel

    public TMP_Text livesText;

    private Vector3 currentCheckpoint;
    private GameObject currentPlayer;

    public int maxDeath = 3;
    public int currentDeath = 0;

    void Start()
    {
        currentCheckpoint = defaultSpawnPoint.position;
        SpawnRandomPlayer();
        // Hide menu at the start
        if (respawnMenuPanel != null)
            respawnMenuPanel.SetActive(false);
        UpdateLivesUI();
    }

    // Called when player dies
    public void KillPlayer()
    {
        if (currentPlayer != null)
        {
            Instantiate(deathParticlesPrefab, currentPlayer.transform.position, Quaternion.identity);
            Destroy(currentPlayer);

            SFXManager.Instance.PlaySound(SFXManager.Instance.deathSound);

            currentDeath++;
            UpdateLivesUI();

            if (currentDeath >= maxDeath)
            {
                Invoke(nameof(RestartStage), 0.5f);
            }
            else
            {
                // Show respawn menu instead of auto respawn
                if (respawnMenuPanel != null)
                    respawnMenuPanel.SetActive(true);
            }
        }
    }

    // Specific respawn called by UI buttons
    public void RespawnSpecificPlayer(int index)
    {
        Debug.Log("[Respawn] called with index: " + index);
        if (respawnMenuPanel != null)
            respawnMenuPanel.SetActive(false);

        if (index >= 0 && index < playerForms.Length)
        {
            GameObject newPlayer = Instantiate(playerForms[index], currentCheckpoint, Quaternion.identity);
            currentPlayer = newPlayer;
        }
        else
        {
            Debug.LogWarning("[Respawn] Invalid player index: " + index);
        }
    }

    void SpawnRandomPlayer()
    { 
        int index = Random.Range(0, playerForms.Length); 
        GameObject newPlayer = Instantiate(playerForms[index], currentCheckpoint, Quaternion.identity); 
        currentPlayer = newPlayer; 
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    void RestartStage()
    {
        currentDeath = 0;
        UpdateLivesUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateLivesUI()
    {
        int livesLife = maxDeath - currentDeath;
        livesText.text = "Lives: " +livesLife.ToString();
    }
}
