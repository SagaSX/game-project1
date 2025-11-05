using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpriteSoundUpdater : MonoBehaviour
{

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    private Image buttonImage;
    private bool isMuted = false;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateIcon();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (isMuted)
            buttonImage.sprite = soundOffSprite;
        else
            buttonImage.sprite = soundOnSprite;
    }
}
