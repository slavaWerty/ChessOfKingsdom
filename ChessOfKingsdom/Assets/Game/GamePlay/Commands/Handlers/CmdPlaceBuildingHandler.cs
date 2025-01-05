using Buildings;
using StateRoot;

namespace GamePlay.Commands
{
    internal class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuilding>
    {
        private readonly GameStateProxy _gameState;

        public CmdPlaceBuildingHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdPlaceBuilding command)
        {
            var entityID = _gameState.GreateEntityId();
            var newBuildingEntity = new BuildingsEntity
            {
                Id = entityID,
                Position = command.Position,
                TypeId = command.BuildingTypeId
            };

            var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);

            _gameState.Buildings.Add(newBuildingEntityProxy);

            return true;
        }
    }
}
