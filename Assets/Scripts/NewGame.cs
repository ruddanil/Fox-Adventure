using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour //Перезапуск игры 
{
    public GameObject pauseMenuUI2;
    public void LoadMenu() //Загрузка главного меню
    {
        SceneManager.LoadScene("MainMenu");
    }
}

