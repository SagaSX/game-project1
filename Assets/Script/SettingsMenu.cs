using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public GameObject settingsPanel;
    public Button settingsButton;
    public Button mainmenuButton;
    public Button muteButton;
    public Button quitButton;

    private bool isMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);

        settingsButton.onClick.AddListener(ToggleSettings);
        mainmenuButton.onClick.AddListener(GoToMainMenu);
        muteButton.onClick.AddListener(ToggleMute);
        quitButton.onClick.AddListener(QuitGame);
    }

    void ToggleSettings()
    {
        bool newstate = !settingsPanel.activeSelf;
        settingsPanel.SetActive(newstate);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
        Debug.Log(isMuted ? "Muted" : "Unmuted");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool newstate = !settingsPanel.activeSelf;
            settingsPanel.SetActive(newstate);
        }
    }
}
