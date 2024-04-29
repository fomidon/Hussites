using UnityEngine;

public class MapRegion : MonoBehaviour
{
    private Sprite _normalTexture; // Обычная текстура региона
    public Sprite highlightTexture; // Текстура региона при выделении
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer региона
    private static MapRegion previousSelectedRegion; // Ссылка на предыдущий выделенный регион
    public string regionName;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _normalTexture = spriteRenderer.sprite;
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
}