using Assets.Game.Scenes.MainMenu.View;
using mBuilding.Scripts.MVVM.UI;
using R3;

namespace Scenes.MainMenu.View.UI.Screen
{
    public class ScreenMenuViewModel : WindowViewModel
    {
        private readonly MenuUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Id => "ScreenMenu";

        public ScreenMenuViewModel(MenuUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = uiManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestOpenSetting()
        {
            _uiManager.OpenSetting();
        }

        public void RequestOpenAuthor()
        {
            _uiManager.OpenAuthor();
        }

        public void RequestGoToCastle()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}
