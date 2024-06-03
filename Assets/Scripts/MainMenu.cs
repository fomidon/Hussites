using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Tutorial _tutorial;
    public GameObject canvas;
    public readonly ProgressSaveManager SaveManager = new();
    
    
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
        SaveManager.TryReadFromSave(SaveType.BasicSave, out var baseData);
        _gameManager.LoadFromSave(baseData);
        _tutorial.Restart();
        canvas.SetActive(false);
        
    }
    
    public void SaveAndQuit()
    {
        Application.Quit();
    }
}
