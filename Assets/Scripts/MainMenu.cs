using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject canvas;
    
    public void OpenMenu()
    {
        canvas.SetActive(true);
       
    }

    public void Continue()
    {
        canvas.SetActive(false);
       
    }

    // Update is called once per frame
    public void StartNewGame()
    {
        
    }
    
    public void SaveAndQuit()
    {
        
    }
}
