using BaCon;
using GameRoot.Services;
using MainMenu.Services;

namespace MainMenu
{
    public static class MainMenuRegistations
    {
        public static void Register(DIContainer container, MenuEnterParams enterParams)
        {
            container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}
