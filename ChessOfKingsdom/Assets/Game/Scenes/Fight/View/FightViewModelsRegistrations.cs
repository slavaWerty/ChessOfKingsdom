using BaCon;
using Fight.Services;
using Fight.View;
using Fight.View.UI;

public class FightViewModelsRegistrations 
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new FightUIManager(container)).AsSingle();
        container.RegisterFactory(c => new UIFightRootViewModel()).AsSingle();
        container.RegisterFactory(c => new WorldFightRootViewModel(container.Resolve<MilitaryService>())).AsSingle();
    }
}
