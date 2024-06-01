using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    public GameObject canvas;
    public ProgressSaveManager saveManager;
    
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
        // saveManager.TryReadFromSave(SaveType.BasicSave, out var baseData);
        // _gameManager.LoadFromSave(baseData);
        // canvas.SetActive(false);
        
    }
    
    public void SaveAndQuit()
    {
        Application.Quit();
    }
}
