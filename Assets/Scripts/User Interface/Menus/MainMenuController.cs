using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //able to load scenes from script

public class MainMenuController : MonoBehaviour
{

    public void OnCreateButton () 
    {
        SceneManager.LoadScene("Create");
    }

    public void OnPlayButton ()
    {
        SceneManager.LoadScene("Play");
    }

}
