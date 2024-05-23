using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //able to load scenes from script

public class HUDController : MonoBehaviour
{

    GameObject helpText;

    public void Start()
    {
        helpText = GameObject.Find("HelpText");
    }

    public void OnCreateButton() 
    {
        SceneManager.LoadScene("Create");
    }

    public void OnPlayButton()
    {
        // Load and Play
        GameObject.Find("SaveAndLoadHandler").GetComponent<SaveAndLoadHandler>().SaveGame();
        SceneManager.LoadScene("Play");
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnHelpButton(){
        helpText.SetActive(!helpText.activeSelf);
    }

}
