using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour //Логика передвижения противников
{
    private Rigidbody2D rb;
    private Collider2D coll;
    
    [SerializeField] private float leftCap; //Левая граница передвижения
    [SerializeField] private float rightCap; //Правая граница передвижения

    [SerializeField] private float Speed = 5f; //Скорость передвижения

    private bool facingLeft = true;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(facingLeft) //Повернут влево 
        {
            if(transform.position.x > leftCap) //Не достиг левой границы
            {
                if(transform.localScale.x != 1) //Направлен влево? 
                {
                    transform.localScale = new Vector3(1, 1); //Направляем влево
                }
                rb.velocity = new Vector2(-Speed, rb.velocity.y); //Двигаемся влево 
            }
            else 
            {
                facingLeft = false; //Нужно развернуться 
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

