using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Header("Transition UI")]
    public Image fadeImage;
    public TextMeshProUGUI levelText;
    public float fadeDuration = 1f;
    public float levelTextDisplayTime = 1.5f;

    private bool isTransitioning = false;

    private void Awake()
    {
        // Singleton pattern (only one across all scenes)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Start with screen black, then fade in
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
        }

        StartCoroutine(FadeFromBlack());
    }

    public void LoadNextScene()
    {
        if (isTransitioning) return;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Wrap around if reached last scene
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        StartCoroutine(FadeAndLoad(nextSceneIndex));
    }

    public void LoadSceneByName(string sceneName)
    {
        if (isTransitioning) return;
        StartCoroutine(FadeAndLoad(sceneName));
    }

    // ===== Core Transition Coroutine (by build index) =====
    private IEnumerator FadeAndLoad(int sceneIndex)
    {
        isTransitioning = true;

        // Fade to black
        yield return StartCoroutine(FadeToBlack());

        // Load next scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
            yield return null;

        // Wait one frame so UI references are ready
        yield return null;

        // Show level text after load
        yield return StartCoroutine(ShowLevelText(sceneIndex));

        isTransitioning = false;
    }

    // ===== Core Transition Coroutine (by name) =====
    private IEnumerator FadeAndLoad(string sceneName)
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeToBlack());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        yield return null;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return StartCoroutine(ShowLevelText(sceneIndex));

        isTransitioning = false;
    }

    // ===== Fade Helpers =====
    private IEnumerator FadeToBlack()
    {
        if (fadeImage == null) yield break;
        float t = 0f;
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }

    private IEnumerator FadeFromBlack()
    {
        if (fadeImage == null) yield break;
        float t = 0f;
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }

    // ===== Level Text Display =====
    private IEnumerator ShowLevelText(int sceneIndex)
    {
        // Fade in from black
        yield return StartCoroutine(FadeFromBlack());

        // Only show level text for gameplay scenes (not MainMenu)
        if (sceneIndex > 0 && levelText != null)
        {
            levelText.text = "Level " + sceneIndex;
            levelText.alpha = 0f;
            levelText.gameObject.SetActive(true);

            // Fade in text
            float t = 0f;
            while (t < 0.5f)
            {
                t += Time.deltaTime;
                levelText.alpha = Mathf.Lerp(0f, 1f, t / 0.5f);
                yield return null;
            }

            yield return new WaitForSeconds(levelTextDisplayTime);

            // Fade out text
            t = 0f;
            while (t < 0.5f)
            {
                t += Time.deltaTime;
                levelText.alpha = Mathf.Lerp(1f, 0f, t / 0.5f);
                yield return null;
            }

            levelText.gameObject.SetActive(false);
        }
    }
}
