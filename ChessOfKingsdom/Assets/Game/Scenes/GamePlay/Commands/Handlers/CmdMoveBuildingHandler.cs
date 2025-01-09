using Buildings;
using StateRoot;

namespace Scenes.GamePlay.Commands.Handlers
{
    public class CmdMoveBuildingHandler : ICommandHandler<CmdMoveBuildings>
    {
        private readonly GameStateProxy _gameState;

        private BuildingEntityProxy _moveBuilding;

        public CmdMoveBuildingHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdMoveBuildings command)
        {
            var buildings = _gameState.Buildings;

            foreach (var building in buildings)
            {
                if (building.Id == command.BuildingEntityId)
                {
                    _moveBuilding = building;
                    break;
                }
            }

            _moveBuilding.Position.Value = command.Position;

            return true;
        }
    }
}
