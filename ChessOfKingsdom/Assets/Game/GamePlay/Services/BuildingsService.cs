using Buildings;
using GamePlay.Commands;
using GamePlay.View.Buildings;
using ObservableCollections;
using R3;
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
        private readonly Dictionary<int, BuildingsViewModel> _buildingsMap = new();
        private readonly Dictionary<string, BuildingSettings> _buildingsSettingsMap = new();

        public IObservableCollection<BuildingsViewModel> AllBuildings => _allBuildings; 

        public BuildingsService(IObservableCollection<BuildingEntityProxy> buildings, ICommandProcessor cmd, BuildingsSettings buildingsSettings)
        {
            _cmd = cmd;

            foreach(var buildingSettings in buildingsSettings.AllBuildings)
            {
                _buildingsSettingsMap[buildingSettings.TypeId] = buildingSettings;
            }

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
            return false;
        }

        public bool DeleteBuildings(int buildingEntityId)
        {
            return false;
        }

        private void CreateBuildingsViewModel(BuildingEntityProxy buildingEntity)
        {
            var buildingSettings = _buildingsSettingsMap[buildingEntity.TypeId];
            var buildingViewModel = new BuildingsViewModel(buildingEntity, buildingSettings ,this);

            _allBuildings.Add(buildingViewModel);
            _buildingsMap[buildingEntity.Id] = buildingViewModel;
        }

        private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
        {
            if(_buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
            {
                _allBuildings.Remove(buildingViewModel);
                _buildingsMap.Remove(buildingEntity.Id);
            }
        }
    }
}
