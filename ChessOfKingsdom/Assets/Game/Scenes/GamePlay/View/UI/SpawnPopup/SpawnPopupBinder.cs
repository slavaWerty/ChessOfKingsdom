using GamePlay.Commands;
using GamePlay.View.UI.SpawnPopup;
using mBuilding.Scripts.MVVM.UI;
using States.GameResources;
using System.Linq;
using View.Buildings;

public class SpawnPopupBinder : PopupBinder<SpawnPopupViewModel>
{
    private BuildingsGrid _grid;

    private void Awake()
    {
        _grid = FindAnyObjectByType<BuildingsGrid>();
    }

    public void PlaceBuilding(BuildingsBinder building)
    {
        var settings = ViewModel.BuildingsService.BuildingsSettingsMap[building.TypeId];

        if (ViewModel.ResourcesService
           .TrySpendResources(ResourceType.SoftCurrency, settings.Price))
        {

            var gameStateProvider = ViewModel.GameState;
            var command = new CmdPlaceBuilding(building.TypeId, building.Position);
            ViewModel.Cmd.Process(command);
            var buildingProxy = ViewModel.GameState.Buildings.First(
                e => e.TypeId == building.TypeId);

            var buildingsService = ViewModel.BuildingsService;
            var viewModel = buildingsService.CreateBuildingsViewModel(buildingProxy);

            ViewModel.RootBinder.CreateBuilding(viewModel);

            _grid.StartPlacingBuilding(building,
                ViewModel.BuildingsService,
                viewModel);
        }
    }
}
