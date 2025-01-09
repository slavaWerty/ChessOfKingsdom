using BaCon;
using mBuilding.Scripts.Game.Common;
using R3;

namespace MainMenu
{
    public static class MainMenuRegistations
    {
        public static void Register(DIContainer container, MenuEnterParams enterParams)
        {
            container.RegisterInstance(AppConstants.EXIT_IN_CASTLE_SCENE_REQUEST_TAG, new Subject<Unit>());
        }
    }
}
