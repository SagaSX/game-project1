using UnityEngine;

public class MovableWall : MonoBehaviour
{
    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float moveSpeed = 2f;

    private bool shouldOpen = false;

    void Start()
    {
        closedPosition = transform.localPosition;
    }

    void Update()
    {
        Vector3 target = shouldOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, moveSpeed * Time.deltaTime);
    }

    public void SetOpen(bool open)
    {
        shouldOpen = open;
    }
}
