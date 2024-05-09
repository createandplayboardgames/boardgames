using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //able to load scenes from script

/*
 * Script for various button actions on Main menu and other scenes
 */
public class Buttons : MonoBehaviour
{

    GameObject helpText;

    public void Start(){
        helpText = GameObject.Find("Help Text");
    }

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
    public void OnHelpButton(){
        helpText.SetActive(!helpText.activeSelf);
    }

    // TODO:
    // Lobby Button


}
