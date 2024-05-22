using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameManager _gameManager;
    private bool _canClick = true;
    private MapRegion _currentMapRegion;
    

    void Update()
    {
        if (_canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetRegionInfo();
                _currentMapRegion.SelectRegion();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetRegionInfo();
                //_image.gameObject.SetActive(true);
                //_canClick = false;
                
                //Временно
                ClickYes();
            }
        }
    }

    private void GetRegionInfo()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            _currentMapRegion = hit.collider.GetComponent<MapRegion>();
        }
    }

    private IEnumerator MovePlayerAfterDelay()
    {
        // Блокируем обработку правой кнопки мыши на некоторое время, чтобы избежать мгновенного перемещения
        yield return new WaitForSeconds(0.5f);

        // После задержки перемещаем игрока
        if (_gameManager != null)
        {
            _gameManager.MovePlayerToRegion(_currentMapRegion);
        }
        else
        {
            Debug.LogWarning("Экземпляр GameManager не установлен.");
        }
    }

    public void ClickYes()
    {
        StartCoroutine(MovePlayerAfterDelay());
        ClickNo();
        
    }

    public void ClickNo()
    {
        //_image.gameObject.SetActive(false);
        _canClick = true;
    }
}