using Buildings;
using Game.Settings;
using StateRoot;
using System.Collections.Generic;

public class CmdCreateBuildingsStateHandler : ICommandHandler<CmdCreateBuildingState>
{
    private readonly GameStateProxy _gameState;
    private readonly GameSettings _gameSettings;

    public CmdCreateBuildingsStateHandler(GameStateProxy gameState, GameSettings gameSettings)
    {
        _gameState = gameState;
        _gameSettings = gameSettings;
    }

    public bool Handle(CmdCreateBuildingState command)
    {
        var newBuildingSettings = _gameSettings.NewBuildingsSettingsp;
        var newBuildingInitialStateSettings = newBuildingSettings.InitzialSettings;

        var initialBuildings = new List<BuildingsEntity>();
        foreach (var buildingSettings in newBuildingInitialStateSettings.Buildings)
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

        var newBuildingStateProxy = new List<BuildingEntityProxy>();

        foreach (var initzialBuilding in initialBuildings)
            _gameState.Buildings.Add(new BuildingEntityProxy(initzialBuilding));

        return true;
    }
}
