using BaCon;
using GamePlay.Services;

namespace GamePlay.View
{
    public static class CastleViewModelsRegistrations
    {
        public static void Register(DIContainer container)
        {
            container.RegisterFactory(c => new UICastleRootViewModel()).AsSingle();
            container.RegisterFactory(c => new WorldCastleRootViewModel(c.Resolve<BuildingsService>(), c.Resolve<ResourcesService>())).AsSingle();
        }
    }
}
