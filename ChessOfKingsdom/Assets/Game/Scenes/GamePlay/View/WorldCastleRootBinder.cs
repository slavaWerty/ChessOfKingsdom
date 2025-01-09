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
    private WorldCastleRootViewModel _viewModel;

    public void Bind(WorldCastleRootViewModel viewModel)
    {
        _viewModel = viewModel;

        foreach (var buildingsViewModel in viewModel.AllBuildings)
        {
            CreateBuilding(buildingsViewModel);
        }

        _disposable.Add(viewModel.AllBuildings.ObserveAdd().Subscribe(e => CreateBuilding(e.Value)));

        _disposable.Add(viewModel.AllBuildings.ObserveRemove().Subscribe(e => DestroyBuilding(e.Value)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _viewModel.TestInput();
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }

    public BuildingsBinder CreateBuilding(BuildingsViewModel viewModel)
    {
        var buildingLevel = viewModel.Level.CurrentValue;
        var buildingType = viewModel.TypeId;
        var prefapBuildingLevelPath = $"Prefabs/Castle/Buildings/Building_{buildingType}_{buildingLevel}";
        var buildingPrefap = Resources.Load<BuildingsBinder>(prefapBuildingLevelPath);

        var createdBuilding = Instantiate(buildingPrefap);
        createdBuilding.Bind(viewModel);
        buildingPrefap.Bind(viewModel);

        _createBuildingsMap[viewModel.BuildingsEntityId] = createdBuilding;

        return createdBuilding;
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
