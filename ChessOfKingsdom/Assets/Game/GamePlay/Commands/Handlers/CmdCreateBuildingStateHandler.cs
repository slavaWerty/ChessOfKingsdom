using Buildings;
using Game.Settings;
using StateRoot;
using System.Collections.Generic;

namespace mBuilding.Scripts.Game.Gameplay.Commands
{
    public class CmdCreateBuildingStateHandler : ICommandHandler<CmdCreateBuildingState>
    {
        private readonly GameStateProxy _gameState;
        private readonly GameSettings _gameSettings;

        public CmdCreateBuildingStateHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            _gameState = gameState;
            _gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateBuildingState command)
        {
            var newBuildingSettings = _gameSettings.NewBuildingsSettingsp;
            var newBuildingsInitialStateSettings = newBuildingSettings.InitzialSettings;

            var initialBuildings = new List<BuildingsEntity>();
            foreach (var buildingSettings in newBuildingsInitialStateSettings.Buildings)
            {
                var initialBuilding = new BuildingsEntity
                {
                    Id = _gameState.GreateEntityId(),
                    TypeId = buildingSettings.TypeId,
                    Position = buildingSettings.Position,
                    Level = buildingSettings.Level
                };

                initialBuildings.Add(initialBuilding);
            }

            var newMapStateProxy = new List<BuildingEntityProxy>();

            foreach(var initzialzBuilding in initialBuildings)
            {
                _gameState.Buildings.Add(new BuildingEntityProxy(initzialzBuilding));
            }

            return true;
        }
    }
}