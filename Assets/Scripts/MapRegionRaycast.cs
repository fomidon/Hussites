using UnityEngine;

public class MapController : MonoBehaviour
{
    public Color highlightColor = Color.red; // Цвет выделения региона
    private Color originalColor; // Исходный цвет региона
    private SpriteRenderer selectedRegionRenderer; // Ссылка на SpriteRenderer выделенного региона

    void Start()
    {
        // Получаем ссылку на SpriteRenderer карты
        selectedRegionRenderer = GetComponent<SpriteRenderer>();
        // Запоминаем исходный цвет
        originalColor = selectedRegionRenderer.color;
    }

    void Update()
    {
        // Обработка нажатия на карту
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                // Если нажатый объект имеет коллайдер, который нужно обрабатывать
                if (hit.collider.gameObject == gameObject)
                {
                    // Снимаем выделение с предыдущего региона
                    if (selectedRegionRenderer != null)
                    {
                        selectedRegionRenderer.color = originalColor;
                    }
                    // Устанавливаем новый выделенный регион
                    selectedRegionRenderer = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    // Запоминаем исходный цвет нового региона
                    originalColor = selectedRegionRenderer.color;
                    // Выделяем регион новым цветом
                    selectedRegionRenderer.color = highlightColor;
                }
            }
        }
    }
}