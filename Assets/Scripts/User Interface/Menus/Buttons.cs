using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //able to load scenes from script

/*
 * Script for various button actions on Main menu and other scenes
 */
public class Buttons : MonoBehaviour
{
    public void OnCreateButton () 
    {
        SceneManager.LoadScene("Create");
    }

    public void OnPlayButton ()
    {
        // Load and Play
        SceneManager.LoadScene("Play");
    }

    public void OnMenuButton ()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnSaveButton ()
    {
        // Save Things
    }

    // TODO:
    // Lobby Button


}
