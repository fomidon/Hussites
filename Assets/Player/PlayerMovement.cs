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
            TurnManager.Instance.UseMove();
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
        if ((region.Position - _currentPosition.Position).magnitude > TransitionMaxLength)
        {
            return false;
        }
        
        // Обновляем текущую позицию игрока
        _currentPosition = region;

        // Если регион - не вражеский, обновляем последнюю позицию дружественных войск
        if (region.regionType.ToLower() != "enemy")
        {
            _lastFriendlyPosition = _currentPosition;
        }
        return true;
    }

    public void Teleport(Player player, MapRegion region)
    {
        _currentPosition = region;
        if (region.regionType.ToLower() != "enemy")
        {
            _lastFriendlyPosition = _currentPosition;
        }
        player.position = _currentPosition;
    }
    
    // Телепорт в последнюю дружественную позицию
    public MapRegion EmergencyTeleport(Player player)
    {
        Teleport(player, _lastFriendlyPosition);
        return _lastFriendlyPosition;
    }
}