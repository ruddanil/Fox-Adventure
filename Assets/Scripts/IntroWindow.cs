using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroWindow : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    public void SkipIntro()
    {
        SceneManager.LoadScene("Game");
    }

    void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnDisable() 
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer causedVideoPlayer)
    {
        SceneManager.LoadScene("Game");
    }
}
