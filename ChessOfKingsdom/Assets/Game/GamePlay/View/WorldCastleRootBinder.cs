using GamePlay.View;
using GamePlay.View.Buildings;
using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;
using View.Buildings;

public class WorldCastleRootBinder : MonoBehaviour
{
    [SerializeField] private BuildingsBinder _prefapBuildings;

    private readonly Dictionary<int, BuildingsBinder> _createBuildingsMap = new();

    private readonly CompositeDisposable _disposable = new();

    public void Bind(WorldCastleRootViewModel viewModel)
    {
        foreach(var buildingsViewModel in viewModel.AllBuildings)
        {
            CreateBuilding(buildingsViewModel);
        }

        _disposable.Add(viewModel.AllBuildings.ObserveAdd().Subscribe(e => CreateBuilding(e.Value)));

        _disposable.Add(viewModel.AllBuildings.ObserveRemove().Subscribe(e => DestroyBuilding(e.Value)));
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }

    private void CreateBuilding(BuildingsViewModel viewModel)
    {
        var buildingLevel = Random.Range(1, 4);
        var buildingType = viewModel.TypeId;
        var prefapBuildingLevelPath = $"Prefaps/Castle/Buildings/Building_{buildingType}_{buildingLevel}";
        var buildingPrefap = Resources.Load<BuildingsBinder>(prefapBuildingLevelPath);
        var createdBuilding = Instantiate(_prefapBuildings);
        createdBuilding.Bind(viewModel);

        _createBuildingsMap[viewModel.BuildingsEntityId] = createdBuilding;
    }

    private void DestroyBuilding(BuildingsViewModel viewModel)
    {
        if (_createBuildingsMap.TryGetValue(viewModel.BuildingsEntityId, out var buildingsBinder))
        { 
            Destroy(buildingsBinder.gameObject);
            _createBuildingsMap.Remove(viewModel.BuildingsEntityId);
        }
    }
}
