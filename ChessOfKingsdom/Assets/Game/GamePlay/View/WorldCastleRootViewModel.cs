using GamePlay.Services;
using GamePlay.View.Buildings;
using ObservableCollections;

namespace GamePlay.View
{
    public class WorldCastleRootViewModel
    {
        public readonly IObservableCollection<BuildingsViewModel> AllBuildings;

        public WorldCastleRootViewModel(BuildingsService service)
        {
            AllBuildings = service.AllBuildings;
        }
    }
}
