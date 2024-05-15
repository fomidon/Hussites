using UnityEngine;

public static class GameManager
{
    public static Player player { get; private set; }
    public const float TransitionMaxLength = 2;

    public static void Initialize(Player playerData)
    {
        player = playerData;
    }

    public static void MovePlayer(MapRegion province)
    {
        if (!player.CanMovePosition(province))
        {
            return;
        }
        player.TryMovePosition(province);
    }
}