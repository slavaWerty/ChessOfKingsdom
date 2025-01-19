using Fight.Services;

namespace Fight.View
{
    public class WorldFightRootViewModel
    {
        public MilitaryService MilitaryService { get; }

        public WorldFightRootViewModel(MilitaryService militaryService)
        {
            MilitaryService = militaryService;
        }
    }
}
