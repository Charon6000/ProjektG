using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OWController : MonoBehaviour
{
    public bool canMove = true;
    public bool inDialogue = false;
    Inputs _inputs;
    InputAction move, interact;
    [SerializeField] float dir, speed, interactRange;
    
    SpriteRenderer spr;
    private void Awake()
    {
        _inputs = new Inputs();
        spr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        interact.performed += findInteraction;
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
    void findInteraction(InputAction.CallbackContext ctx)
    {
        if(inDialogue){FindObjectOfType<NodePasser>().playerInterrupts();return;}
        Collider2D[] boxResult;
        boxResult = Physics2D.OverlapCircleAll(this.transform.position, interactRange);
        foreach (Collider2D col in boxResult)
        {
            if(col.transform.tag == "Interactable")
            {
                col.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
        
    }

    private void OnEnable()
    {
        move = _inputs.Player.Movement;
        move.Enable();
        interact = _inputs.Player.Interaction;
        interact.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        interact.Disable();
    }
}
