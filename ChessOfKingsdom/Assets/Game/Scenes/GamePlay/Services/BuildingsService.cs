using Buildings;
using GamePlay.Commands;
using GamePlay.Services.BuildingsAbilities;
using GamePlay.View.Buildings;
using ObservableCollections;
using R3;
using Scenes.GamePlay.Commands;
using Settings.Castle;
using Settings.Castle.Buildings;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Services
{
    public class BuildingsService
    {
        private readonly ICommandProcessor _cmd;
        private readonly ObservableList<BuildingsViewModel> _allBuildings = new();
        public readonly Dictionary<int, BuildingsViewModel> BuildingsMap = new();
        public readonly Dictionary<string, BuildingSettings> BuildingsSettingsMap = new();
        public readonly Dictionary<string, IBuildingAbility> BuildingAbilitiesMap = new(); 

        public IObservableCollection<BuildingsViewModel> AllBuildings => _allBuildings; 

        public BuildingsService(IObservableCollection<BuildingEntityProxy> buildings,
            ICommandProcessor cmd, 
            BuildingsSettings buildingsSettings)
        {
            _cmd = cmd;

            foreach(var buildingSettings in buildingsSettings.AllBuildings)
            {
                BuildingsSettingsMap[buildingSettings.TypeId] = buildingSettings;
            }

            // Все это для теста
            BuildingAbilitiesMap["TestAbility"] = new TestBuildingAbility();
 

            foreach(var buildingEntity in buildings)
            {
                CreateBuildingsViewModel(buildingEntity);
            }

            buildings.ObserveAdd().Subscribe(e =>
            {
                CreateBuildingsViewModel(e.Value);
            });

            buildings.ObserveRemove().Subscribe(e =>
            {
                RemoveBuildingViewModel(e.Value);
            });
        }

        public bool PlaceBuilding(string buildingTypeId, Vector2Int Position)
        {
            var command = new CmdPlaceBuilding(buildingTypeId, Position);
            var result = _cmd.Process(command);

            return result;
        }

        public bool MoveBuilding(int buildingEntityId, Vector2Int newPosition)
        {
            var command = new CmdMoveBuildings(buildingEntityId, newPosition);
            var result = _cmd.Process(command);

            return result;
        }

        public bool DeleteBuildings(int buildingEntityId)
        {
            return false;
        }

        public BuildingsViewModel CreateBuildingsViewModel(BuildingEntityProxy buildingEntity)
        {
            var buildingSettings = BuildingsSettingsMap[buildingEntity.TypeId];
            var buildingViewModel = new BuildingsViewModel(buildingEntity, 
                buildingSettings , 
                this,
                BuildingAbilitiesMap["TestAbility"]);

            _allBuildings.Add(buildingViewModel);
            BuildingsMap[buildingEntity.Id] = buildingViewModel;

            return buildingViewModel;
        }

        private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
        {
            if(BuildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
            {
                _allBuildings.Remove(buildingViewModel);
                BuildingsMap.Remove(buildingEntity.Id);
            }
        }
    }
}
