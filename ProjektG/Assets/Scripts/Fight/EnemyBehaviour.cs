using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int HP;
    Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }    

    void Update()
    {
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Hit" && !anim.GetBool("Block"))
            HP--;
    }
}
