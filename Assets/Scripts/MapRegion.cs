using UnityEngine;

public class MapRegion : MonoBehaviour
{
    private Sprite _normalTexture; // Обычная текстура региона
    public Sprite highlightTexture; // Текстура региона при выделении
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer региона
    private static MapRegion previousSelectedRegion; // Ссылка на предыдущий выделенный регион
    public string regionName;
    public string regionType;

    public Vector2 Position { get; private set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _normalTexture = spriteRenderer.sprite;
        Position = transform.position;
    }

    public void SelectRegion()
    {
        if (previousSelectedRegion != null)
            previousSelectedRegion.DeselectRegion();

        // Выделяем текущий регион
        spriteRenderer.sprite = highlightTexture;
        previousSelectedRegion = this;
    }

    private void DeselectRegion()
    {
        spriteRenderer.sprite = _normalTexture;
    }
}