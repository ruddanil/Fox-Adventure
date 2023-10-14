using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour //Скрипт для завершения игры 
{
    [SerializeField] private string sceneName; 

    private void OnTriggerEnter2D(Collider2D collision) //При соприкосновеннии с объектом загружается сцена
    {
        if (collision.gameObject.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
