using UnityEngine;

namespace GamePlay.Commands
{
    public class CmdPlaceBuilding : ICommand
    {
        public readonly string BuildingTypeId;
        public readonly Vector2Int Position;

        public CmdPlaceBuilding(string buildingTypeId, Vector2Int position)
        {
            BuildingTypeId = buildingTypeId;
            Position = position;
        }
    }
}
