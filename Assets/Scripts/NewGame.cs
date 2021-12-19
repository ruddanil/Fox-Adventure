using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public GameObject pauseMenuUI2;
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

