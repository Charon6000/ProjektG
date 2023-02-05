using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int MaxHP;
    public int HP;
    Animator anim;
    public float speed;
    Transform target;
    public float minDistance;
    Rigidbody2D rb;
    public GameObject HPBar;

    private void Start() 
    {
        HP = MaxHP;
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }    

    void Update()
    {
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
        FollowAI();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Hit" && !anim.GetBool("Block"))
            HP--;
    }

    void FollowAI()
    {
        if(Vector2.Distance(transform.position, target.position) > minDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
        else if(Vector2.Distance(transform.position, target.position) < minDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed* Time.deltaTime);

        Vector2 look = new Vector2(target.position.x, target.position.y) - rb.position;
        rb.rotation = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
    }
}
