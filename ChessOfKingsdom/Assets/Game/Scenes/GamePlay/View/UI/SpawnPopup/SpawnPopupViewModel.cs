using GamePlay.Services;
using mBuilding.Scripts.MVVM.UI;
using StateRoot;

namespace GamePlay.View.UI.SpawnPopup
{
    public class SpawnPopupViewModel : WindowViewModel
    {
        public BuildingsService BuildingsService;
        public ICommandProcessor Cmd;
        public GameStateProxy GameState;
        public WorldCastleRootBinder RootBinder;
        public ResourcesService ResourcesService;

        public override string Id => "SpawnPopup";

        public SpawnPopupViewModel(BuildingsService buildingsService,
            WorldCastleRootBinder rootBinder, 
            ICommandProcessor cmd, 
            GameStateProxy gameState,
            ResourcesService resourcesService)
        {
            BuildingsService = buildingsService;
            RootBinder = rootBinder;
            Cmd = cmd;
            GameState = gameState;
            ResourcesService = resourcesService;
        }
    }
}
