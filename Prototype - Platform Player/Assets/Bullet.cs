using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pos;
    public Rigidbody2D rb;
    void Start()
    {
        transform.position = pos.position;
        rb.velocity = Vector2.right;
    }

    public void Spawn()
    {
        transform.position = pos.position;
        rb.velocity = Vector2.right;
    }
}
