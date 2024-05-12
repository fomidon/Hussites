using System.Threading;
using UnityEngine;

public class MapRegion : MonoBehaviour
{
    private Sprite _normalTexture; // Обычная текстура региона
    public Sprite highlightTexture; // Текстура региона при выделении
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer региона
    private static MapRegion previousSelectedRegion; // Ссылка на предыдущий выделенный регион
    public string regionName;
    public string regionType;
    private float mouseBlockTimer = 0;

    public Vector2 position { get; private set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _normalTexture = spriteRenderer.sprite;
        position = transform.position;
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

    void DeselectRegion()
    {
        spriteRenderer.sprite = _normalTexture;
    }

    void Update()
    {
        RightMouseCalc();
    }

    private void RightMouseCalc()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GameManager.MovePlayer(this);
            }
        }
    }
}