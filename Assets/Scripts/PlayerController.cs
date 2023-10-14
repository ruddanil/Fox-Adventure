using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Animator anim;
    private Collider2D coll;

    private enum State {idle, running, jumping, falling, hurt} 
    private State state = State.idle;

    [SerializeField] private LayerMask ground; 
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int scoreGem = 0; 
    [SerializeField] private int scoreOpos = 0; 
    [SerializeField] private Text gemText;
    [SerializeField] private Text oposText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private int health = 3;
    [SerializeField] private int numOfHearth = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private AudioClip damageAudio;
    [SerializeField] private AudioClip killAudio;
    [SerializeField] private AudioClip gemAudio;
    [SerializeField] private AudioSource damageAudioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        if (state != State.hurt) //Если персонаж не в анимации после получения урона - движения разблокированы
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int) state);
    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.tag == "Collectable") //"Сбор" гемов 
        {
            damageAudioSource.PlayOneShot(gemAudio);
            Destroy(collision.gameObject);
            scoreGem += 1;
            gemText.text = scoreGem.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(state == State.falling) //Если соприкосновение произошло в прыжке - враг уничтожается 
            {
                damageAudioSource.PlayOneShot(killAudio);
                Destroy(other.gameObject);
                scoreOpos += 1;
                oposText.text = scoreOpos.ToString();
            }
            else //Иначе игрок получает "урон"
            {
                health -= 1;
                damageAudioSource.PlayOneShot(damageAudio);
                if (health < 1)
                {
                    SceneManager.LoadScene("GameOver");
                }
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

    private void AnimationState() //Переключение анимаций
    {
        if (state == State.jumping) //Из прыжка в падение
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling) //Из падения в обычную
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
