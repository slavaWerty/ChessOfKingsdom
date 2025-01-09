using BaCon;
using GamePlay.Services;

namespace GamePlay.View
{
    public static class CastleViewModelsRegistrations
    {
        public static void Register(DIContainer container, WorldCastleRootBinder rootBinder)
        {
            container.RegisterFactory(c => new CastleUIManager(container, rootBinder)).AsSingle();
            container.RegisterFactory(c => new UICastleRootViewModel()).AsSingle();
            container.RegisterFactory(c => new WorldCastleRootViewModel(c.Resolve<BuildingsService>(), c.Resolve<ResourcesService>())).AsSingle();
        }
    }
}
