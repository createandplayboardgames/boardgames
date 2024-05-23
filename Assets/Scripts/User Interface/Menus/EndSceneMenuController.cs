using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //able to load scenes from script

public class EndSceneMenuController : MonoBehaviour
{

    public void OnPlayButton()
    {
        // Load and Play
        SceneManager.LoadScene("Play");
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

}
