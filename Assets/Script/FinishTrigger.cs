using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSquare") ||
            other.CompareTag("PlayerCircle") ||
            other.CompareTag("PlayerTriangle"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // If this is the last scene, reload the first one
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Optional: Show "Game Complete" screen
            Debug.Log("Game Complete!");
            SceneManager.LoadScene(0); // or reload current level
        }
    }
}
