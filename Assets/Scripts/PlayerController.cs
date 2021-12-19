using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; //start variables
    private Animator anim;
    private Collider2D coll;

    private enum State {idle, running, jumping, falling, hurt} //fsm
    private State state = State.idle;

    [SerializeField] private LayerMask ground; //глобальный доступ "инспектора"
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int scoreGem = 0;
    [SerializeField] private int scoreOpos = 0;
    [SerializeField] private Text gemText;
    [SerializeField] private Text oposText;
    [SerializeField] private float hurtForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int) state);
    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            scoreGem += 1;
            gemText.text = scoreGem.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling) 
            {
                Destroy(other.gameObject);
                scoreOpos += 1;
                oposText.text = scoreOpos.ToString();
            }
            else
            {
                state = State.hurt;
                if(other.gameObject.transform.position.x > transform.position.x) // Враг справа, откидывает влево
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y); // Враг слева, откидывает вправо
                }
            }
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) // проверка на касание земли в прыжке
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
