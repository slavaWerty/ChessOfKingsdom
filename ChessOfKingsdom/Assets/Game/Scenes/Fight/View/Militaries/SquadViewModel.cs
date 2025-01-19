using R3;
using States.Military;
using UnityEngine;

namespace Fight.View.Militaries
{
    public class SquadViewModel
    {
        public ReadOnlyReactiveProperty<int> Amount;
        public readonly SquadType SquadType;
        public readonly float Speed;

        public SquadViewModel(Squad squad)
        {
            Amount = squad.Amount;
            SquadType = squad.SquadType;
            Speed = squad.Speed.Value;

            Debug.Log("SquadViewModel Init");
        }
    }
}
