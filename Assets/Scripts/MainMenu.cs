using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Tutorial _tutorial;
    public GameObject canvas;
    public readonly ProgressSaveManager SaveManager = new();
    
    public StoryScript _story;
    
    public void OpenMenu()
    {
        canvas.SetActive(true);
        //_story.ShowStory();
       
    }

    public void Continue()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    public void StartNewGame()
    {
        SaveManager.TryReadFromSave(SaveType.BasicSave, out var baseData);
        _story.ShowStory();
        _gameManager.LoadFromSave(baseData);
        _tutorial.Restart();
        canvas.SetActive(false);
        
        
    }
    
    public void SaveAndQuit()
    {
        SaveManager.SaveProgress(SaveType.ManualSave, _gameManager.player);
        Application.Quit();
    }
}
