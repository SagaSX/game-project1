using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public Transform plateVisual;
    public Vector3 pressedPosition;
    public Vector3 unpressedPosition;
    public float moveSpeed = 1f;
    public MovableWall linkedWall;

    private int heavyObjectOnPlate = 0;
    void Start()
    {
        unpressedPosition = plateVisual.localPosition;
    }
        
    void Update()
    {
        Vector3 targetPos = heavyObjectOnPlate > 0 ? pressedPosition : unpressedPosition;
        plateVisual.localPosition = Vector3.Lerp(plateVisual.localPosition, targetPos, Time.deltaTime * moveSpeed);

        if (linkedWall != null)
        {
            linkedWall.SetOpen(heavyObjectOnPlate > 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            heavyObjectOnPlate++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            heavyObjectOnPlate--;
        }
    }
}   
