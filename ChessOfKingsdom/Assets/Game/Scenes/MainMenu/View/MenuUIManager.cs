using BaCon;
using MainMenu.View;
using mBuilding.Scripts.Game.Common;
using mBuilding.Scripts.MVVM.UI;
using R3;
using Scenes.MainMenu.View.UI.Author;
using Scenes.MainMenu.View.UI.Screen;
using Scenes.MainMenu.View.UI.Settings;

namespace Assets.Game.Scenes.MainMenu.View
{
    public class MenuUIManager : UIManager
    {
        private readonly Subject<Unit> _exitSceneRequest;

        public MenuUIManager(DIContainer container) : base(container)
        {
            _exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_IN_CASTLE_SCENE_REQUEST_TAG);
        }

        public ScreenMenuViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenMenuViewModel(this, _exitSceneRequest);
            var rootUI = Container.Resolve<UIMainMenuRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public SettingsViewModel OpenSetting()
        {
            var a = new SettingsViewModel();
            var rootUI = Container.Resolve<UIMainMenuRootViewModel>();

            rootUI.OpenPopup(a);

            return a;
        }

        public AuthorViewModel OpenAuthor()
        {
            var b = new AuthorViewModel();
            var rootUI = Container.Resolve<UIMainMenuRootViewModel>();

            rootUI.OpenPopup(b);

            return b;
        } 
    }
}
