using Assets.Game.Scenes.MainMenu.View;
using BaCon;

namespace MainMenu.View
{
    public static class MainMenuViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UIMainMenuRootViewModel()).AsSingle();
            container.RegisterFactory(c => new MenuUIManager(container)).AsSingle();
        }
    }
}
