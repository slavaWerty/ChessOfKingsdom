using ObservableCollections;
using States.Military;
using UnityEngine;

namespace Fight.View.Militaries
{
    public class MilitaryViewModel
    {
        public readonly int Id;
        public ObservableList<Squad> SquadEntityes;

        public ObservableList<SquadViewModel> SquadViewModels = new();

        public MilitaryViewModel(Military military)
        {
            Id = military.Id;
            SquadEntityes = military.SquadeDates;

            Debug.Log("MilitaryViewModel Init");
        }
    }
}
