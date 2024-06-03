using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapRegion : MonoBehaviour
{
    // [SerializeField] private Image _regionInfo;
    // [SerializeField] private TMP_Text _regionInfoName;
    // [SerializeField] private TMP_Text _regionInfoType;
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
        // _regionInfo.gameObject.SetActive(true);
        // _regionInfoName.text = regionName;
        // _regionInfoType.text = regionType;

    }

    public void DeselectRegion()
    {
        spriteRenderer.sprite = _normalTexture;
        // _regionInfo.gameObject.SetActive(false);
    }
}