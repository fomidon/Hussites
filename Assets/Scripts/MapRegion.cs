using System;
using System.Collections;
using UnityEngine;

public class MapRegion : MonoBehaviour
{
    private Sprite _normalTexture; // Обычная текстура региона
    public Sprite highlightTexture; // Текстура региона при выделении
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer региона
    private static MapRegion previousSelectedRegion; // Ссылка на предыдущий выделенный регион
    public string regionName;
    public string regionType;
    private float mouseBlockTimer = 0.5f;
    private GameManager gameManager; // Ссылка на экземпляр GameManager

    public Vector2 position { get; private set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _normalTexture = spriteRenderer.sprite;
        position = transform.position;

        // Находим экземпляр GameManager в сцене
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Не удалось найти экземпляр GameManager в сцене.");
        }
    }

    void OnMouseDown()
    {
        // Снимаем выделение с предыдущего региона
        if (previousSelectedRegion != null)
        {
            previousSelectedRegion.DeselectRegion();
        }
        // Выделяем текущий регион
        spriteRenderer.sprite = highlightTexture;
        // Обновляем ссылку на предыдущий выделенный регион
        previousSelectedRegion = this;
    }

    private void DeselectRegion()
    {
        spriteRenderer.sprite = _normalTexture;
    }

    void Update()
    {
        RightMouseCalc();
    }

    private void RightMouseCalc()
    {
        if (Input.GetMouseButtonUp(1) && mouseBlockTimer <= 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                StartCoroutine(MovePlayerAfterDelay());
            }
        }
        else if (mouseBlockTimer > 0)
        {
            mouseBlockTimer -= Time.deltaTime;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator MovePlayerAfterDelay()
    {
        // Блокируем обработку правой кнопки мыши на некоторое время, чтобы избежать мгновенного перемещения
        yield return new WaitForSeconds(0.005f);

        // После задержки перемещаем игрока
        if (gameManager != null)
        {
            gameManager.MovePlayerToRegion(this);
        }
        else
        {
            Debug.LogWarning("Экземпляр GameManager не установлен.");
        }
    }
}
