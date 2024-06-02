using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public MapRegion initialRegion;
    [FormerlySerializedAs("_currentPosition")] public MapRegion currentPosition;
    private MapRegion _lastFriendlyPosition;
    private const float TransitionMaxLength = 2;

    //Метод для установки начального местоположения
    public void SetFirstCurrentPosition()
    {
        currentPosition = initialRegion;
    }

    // Метод для перемещения игрока в указанный регион
    public MapRegion MoveToRegion(Player player, MapRegion targetRegion)
    {
        // Проверяем, является ли целевой регион доступным для перемещения из текущего региона
        if (TryMovePosition(targetRegion))
        {
            TurnManager.Instance.UseMove();
            player.position = currentPosition;
        }
        else
        {
            Debug.LogWarning("Невозможно переместиться в указанный регион.");
        }

        return currentPosition;
    }

    // Метод для попытки перемещения в другой регион
    public bool TryMovePosition(MapRegion region)
    {
        // Проверяем, допустимо ли перемещение на такое расстояние
        if ((region.Position - currentPosition.Position).magnitude > TransitionMaxLength)
        {
            return false;
        }
        
        // Обновляем текущую позицию игрока
        currentPosition = region;

        // Если регион - не вражеский, обновляем последнюю позицию дружественных войск
        if (region.regionType.ToLower() != "enemy")
        {
            _lastFriendlyPosition = currentPosition;
        }
        return true;
    }

    public void Teleport(Player player, MapRegion region)
    {
        currentPosition = region;
        if (region.regionType.ToLower() != "enemy")
        {
            _lastFriendlyPosition = currentPosition;
        }
        player.position = currentPosition;
    }
    
    // Телепорт в последнюю дружественную позицию
    public MapRegion EmergencyTeleport(Player player)
    {
        Teleport(player, _lastFriendlyPosition);
        return _lastFriendlyPosition;
    }
}