using UnityEngine;

public class ShrinkAbility : MonoBehaviour
{
    private bool isShrunk = false;
    private Vector3 originalScale;
    private Vector3 shrunkScale = new Vector3(0.1f, 0.1f, 1f);

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Shrink activated");
            SFXManager.Instance.PlaySound(SFXManager.Instance.shrinkSound);

            if (!isShrunk)
            {
                transform.localScale = shrunkScale;
                isShrunk = true;
            }
            else
            {
                transform.localScale = originalScale;
                isShrunk = false;
            }
        }
    }
}
