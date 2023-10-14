using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //ѕроверка на падение за пределы карты
    {
        if(collision.gameObject.tag == "Player") //ѕри соприкосновении с игроком игра перезапускаетс€
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
