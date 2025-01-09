using R3;
using States.GameResources;

namespace GamePlay.View
{
    public class ResourceViewModel
    {
        public readonly ResourceType ResourceType;
        public ReadOnlyReactiveProperty<int> Amount;

        public ResourceViewModel(Resource resource)
        {
            ResourceType = resource.ResourceType;
            Amount = resource.Amount;
        }
    }
}
