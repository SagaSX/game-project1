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
            // Trigger fade transition if available
            if (SceneTransitionManager.Instance != null)
            {
                SceneTransitionManager.Instance.LoadNextScene();
            }
            else
            {
                Debug.LogWarning("SceneTransitionManager not found! Loading directly...");
                LoadNextLevelDirect();
            }
        }
    }

    // Backup method if no transition manager found
    private void LoadNextLevelDirect()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Game Complete!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
