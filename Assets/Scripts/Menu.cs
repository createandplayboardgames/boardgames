using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //able to load scenes from script

public class Menu : MonoBehaviour
{
    public void OnCreateButton () 
    {
        SceneManager.LoadScene(1);
    }

    public void OnPlayButton ()
    {
        SceneManager.LoadScene(2);
    }

}
