using GamePlay.Services;
using GamePlay.View.Buildings;
using ObservableCollections;
using States.GameResources;
using UnityEngine;
using R3;
using System;

namespace GamePlay.View
{
    public class WorldCastleRootViewModel
    {
        private readonly ResourcesService _resourcesService;
        public readonly IObservableCollection<BuildingsViewModel> AllBuildings;

        public WorldCastleRootViewModel(BuildingsService service, ResourcesService resourcesService)
        {
            AllBuildings = service.AllBuildings;
            _resourcesService = resourcesService;

            resourcesService.ObserveResource(ResourceType.SoftCurrency)
                .Subscribe(newValue => Debug.Log(newValue));

            resourcesService.ObserveResource(ResourceType.HardCurrency)
                .Subscribe(newValue => Debug.Log(newValue));
        }
        public void TestInput()
        {
            var resourceType = (ResourceType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ResourceType)).Length);
            var rValue = UnityEngine.Random.Range(1, 1000);
            var rOperation = UnityEngine.Random.Range(0, 2);

            if (rOperation == 0)
            {
                _resourcesService.AddResources(resourceType, rValue);
            }

            _resourcesService.TrySpendResources(resourceType, rValue);
        }
    }
}
