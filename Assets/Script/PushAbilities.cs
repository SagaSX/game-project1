using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbilities : MonoBehaviour
{
    private Rigidbody2D rb;
    public float defaultMass = 1000f;
    public float pushableMass = 40f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = defaultMass;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerSquare"))
        {
            rb.mass = pushableMass;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerSquare"))
            {
                rb.mass = defaultMass;
        }
    }
}
