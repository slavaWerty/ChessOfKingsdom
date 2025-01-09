using UnityEngine;

namespace Scenes.GamePlay.Commands
{
    public class CmdMoveBuildings : ICommand
    {
        public readonly int BuildingEntityId;
        public readonly Vector2Int Position;

        public CmdMoveBuildings(int buildingTypeId, Vector2Int position)
        {
            BuildingEntityId = buildingTypeId;
            Position = position;
        }
    }
}
