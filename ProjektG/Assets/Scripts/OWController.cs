using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OWController : MonoBehaviour
{
    bool canMove = true;
    Inputs _inputs;
    InputAction move;
    [SerializeField] float dir, speed;
    SpriteRenderer spr;
    private void Awake()
    {
        _inputs = new Inputs();
        spr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        dir = move.ReadValue<float>();
    }
    private void FixedUpdate()
    {
        doMove();
    }

    void doMove()
    {
        if (canMove == false)
        {
            return;
        }
        switch (dir)
        {
            case 1:
                spr.flipX = false;
                break;
            case -1:
                spr.flipX = true;
                break;
            case 0:
                break;
        }
        Vector2 movementV2 = new Vector2(dir, 0);
        transform.Translate(movementV2 * Time.deltaTime * speed);
    }
    public IEnumerator stopMove()
    {
        canMove = false;
        yield return new WaitForSeconds(2f);
        canMove = true;
    }
    private void OnEnable()
    {
        move = _inputs.Player.Movement;
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }
}
