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
    public string Bron;
    public Animator anim;

    void Update()
    {
        MoveUpdate();
        Fight();
        Block();
    }

    private void FixedUpdate() 
    {
        MoveFixed();
    }

    void Fight()
    {
        if(Input.GetButtonDown("Fire1"))
            anim.SetBool("handHit", true);
        else if(Input.GetButtonUp("Fire1"))
            anim.SetBool("handHit", false);
    }

    void Block()
    {
        if(Input.GetButtonDown("Fire2"))
            anim.SetBool("Block", true);
        else if(Input.GetButtonUp("Fire2"))
            anim.SetBool("Block", false);
    }

    void MoveUpdate()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void MoveFixed()
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        Vector2 look = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
    }
}
