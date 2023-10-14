using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isOpened = false;
	public bool settingsOpened = false;
    
    [SerializeField] public GameObject objPrefab;
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetMouseButtonDown(0) && settingsOpened)
        {
            objPrefab.SetActive(false);
            settingsOpened = false;
        }
    }

    public void OpenSettings()
    {
        Debug.Log("Settings");
		if (settingsOpened)
        {
            objPrefab.SetActive(false);
			settingsOpened = false;
		}
		else
        {
            objPrefab.SetActive(true);
			settingsOpened = true;
		}
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
