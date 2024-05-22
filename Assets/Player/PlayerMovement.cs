using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MapRegion initialRegion;
    private MapRegion _currentPosition;
    private MapRegion _lastFriendlyPosition;
    private const float TransitionMaxLength = 2;

    //Метод для установки начального местоположения
    public void SetFirstCurrentPosition()
    {
        _currentPosition = initialRegion;
    }

    // Метод для перемещения игрока в указанный регион
    public MapRegion MoveToRegion(Player player, MapRegion targetRegion)
    {
        // Проверяем, является ли целевой регион доступным для перемещения из текущего региона
        if (TryMovePosition(targetRegion))
        {
            player.position = _currentPosition;
        }
        else
        {
            Debug.LogWarning("Невозможно переместиться в указанный регион.");
        }

        return _currentPosition;
    }

    // Метод для попытки перемещения в другой регион
    public bool TryMovePosition(MapRegion region)
    {
        // Проверяем, допустимо ли перемещение на такое расстояние
        if ((region.position - _currentPosition.position).magnitude > TransitionMaxLength)
        {
            return false;
        }

        // Если регион - вражеский, обновляем последнюю позицию дружественных войск
        if (region.regionType.ToLower() == "enemy")
        {
            _lastFriendlyPosition = _currentPosition;
        }

        // Обновляем текущую позицию игрока
        _currentPosition = region;

        return true;
    }
}