using BaCon;
using mBuilding.Scripts.MVVM.UI;
using Fight.View.UI.LevelsPopup;
using R3;
using mBuilding.Scripts.Game.Common;
using Fight.Services;

namespace Fight.View.UI
{
    public class FightUIManager : UIManager
    {
        private Subject<Unit> _exitToSceneRequestSubj;
        private MilitaryService _militaryService;
 
        public FightUIManager(DIContainer container) : base(container)
        {
            _exitToSceneRequestSubj = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_CASTLE_TO_FIGHT_SCENE_REQUEST_TAG);
            _militaryService = container.Resolve<MilitaryService>();
        }

        public ScreenFightViewModel OpenScreenFight()
        {
            var viewModel = new ScreenFightViewModel(this, _exitToSceneRequestSubj, _militaryService);
            var rootUI = Container.Resolve<UIFightRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public LevelsPopupViewModel OpenLevelsPopup()
        {
            var a = new LevelsPopupViewModel();
            var rootUI = Container.Resolve<UIFightRootViewModel>();

            rootUI.OpenPopup(a);

            return a;
        }
    }
}
