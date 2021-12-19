using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    
    [SerializeField] private float Speed = 5f;

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(facingLeft)
        {
            if(transform.position.x > leftCap)
            {
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
            }
            else 
            {
                facingLeft = false;
            }
        }
        else 
        {
            if(transform.position.x < rightCap)
            {
                if(transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                rb.velocity = new Vector2(Speed, rb.velocity.y);
            }
            else 
            {
                facingLeft = true;
            }
        }

    }

}

