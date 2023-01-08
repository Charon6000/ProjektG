using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 move;
    Vector2 mousePos;
    public Camera cam;

    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        mousePos= cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        Vector2 look = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
    }
}
