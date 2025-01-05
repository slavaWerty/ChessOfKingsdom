using Buildings;
using GamePlay.Services;
using R3;
using Settings.Castle;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.View.Buildings
{
    public class BuildingsViewModel
    {
        private readonly BuildingEntityProxy buildingProxy;
        private readonly BuildingsService buildings;
        private readonly BuildingSettings _settings;
        private readonly Dictionary<int, BuildingsLevelSettings> _levelSettings = new(); 

        public readonly int BuildingsEntityId;
        public ReadOnlyReactiveProperty<Vector2Int> Position { get; }
        public ReadOnlyReactiveProperty<int> Level { get; }
        public readonly string TypeId;

        public BuildingsViewModel(BuildingEntityProxy buildingProxy, BuildingSettings settings,BuildingsService buildings)
        {
            BuildingsEntityId = buildingProxy.Id;
            _settings = settings;
            TypeId = buildingProxy.TypeId;
            Level = buildingProxy.Level;

            this.buildingProxy = buildingProxy;
            this.buildings = buildings;

            foreach(var levelSettings in settings.BuildingsLevelSettings)
            {
                _levelSettings[levelSettings.Level] = levelSettings;
            }

            Position = buildingProxy.Position;
        }

        public BuildingsLevelSettings GetLevelSettings(int level)
        {
            return _levelSettings[level];
        }
    }
}
