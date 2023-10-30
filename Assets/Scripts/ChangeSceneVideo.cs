using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ChangeSceneVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        if (vp == videoPlayer)
        {
            // Cambiar de escena al final del video
            SceneManager.LoadScene(sceneToLoad);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
